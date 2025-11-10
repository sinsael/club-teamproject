using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatSO", menuName = "Scriptable Objects/WeaponStatSO")]
public class WeaponStatSO : ScriptableObject
{
    [Header("¹«±â Å¸ÀÔ")]
    public WeaponType weaponType;

    [Header("ÃÑ¾Ë")]
    public GameObject bulletPrefab;
    public GameObject muzzleflashPrefab;

    [Header("±âº» ÃÑ±â ½ºÆå")]
    public float Damage;
    public float Range;
    public float Speed;
    public float FireRate;
    public int MaxBullets;
    public float ReloadTime;
    public float FovRange;
    public float FovRadius;
    public float BulletSpeed;
}
