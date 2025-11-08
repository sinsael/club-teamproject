using UnityEngine;

[CreateAssetMenu(fileName = "WeaponStatSO", menuName = "Scriptable Objects/WeaponStatSO")]
public class WeaponStatSO : ScriptableObject
{
    [Header("¹«±â Å¸ÀÔ")]
    public WeaponType weaponType;

    [Header("±âº» ÃÑ±â ½ºÆå")]
    public float Damage;
    public float Range;
    public float Speed;
    public int MaxBullets;
    public float ReloadTime;
    public float fov;
    public float radius;
}
