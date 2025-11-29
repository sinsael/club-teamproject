using System.Collections.Generic;
using UnityEngine;

public class WeaponHandler : MonoBehaviour
{
    public WeaponStatSO pistolStats;
    public WeaponStatSO rifleStats;
    public WeaponStatSO shotgunStats;

    private Weapon weapon;
    private WeaponStatSO currentWeaponStats;
    private Dictionary<WeaponStatSO, IWeaponAction> Wgun;
    private Dictionary<WeaponStatSO, int> ammoCache;

    void Awake()
    {
        weapon = GetComponent<Weapon>();

        Wgun = new Dictionary<WeaponStatSO, IWeaponAction>();
        ammoCache = new Dictionary<WeaponStatSO, int>();

        // 2. 각 무기 데이터가 "있는 경우에만" 등록하도록 안전장치 추가
        if (pistolStats != null)
        {
            Wgun.Add(pistolStats, new WPistol());
            ammoCache.Add(pistolStats, (int)pistolStats.MaxBullets);
        }

        if (rifleStats != null)
        {
            Wgun.Add(rifleStats, new WRifle());
            ammoCache.Add(rifleStats, (int)rifleStats.MaxBullets);
        }

        if (shotgunStats != null)
        {
            Wgun.Add(shotgunStats, new WShotgun());
            ammoCache.Add(shotgunStats, (int)shotgunStats.MaxBullets);
        }
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
        if (stats == null || stats == currentWeaponStats)
        {
            return;
        }
        if (currentWeaponStats != null)
        {
            // weapon.GetCurrentAmmo()를 호출해서 '줄어든' 총알 수를 가져옵니다.
            ammoCache[currentWeaponStats] = weapon.GetCurrentAmmo();
            Debug.Log($"[저장] {currentWeaponStats.name}의 총알: {weapon.GetCurrentAmmo()}발");
        }
        // 캐시에서 해당 스탯에 맞는 '전략(부품)'을 찾습니다.
        if (Wgun.TryGetValue(stats, out IWeaponAction strategy))
        {
            int ammoToLoad = ammoCache[stats];
            Debug.Log($"===== EquipWeapon 호출 시도: {stats?.name} =====", this);
            // Weapon.cs의 EquipNewWeapon 함수 호출
            weapon.EquipNewWeapon(stats, strategy, ammoToLoad);
            currentWeaponStats = stats;
        }
        else
        {
            Debug.LogError($"{stats.weaponType}에 맞는 전략(Strategy)이 캐시에 없습니다!");
        }
    }
}
