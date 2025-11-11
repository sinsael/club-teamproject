using UnityEngine;
using System.Collections;

public class Player : Entity
{
    public PlayerInputHandler input { get; private set; }
    public Weapon Weapon { get; private set; }

    public Interaction interaction { get; private set; }
    public Entity_Stat Entity_Stat { get; private set; }
    public Entity_Health Entity_Health { get; private set; }

    //플레이어 상태
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }


    private Camera maincamera;
    [SerializeField] private GameObject whiteScreenPanel;

    [Header("Flashbang Grenade")]
    [SerializeField] private GameObject flashbangPrefab;
    [SerializeField] private WeaponStatSO flashbangWeaponStat;
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

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        if(flashbangTimer > 0)
        {
            flashbangTimer -= Time.deltaTime;
        }

        Shoot();
        HandleMouseRotation();
        Intertable();
        input.SwitchWeaponInput();
        ThrowFlashbang();
    }
    public override void OnStun(float duration)
    {
        base.OnStun(duration);
        // 이미 스턴 상태라면 중복 실행 방지 (선택적)
        if (whiteScreenPanel != null && whiteScreenPanel.activeInHierarchy)
            return;

        Debug.Log($"[플레이어] 섬광탄에 {duration}초 동안 스턴!");
        StartCoroutine(CoShowWhiteScreen(duration));
    }
    private void HandleMouseRotation()
    {
        if (maincamera == null) return;

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
            float speed = flashbangWeaponStat.BulletSpeed;

            grenadeScript.Initialize(radius, duration, speed, whatIsTarget);
        }
    }

    private IEnumerator CoShowWhiteScreen(float duration)
    {
        // 1. 하얀 패널을 켠다
        if (whiteScreenPanel != null)
            whiteScreenPanel.SetActive(true);

        // 2. duration(스턴 시간)만큼 기다린다
        yield return new WaitForSeconds(duration);

        // 3. 하얀 패널을 다시 끈다
        if (whiteScreenPanel != null)
            whiteScreenPanel.SetActive(false);
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
