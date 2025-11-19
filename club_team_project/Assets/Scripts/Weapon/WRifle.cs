using UnityEngine;

public class WRifle : WeaponControllerBase
{
    public override void Initialize(Weapon weapon, Entity_Stat stats)
    {
        base.Initialize(weapon, stats);
        Debug.Log("소총 초기화");
    }
    public override void OnAttackInput()
    {
        weapon.StartRifleShoot();
    }

    public override void PerformAttack()
    {
        if (weapon.bulletPrefab == null) return;

        // 2. 총알 생성
        GameObject bulletObj = Object.Instantiate(weapon.bulletPrefab, weapon.firePoint.position, weapon.firePoint.rotation);

        // 3. 이펙트 생성
        if (weapon.muzzleflashPrefab != null)
        {
            GameObject muzzleflashObj = Object.Instantiate(weapon.muzzleflashPrefab, weapon.firePoint.position, weapon.firePoint.rotation);

            muzzleflashObj.transform.localScale = weapon.muzzleflashPrefab.transform.localScale;

            muzzleflashObj.transform.SetParent(weapon.firePoint, true);

            muzzleflashObj.transform.localPosition = Vector3.zero;
            muzzleflashObj.transform.localRotation = Quaternion.identity;
        }

        // 4. [핵심] 총알 속도 강제 주입
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        Rigidbody2D bulletRb = bulletObj.GetComponent<Rigidbody2D>();

        if (bulletScript != null)
        {
            // 스탯에서 속도 가져오기 (혹시 0이면 기본값 20으로 설정)
            float speed = stats.GetBulletSpeed();
            if (speed <= 0) speed = 20f;

            bulletScript.damage = stats.GetDamage();
            bulletScript.bulletSpeed = speed;
            bulletScript.maxRange = stats.GetRange();

            // [해결책] 총알 스크립트의 Start를 기다리지 말고, 여기서 물리 엔진으로 직접 쏩니다!
            if (bulletRb != null)
            {
                // Unity 6 이상은 linearVelocity, 구버전은 velocity
                bulletRb.linearVelocity = weapon.firePoint.up * speed;
                // bulletRb.velocity = weapon.firePoint.up * speed; // 구버전용
            }
        }
    }
}
