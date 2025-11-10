using System.Collections;
using UnityEditor;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Entity_Stat entitystat { get; private set; }
    private IWeaponAction currentStrategy;

    [Header("총알")]
    public Transform firePoint;
    public GameObject bulletPrefab { get; private set; }
    public GameObject muzzleflashPrefab { get; private set; }

    [Header("Weapon State")]
    [SerializeField] private int currentBullets;
    [SerializeField] private bool isReloading;
    [SerializeField] private bool isAttackCooldown;

    private Color color = new Color(0, 1, 0, 0.3f);

    public int GetCurrentAmmo()
    {
        return currentBullets;
    }

    private void Awake()
    {
        entitystat = GetComponent<Entity_Stat>();
    }

    private void Start()
    {
    }

    public void HandleAttack()
    {
        currentStrategy?.OnAttackInput();
    }

    [ContextMenu("테스트")]
    public void RequestAttack()
    {
        if (isReloading || isAttackCooldown)
        {
            return;
        }

        if (currentBullets == 0)
        {
            StartCoroutine(CoReload());
            return;
        }

        currentBullets--;
        Debug.Log($"발사! 남은 총알: {currentBullets}");

        currentStrategy?.PerformAttack();

        StartCoroutine(AttackCooldownRoutine());
    }

    public void EquipNewWeapon(WeaponStatSO stats, IWeaponAction strategy, int ammoToLoad)
    {
        entitystat.EquipNewWeapon(stats);

        currentStrategy = strategy;
        currentStrategy.Initialize(this, entitystat);

        bulletPrefab = stats.bulletPrefab;
        muzzleflashPrefab = stats.muzzleflashPrefab;
        currentBullets = ammoToLoad;

        isReloading = false;
        isAttackCooldown = false;
        StopAllCoroutines();

        Debug.Log($"{stats.weaponType} 장착! 전략: {strategy.GetType().Name}");

    }

    private IEnumerator AttackCooldownRoutine()
    {
        isAttackCooldown = true;
        float attackSpeed = entitystat.GetFireRate();

        if (attackSpeed <= 0)
        {
            attackSpeed = 1f;
            Debug.LogWarning($"[Weapon] {name}의 공격 속도가 0 이하입니다. 기본값 1초로 설정합니다.");
        }

        yield return new WaitForSeconds(attackSpeed);

        isAttackCooldown = false;
    }

    private void OnDrawGizmos()
    {
        // 1. entitystat이 null일 경우(Awake 전) 대비
        if (entitystat == null)
        {
            entitystat = GetComponent<Entity_Stat>();
        }
        if (entitystat == null) return; // 그래도 없으면 중단

        // 2. Update() 대신, OnDrawGizmos가 매번 직접 스탯 값을 가져옵니다.
        float currentFov = entitystat.GetFovRadius();
        float currentRadius = entitystat.GetFovRadius();
        float currentRange = entitystat.GetRange();

        // 3. 값이 유효할 때만 그립니다.
        if (currentFov <= 0 || currentRadius <= 0)
        {
            return;
        }

        Handles.color = color;

        Vector2 myUp = transform.rotation * Vector3.up;
        Vector2 startDirection = Quaternion.Euler(0, 0, -currentFov / 2) * myUp;

        Handles.DrawSolidArc(transform.position, Vector3.forward, startDirection, currentFov, currentRadius);

        Gizmos.DrawWireSphere(transform.position, currentRange);
    }

    private IEnumerator CoReload()
    {
        isReloading = true;
        float reloadTime = entitystat.GetReloadTime();
        yield return new WaitForSeconds(reloadTime);
        currentBullets = (int)entitystat.GetStatByType(StatType.MaxBullets).GetValue();
        isReloading = false;
    }
}