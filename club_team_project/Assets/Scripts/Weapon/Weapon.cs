using System;
using System.Collections;
using UnityEditor;
using UnityEngine;



public class Weapon : MonoBehaviour
{
    public event Action<WeaponStatSO, int> OnWeaponEquipped;
    public event Action<int> OnAmmoChanged;
    public event Action<bool> OnReloadStatusChanged;

    public Entity_Stat entitystat { get; private set; }
    private IWeaponAction currentStrategy;

    private WeaponType currentWeaponType;

    [Header("총알")]

    public Transform firePoint;
    public GameObject bulletPrefab { get; private set; }
    public GameObject muzzleflashPrefab { get; private set; }

    public float RiflebetweenShots;
    public int ShootBullet;
    public bool isShooting { get; private set; }

    private float burstFireTimer;
    private int shotsFiredInBurst;

    [Header("Weapon State")]
    [SerializeField] private int currentBullets;
    [SerializeField] private bool isReloading;
    [SerializeField] private bool isAttackCooldown;

    private Color color = new Color(0, 1, 0, 0.3f);

    public bool IsWeaponBusy()
    {
        // 점사 중이거나, 공격 쿨다운 중이거나, 재장전 중이면 true 반환
        return isShooting || isAttackCooldown || isReloading;
    }

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

    private void Update()
    {
        HandleBurstFire();
    }
    public void StartRifleShoot()
    {
        // 쿨다운/재장전/이미 점사중이면 시작 안함
        if (isAttackCooldown || isReloading || isShooting)
        {
            return;
        }

        // 탄약 체크
        if (currentBullets == 0)
        {
            StartCoroutine(CoReload());
            return;
        }

        // 점사 상태로 전환
        isShooting = true;
        shotsFiredInBurst = 0;
        burstFireTimer = 0f; // 즉시 첫 발 발사
    }

    private void HandleBurstFire()
    {
        if (!isShooting) return;

        // 타이머가 0 이하가 되면 발사
        if (burstFireTimer <= 0f)
        {
            // 1. 실제 발사 로직 호출
            RequestAttack(); // (이 안에서 WRifle.PerformAttack()가 불림)
            shotsFiredInBurst++;

            // 2. 점사가 끝났는지 확인 (혹은 탄창이 비었는지)
            if (shotsFiredInBurst >= ShootBullet || currentBullets == 0)
            {
                // 점사 완료
                isShooting = false;
                // "점사가 끝난 후"에 전체 공격 쿨다운 시작
                StartCoroutine(CoStartAttackCooldown());
            }
            else
            {
                // 3. 다음 발사를 위해 타이머 재설정
                burstFireTimer = RiflebetweenShots;
            }
        }
        else
        {
            // 타이머 시간 감소
            burstFireTimer -= Time.deltaTime;
        }
    }

    public void HandleAttack()
    {
        if (isReloading || isAttackCooldown)
        {
            return;
        }

        currentStrategy?.OnAttackInput();
    }

    [ContextMenu("테스트")]
    public void RequestAttack()
    {
        if (isReloading || isAttackCooldown)
        {
            Debug.Log($"[Weapon] 발사 실패: 쿨다운({isAttackCooldown}) 또는 재장전({isReloading}) 중");
            return;
        }

        if (currentBullets == 0)
        {
            Debug.Log("[Weapon] 발사 실패: 총알이 없음 -> 재장전 시도");
            StartCoroutine(CoReload());
            return;
        }

        currentBullets--;
        Debug.Log($"발사! 남은 총알: {currentBullets}");

        OnAmmoChanged?.Invoke(currentBullets);
        if (currentStrategy == null)
        {
            Debug.LogError("[Weapon] 오류: 무기 전략(Strategy)이 연결되지 않았습니다!");
            return;
        }

        currentStrategy?.PerformAttack();

        StartCoroutine(CoStartAttackCooldown());
    }

    public void EquipNewWeapon(WeaponStatSO stats, IWeaponAction strategy, int ammoToLoad)
    {
        entitystat.EquipNewWeapon(stats);

        currentWeaponType = stats.weaponType;

        currentStrategy = strategy;
        currentStrategy.Initialize(this, entitystat);

        bulletPrefab = stats.bulletPrefab;
        muzzleflashPrefab = stats.muzzleflashPrefab;
        currentBullets = ammoToLoad;

        isReloading = false;
        isAttackCooldown = false;
        StopAllCoroutines();

        Debug.Log($"{stats.weaponType} 장착! 전략: {strategy.GetType().Name}");

        OnWeaponEquipped?.Invoke(stats, currentBullets);
        OnReloadStatusChanged?.Invoke(false);

    }



    private IEnumerator CoStartAttackCooldown()
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

    private IEnumerator CoReload()
    {
        isReloading = true;
        float reloadTime = entitystat.GetReloadTime();
        OnReloadStatusChanged?.Invoke(true);
        if (currentWeaponType == WeaponType.shotgun)
        {
            int reloadSoundCount = 5;
            float interval = reloadTime / reloadSoundCount;
            for (int i = 0; i < reloadSoundCount; i++)
            {
                SoundManager.Instance.PlaySFX(SoundManager.Instance.shotgunReloadClip);

                yield return new WaitForSeconds(interval);
            }
        }
        else
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.reloadClip);
            yield return new WaitForSeconds(reloadTime);
        }
        currentBullets = (int)entitystat.GetStatByType(StatType.MaxBullets).GetValue();
        OnAmmoChanged?.Invoke(currentBullets);
        isReloading = false;
        OnReloadStatusChanged?.Invoke(false);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        // 1. entitystat이 null일 경우(Awake 전) 대비
        if (entitystat == null)
        {
            entitystat = GetComponent<Entity_Stat>();
        }
        if (entitystat == null) return; // 그래도 없으면 중단

        Vector3 rangeCenter = (transform.position != null) ? firePoint.position : transform.position;

        // 2. Update() 대신, OnDrawGizmos가 매번 직접 스탯 값을 가져옵니다.
        float currentFov = entitystat.GetFovRadius();
        float currentRadius = entitystat.GetFovRadius();
        float currentRange = entitystat.GetRange();

        // 3. 값이 유효할 때만 그립니다.
        if (currentFov <= 0 || currentRadius <= 0)
        {
            Handles.color = color;

            Vector2 myUp = transform.rotation * Vector3.up;
            Vector2 startDirection = Quaternion.Euler(0, 0, -360 / 2) * myUp;
            Handles.DrawSolidArc(transform.position, Vector3.forward, startDirection, 360, currentRadius);
        }

        if (currentRange > 0)
        {
            Gizmos.color = Color.white; // 총알 사거리는 다른 색으로
            // [수정] 기즈모의 중심을 transform.position이 아닌 rangeCenter(총구)로 변경
            Gizmos.DrawWireSphere(rangeCenter, currentRange);
        }

        float currentShotgunRange = entitystat.GetShotgunRange();
        float currentShotgunRadius = entitystat.GetShotgunRadius();
        if (currentShotgunRange > 0 && currentShotgunRadius > 0)
        {
            Handles.color = Color.yellow; // 샷건 범위는 노란색으로
            Vector2 myUp = transform.rotation * Vector3.up;
            Vector2 startDirection = Quaternion.Euler(0, 0, -currentShotgunRange / 2) * myUp;
            Handles.DrawSolidArc(transform.position, Vector3.forward, startDirection, currentShotgunRange, currentShotgunRadius);
        }
    }
#endif
}