using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public WeaponStatSO pistolStats;
    public WeaponStatSO rifleStats;
    public WeaponStatSO shotgunStats;

    private Weapon weapon;
    private Entity_Stat entityStat;

    private Dictionary<WeaponStatSO, IWeaponStrategy> Wgun;

    void Awake()
    {
        weapon = GetComponent<Weapon>();
        entityStat = GetComponent<Entity_Stat>();
        Wgun = new Dictionary<WeaponStatSO, IWeaponStrategy>
        {
           { pistolStats, new WPistol() },
            { rifleStats, new WRifle() },
            { shotgunStats, new WShotgun() }
        };
    }

    private void Start()
    {
        if (weapon != null && pistolStats != null)
        {
            EquipWeapon(pistolStats);
        }
    }

    public void EquipPistol()
    {
        EquipWeapon(pistolStats);
    }

    public void EquipRifle()
    {
        EquipWeapon(rifleStats);
    }

    public void EquipShotgun()
    {
        EquipWeapon(shotgunStats);
    }

    private void EquipWeapon(WeaponStatSO stats)
    {
        if (stats == null) return;

        // 캐시에서 해당 스탯에 맞는 '전략(부품)'을 찾습니다.
        if (Wgun.TryGetValue(stats, out IWeaponStrategy strategy))
        {
            // Weapon.cs의 EquipNewWeapon 함수 호출
            weapon.EquipNewWeapon(stats, strategy);
        }
        else
        {
            Debug.LogError($"{stats.weaponType}에 맞는 전략(Strategy)이 캐시에 없습니다!");
        }
    }
}
