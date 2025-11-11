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
        // 총알 1발 생성 코드
        GameObject bullet = weapon.bulletPrefab;
        GameObject bulletObj = Object.Instantiate(bullet, weapon.firePoint.position, weapon.firePoint.rotation);
        GameObject muzzleflash = weapon.muzzleflashPrefab;
        GameObject muzzleflashObj = Object.Instantiate(muzzleflash, weapon.firePoint.position, weapon.firePoint.rotation);

        muzzleflashObj.transform.SetParent(weapon.firePoint);

        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = stats.GetDamage();
            bulletScript.bulletSpeed = stats.GetBulletSpeed();
            bulletScript.maxRange = stats.GetRange();
        }
    }
}
