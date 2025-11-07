using System;
using UnityEngine;

[Serializable]
public class WeaponStats
{
    public float Speed;
    public float Range;
    public int Bullets;
    public float Damage;
    public int MaxBullets;
    public float ReloadTime;
}

[CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Objects/WeaponData")]
public class WeaponSO : ScriptableObject
{
    public WeaponType weaponType;
    public WeaponStats stats;
}
