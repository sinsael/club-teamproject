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
        SoundManager.Instance.PlaySFX(SoundManager.Instance.pistolClip);
        GameObject bullet = weapon.bulletPrefab;
        GameObject bulletObj = Object.Instantiate(bullet, weapon.firePoint.position, weapon.firePoint.rotation);
        if (weapon.muzzleflashPrefab != null)
        {
            GameObject muzzleflashObj = Object.Instantiate(weapon.muzzleflashPrefab, weapon.firePoint.position, weapon.firePoint.rotation);

            muzzleflashObj.transform.localScale = weapon.muzzleflashPrefab.transform.localScale;

            muzzleflashObj.transform.SetParent(weapon.firePoint, true);

            muzzleflashObj.transform.localPosition = Vector3.zero;
            muzzleflashObj.transform.localRotation = Quaternion.identity;
        }

        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        if (bulletScript != null)
        {
            bulletScript.damage = stats.GetDamage();
            bulletScript.bulletSpeed = stats.GetBulletSpeed();
            bulletScript.maxRange = stats.GetRange();

            bulletScript.weaponType = WeaponType.pistol;
        }
    }
}
