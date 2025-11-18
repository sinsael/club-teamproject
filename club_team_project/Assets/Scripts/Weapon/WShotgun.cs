using UnityEngine;

public class WShotgun : WeaponControllerBase
{
    // [추가] 우리가 업그레이드한 '공격 도구' 참조
    private Entity_TakeDamage entityAttack;
    private const int BulletCount = 5;
    public override void Initialize(Weapon weapon, Entity_Stat stats)
    {
        base.Initialize(weapon, stats);
    }

    public override void OnAttackInput()
    {
        base.OnAttackInput(); // weapon.RequestAttack() 호출
    }

    public override void PerformAttack()
    {
        // 1. 총구 이펙트 생성 (한 번만)
        if (weapon.muzzleflashPrefab != null)
        {
            GameObject muzzleflash = weapon.muzzleflashPrefab;
            GameObject muzzleflashObj = Object.Instantiate(muzzleflash, weapon.firePoint.position, weapon.firePoint.rotation);
            muzzleflashObj.transform.SetParent(weapon.firePoint);
        }

        // 2. 발사 로직: 부채꼴 계산
        // 사용자의 변수 매핑: GetShotgunRange()가 '각도', GetShotgunRadius()가 '사거리'라고 가정합니다.
        float spreadAngle = stats.GetShotgunRange(); // 전체 부채꼴 각도 (예: 45도)
        float bulletRange = stats.GetShotgunRadius(); // 총알 사거리
        float damage = stats.GetDamage();
        float bulletspeed = stats.GetBulletSpeed();

        // 각 총알 사이의 각도 간격 계산
        // (총알이 1발이면 나눌 수 없으므로 예외 처리)
        float angleStep = (BulletCount > 1) ? spreadAngle / (BulletCount - 1) : 0;

        // 시작 각도 (가장 왼쪽 총알의 각도)
        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < BulletCount; i++)
        {
            // 현재 총알의 각도 계산
            float currentAngleOffset = startAngle + (angleStep * i);

            // 회전값 계산: 총구의 기본 회전값 + 부채꼴 오프셋
            Quaternion rotation = weapon.firePoint.rotation * Quaternion.Euler(0, 0, currentAngleOffset);

            // 3. 총알 생성
            GameObject bulletObj = Object.Instantiate(weapon.bulletPrefab, weapon.firePoint.position, rotation);

            // 4. 총알 스탯 적용 (데미지, 사거리 등)
            Bullet bulletScript = bulletObj.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.damage = damage;
                bulletScript.maxRange = bulletRange;
                bulletScript.bulletSpeed = bulletspeed;
                // bulletScript.bulletSpeed = ...; // 속도도 스탯에 있다면 여기서 설정 가능
            }
        }
    }
}
