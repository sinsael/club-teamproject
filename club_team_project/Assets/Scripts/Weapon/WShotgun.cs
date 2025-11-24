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
        SoundManager.Instance.PlaySFX(SoundManager.Instance.shotgunClip);


        if (weapon.muzzleflashPrefab != null)
        {
            GameObject muzzleflashObj = Object.Instantiate(weapon.muzzleflashPrefab, weapon.firePoint.position, weapon.firePoint.rotation);

            muzzleflashObj.transform.localScale = weapon.muzzleflashPrefab.transform.localScale;

            muzzleflashObj.transform.SetParent(weapon.firePoint, true);

            muzzleflashObj.transform.localPosition = Vector3.zero;
            muzzleflashObj.transform.localRotation = Quaternion.identity;

        }

        float spreadAngle = stats.GetShotgunRange();
        float bulletRange = stats.GetShotgunRadius();
        float damage = stats.GetDamage();
        float bulletspeed = stats.GetBulletSpeed();

        float angleStep = (BulletCount > 1) ? spreadAngle / (BulletCount - 1) : 0;

        float startAngle = -spreadAngle / 2f;

        for (int i = 0; i < BulletCount; i++)
        {
            float currentAngleOffset = startAngle + (angleStep * i);

            Quaternion rotation = weapon.firePoint.rotation * Quaternion.Euler(0, 0, currentAngleOffset);

            GameObject bulletObj = Object.Instantiate(weapon.bulletPrefab, weapon.firePoint.position, rotation);

            Bullet bulletScript = bulletObj.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.damage = damage;
                bulletScript.maxRange = bulletRange;
                bulletScript.bulletSpeed = bulletspeed;

                bulletScript.weaponType = WeaponType.shotgun;
            }
        }
    }
}
