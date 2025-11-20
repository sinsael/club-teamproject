using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : Entity
{
    public PlayerInputHandler input { get; private set; }
    public Weapon Weapon { get; private set; }

    [System.Serializable]
    public struct WeaponSpriteMapping
    {
        public WeaponStatSO weaponData; // 예: PistolData
        public Sprite weaponSprite;     // 예: 권총 든 이미지
        public Vector2 firePointPos;
    }

    [Header("Weapon Visuals")]
    [SerializeField] private List<WeaponSpriteMapping> weaponSpriteList;

    private SpriteRenderer spriteRenderer; // [추가]

    public Interaction interaction { get; private set; }
    public Entity_Stat Entity_Stat { get; private set; }
    public Entity_Health Entity_Health { get; private set; }

    //플레이어 상태
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }


    private Camera maincamera;
    [SerializeField] private Image whiteScreenPanel;

    [Header("Flashbang Grenade")]
    [SerializeField] private GameObject flashbangPrefab;
    [SerializeField] private FlashStatSO flashbangWeaponStat;
    [SerializeField] private float flashbangCooldown = 5f;
    [SerializeField] private Transform firePoint;
    [SerializeField] private LayerMask whatIsTarget;

    private float flashbangTimer = 0f;



    public override void Awake()
    {
        base.Awake();
        input = GetComponent<PlayerInputHandler>();
        interaction = GetComponent<Interaction>();
        Entity_Stat = GetComponent<Entity_Stat>();
        Entity_Health = GetComponent<Entity_Health>();
        Weapon = GetComponent<Weapon>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>(); // [추가]

        if (Weapon != null)
        {
            Weapon.OnWeaponEquipped += HandleWeaponChange;
        }


        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        maincamera = Camera.main;


    }

    private void OnDestroy()
    {
        if (Weapon != null)
        {
            Weapon.OnWeaponEquipped -= HandleWeaponChange;
        }
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if(flashbangTimer > 0)
        {
            flashbangTimer -= Time.deltaTime;
        }

        interaction.UpdateObjDetected();
        interaction.FindBestTarget();
        interaction.HandleTargetChange();

        Shoot();
        HandleMouseRotation();
        Intertable();
        input.SwitchWeaponInput();
        ThrowFlashbang();
    }

    public override void OnStun(float duration)
    {
        base.OnStun(duration);
        if (whiteScreenPanel != null && whiteScreenPanel.gameObject.activeInHierarchy)
            return;

        StartCoroutine(CoShowWhiteScreen(duration));
    }
    private void HandleMouseRotation()
    {
        if (maincamera == null) return;
        if (GameManager.Instance.currentGameState != GameState.GamePlay) return;

        Vector3 mouseScreenPos = Input.mousePosition;

        mouseScreenPos.z = maincamera.WorldToScreenPoint(transform.position).z;

        Vector3 mouseWorldPos = maincamera.ScreenToWorldPoint(mouseScreenPos);

        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        transform.up = direction;
    }

    public void Shoot()
    {
        if (input.shoot)
        {
            Weapon?.HandleAttack();
        }
    }

    private void Intertable()
    {
        if (input.interact)
        {
            interaction.Interact();
        }
    }

    private void ThrowFlashbang()
    {
        if (input.flash && flashbangTimer <= 0)
        {
            flashbangTimer = flashbangCooldown;

            GameObject grenadeObj = Instantiate(flashbangPrefab, firePoint.position, firePoint.rotation);
            FlashbangGrenade grenadeScript = grenadeObj.GetComponent<FlashbangGrenade>();

            float radius = flashbangWeaponStat.FlashbangRadius;
            float duration = flashbangWeaponStat.FlashbangDuration;
            float speed = flashbangWeaponStat.FlashSpeed;

            grenadeScript.Initialize(radius, duration, speed, whatIsTarget);
        }
    }

    private void HandleWeaponChange(WeaponStatSO newWeapon, int ammo)
    {

        foreach (var mapping in weaponSpriteList)
        {
            if (mapping.weaponData == newWeapon)
            {
                // 1. 스프라이트 교체
                if (spriteRenderer != null)
                {
                    spriteRenderer.sprite = mapping.weaponSprite;
                }

                // 2. [추가] 총구(FirePoint) 위치 교체
                // transform.position이 아니라 localPosition을 써야 플레이어 기준으로 움직입니다.
                if (Weapon != null && Weapon.firePoint != null)
                {
                    Weapon.firePoint.localPosition = mapping.firePointPos;
                }
                else
                {

                    Weapon.firePoint.localPosition = mapping.firePointPos;
                }

                return;
            }
        }
    }

    private IEnumerator CoShowWhiteScreen(float duration)
    {
        // 1. 하얀 패널을 켠다
        if (whiteScreenPanel != null)
            whiteScreenPanel.gameObject.SetActive(true);

        yield return new WaitForSeconds(duration);

        if (whiteScreenPanel != null)
            whiteScreenPanel.gameObject.SetActive(false);
    }

    [ContextMenu("test die")]
    public override void EntityDeath()
    {
        base.EntityDeath();

        SoundManager.Instance.PlaySFX(SoundManager.Instance.playerDeathClip, 1f);
        gameObject.SetActive(false);
    }

    protected void OnDrawGizmos()
    {
        if (interaction == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interaction.InterCheck.position, interaction.InterCheckRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interaction.interactionCheck.position, interaction.interactionRadius);
    }


}
