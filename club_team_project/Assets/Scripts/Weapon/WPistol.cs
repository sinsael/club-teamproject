using UnityEngine;
public class WPistol : WeaponControllerBase
{
    public override void Initialize(Weapon weapon, Entity_Stat stats)
    {
        base.Initialize(weapon, stats);
        Debug.Log("±«√— √ ±‚»≠");
    }

    public override void OnAttackInput()
    {
        base.OnAttackInput();
    }

    public override void PerformAttack()
    {
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
        }
    }
}
