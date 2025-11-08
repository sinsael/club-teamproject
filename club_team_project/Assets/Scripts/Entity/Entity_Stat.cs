using UnityEngine;

public class Entity_Stat : MonoBehaviour
{
    public StatSO defaultStatSetup;
    public WeaponStatSO defaultStatWeaponSetup;

    public HealthStat healthStat;
    public SweaponStats weaponstat;
    public void Awake()
    {
        healthStat = new HealthStat
        {
            maxHealth = new Stat(),
            healthRegen = new Stat(),
        };

        weaponstat = new SweaponStats
        {
            Speed = new Stat(),
            Range = new Stat(),
            Damage = new Stat(),
            MaxBullets = new Stat(),
            ReloadTime = new Stat(),
            fov = new Stat(),
            radius = new Stat(),
        };
        ApplyDefaultStatSetup();
    }

    public float GetMaxHealth()
    {
        return healthStat.maxHealth.GetValue();
    }

    public float GetDamage()
    {
        return weaponstat.Damage.GetValue();
    }

    public float GetSpeed()
    {

        return weaponstat.Speed.GetValue();
    }

    public float GetRange()
    {
        return weaponstat.Range.GetValue();
    }

    public float GetMaxBullets()
    {
        return weaponstat.MaxBullets.GetValue();
    }

    public float GetReloadTime()
    {
        return weaponstat.ReloadTime.GetValue();
    }

    public float GetFov()
    {
        return weaponstat.fov.GetValue();
    }
    public float GetRadius()
    {
        return weaponstat.radius.GetValue();
    }

    public void EquipNewWeapon(WeaponStatSO newWeaponStats)
    {
        if (newWeaponStats == null)
        {
            Debug.LogWarning("장착할 무기 정보가 없습니다! (아마도 '맨손' 상태)");
            // (선택) 맨손일 때의 기본값을 설정할 수 있습니다.
            weaponstat.Damage.SetBaseValue(1); // 예: 맨손 데미지 1
            weaponstat.Range.SetBaseValue(1.5f);
            weaponstat.Speed.SetBaseValue(1f);
            weaponstat.MaxBullets.SetBaseValue(0);
            weaponstat.ReloadTime.SetBaseValue(0);
            weaponstat.fov.SetBaseValue(0);
            weaponstat.radius.SetBaseValue(0);
            return;
        }

        weaponstat.Damage.SetBaseValue(newWeaponStats.Damage);
        weaponstat.Speed.SetBaseValue(newWeaponStats.Speed);
        weaponstat.Range.SetBaseValue(newWeaponStats.Range);
        weaponstat.MaxBullets.SetBaseValue(newWeaponStats.MaxBullets);
        weaponstat.ReloadTime.SetBaseValue(newWeaponStats.ReloadTime);
        weaponstat.fov.SetBaseValue(newWeaponStats.fov);
        weaponstat.radius.SetBaseValue(newWeaponStats.radius);
    }

    public Stat GetStatByType(StatType type)
    {
        switch (type)
        {
            case StatType.MaxHealth: return healthStat.maxHealth;
            case StatType.HealthRegen: return healthStat.healthRegen;
            case StatType.Damage: return weaponstat.Damage;
            case StatType.Speed: return weaponstat.Speed;
            case StatType.range: return weaponstat.Range;
            case StatType.MaxBullets: return weaponstat.MaxBullets;
            case StatType.ReloadTime: return weaponstat.ReloadTime;
            case StatType.fov: return weaponstat.fov;
            case StatType.radius: return weaponstat.radius;

            default:
                Debug.LogWarning($"StatType {type} not implemented yet.");
                return null;
        }
    }

    [ContextMenu("Update Default Stat Setup")]
    public void ApplyDefaultStatSetup()
    {
        if (defaultStatSetup == null)
        {
            Debug.Log("No default stat setup assigned");
            return;
        }

        healthStat.maxHealth.SetBaseValue(defaultStatSetup.maxHealth);
        healthStat.healthRegen.SetBaseValue(defaultStatSetup.healthRegen);
        weaponstat.Damage.SetBaseValue(defaultStatWeaponSetup.Damage);
        weaponstat.Speed.SetBaseValue(defaultStatWeaponSetup.Speed);
        weaponstat.Range.SetBaseValue(defaultStatWeaponSetup.Range);
        weaponstat.MaxBullets.SetBaseValue(defaultStatWeaponSetup.MaxBullets);
        weaponstat.ReloadTime.SetBaseValue(defaultStatWeaponSetup.ReloadTime);
        weaponstat.fov.SetBaseValue(defaultStatWeaponSetup.fov);
        weaponstat.radius.SetBaseValue(defaultStatWeaponSetup.radius);

    }
}
