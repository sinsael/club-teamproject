using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BulletUI : MonoBehaviour
{
    [Header("플레이어 무기")]
    [SerializeField] private Weapon playerWeapon;

    [Header("UI 요소")]
    [SerializeField] private Image weaponIconImage;

    [SerializeField] private TextMeshProUGUI ammoText;

    private int currentMaxAmmo;

    private void OnEnable()
    {
        if (playerWeapon != null)
        {
            playerWeapon.OnWeaponEquipped += HandleWeaponEquipped;
            playerWeapon.OnAmmoChanged += HandleAmmoChanged;
        }
    }

    private void OnDisable()
    {
        if (playerWeapon != null)
        {
            playerWeapon.OnWeaponEquipped -= HandleWeaponEquipped;
            playerWeapon.OnAmmoChanged -= HandleAmmoChanged;
        }
    }

    private void HandleWeaponEquipped(WeaponStatSO newWeaponStats, int currentAmmo)
    {
        currentMaxAmmo = (int)newWeaponStats.MaxBullets;
        ammoText.text = $"{currentAmmo}/{currentMaxAmmo}";
        if (newWeaponStats.weaponIcon != null)
        {
            weaponIconImage.sprite = newWeaponStats?.weaponIcon;

        }
    }

    private void HandleAmmoChanged(int newAmmoCount)
    {
        ammoText.text = $"{newAmmoCount}/{currentMaxAmmo}";
    }
}

