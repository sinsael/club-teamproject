using UnityEngine;

public class Entity_Stat : MonoBehaviour
{
    public StatSO defaultStatSetup;
    public WeaponStatSO defaultStatWeaponSetup;
    public FlashStatSO defaultFlashStatSetup;

    public HealthStat healthStat = new HealthStat
    {
        maxHealth = new Stat(),
        healthRegen = new Stat()
    };
    public SweaponStats weaponstat = new SweaponStats
    {
        Speed = new Stat(),
        Range = new Stat(),
        Damage = new Stat(),
        MaxBullets = new Stat(),
        ReloadTime = new Stat(),
        FovRange = new Stat(),
        FovRadius = new Stat(),
        FireRate = new Stat(),
        BulletSpeed = new Stat(),
        ShotgunRadius = new Stat(),
        ShotgunRange = new Stat(),
    };

    public FlashStat flashStat = new FlashStat
    {
        FlashbangDuration = new Stat(),
        FlashbangRadius = new Stat(),
        FlashbangSpeed = new Stat(),
    };



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

    public int GetMaxBullets()
    {
        return (int)weaponstat.MaxBullets.GetValue();
    }

    public float GetReloadTime()
    {
        return weaponstat.ReloadTime.GetValue();
    }

    public float GetFireRate()
    {
        return weaponstat.FireRate.GetValue();
    }

    public float GetFovRange()
    {
        if (weaponstat.FovRange == null)
        {
            return 0f;
        }
        return weaponstat.FovRadius.GetValue();
    }
    public float GetFovRadius()
    {
        if (weaponstat.FovRadius == null)
        {
            return 0f;
        }
        return weaponstat.FovRadius.GetValue();
    }

    public float GetShotgunRadius()
    {
        return weaponstat.ShotgunRadius.GetValue();
    }
    public float GetShotgunRange()
    {
        return weaponstat.ShotgunRange.GetValue();
    }

    public float GetBulletSpeed()
    {
        return weaponstat.BulletSpeed.GetValue();
    }

    public float GetFlashbangDuration()
    {
        return flashStat.FlashbangDuration.GetValue();
    }

    public float GetFlashbangRadius()
    {
        return flashStat.FlashbangRadius.GetValue();
    }

    public float GetFlashbangSpeed()
    {
        return flashStat.FlashbangSpeed.GetValue();
    }

    public void EquipNewWeapon(WeaponStatSO newWeaponStats)
    {
        defaultStatWeaponSetup = newWeaponStats;
        if (newWeaponStats == null)
        {
            Debug.LogWarning("장착할 무기 정보가 없습니다! (아마도 '맨손' 상태)");
            // (선택) 맨손일 때의 기본값을 설정할 수 있습니다.
            weaponstat.Damage.SetBaseValue(1); // 예: 맨손 데미지 1
            weaponstat.Range.SetBaseValue(1.5f);
            weaponstat.Speed.SetBaseValue(1f);
            weaponstat.MaxBullets.SetBaseValue(0);
            weaponstat.ReloadTime.SetBaseValue(0);
            weaponstat.FovRange.SetBaseValue(0);
            weaponstat.FovRadius.SetBaseValue(0);
            weaponstat.FireRate.SetBaseValue(0);
            weaponstat.BulletSpeed.SetBaseValue(0);
            weaponstat.ShotgunRange.SetBaseValue(0);
            weaponstat.ShotgunRadius.SetBaseValue(0);
            return;
        }

        weaponstat.Damage.SetBaseValue(newWeaponStats.Damage);
        weaponstat.Speed.SetBaseValue(newWeaponStats.Speed);
        weaponstat.Range.SetBaseValue(newWeaponStats.Range);
        weaponstat.MaxBullets.SetBaseValue(newWeaponStats.MaxBullets);
        weaponstat.ReloadTime.SetBaseValue(newWeaponStats.ReloadTime);
        weaponstat.FovRange.SetBaseValue(newWeaponStats.FovRange);
        weaponstat.FovRadius.SetBaseValue(newWeaponStats.FovRadius);
        weaponstat.FireRate.SetBaseValue(newWeaponStats.FireRate);
        weaponstat.BulletSpeed.SetBaseValue(newWeaponStats.BulletSpeed);
        weaponstat.ShotgunRange.SetBaseValue(newWeaponStats.ShotgunRange);
        weaponstat.ShotgunRadius.SetBaseValue(newWeaponStats.ShotgunRadius);
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
            case StatType.FovRange: return weaponstat.FovRange;
            case StatType.FovRadius: return weaponstat.FovRadius;
            case StatType.fireRate: return weaponstat.FireRate;
            case StatType.BulletSpeed: return weaponstat.BulletSpeed;
            case StatType.ShotgunRange: return weaponstat.ShotgunRange;
            case StatType.ShotgunRadius: return weaponstat.ShotgunRadius;
            case StatType.FlashbangDuration: return flashStat.FlashbangDuration;
            case StatType.FlashbangRadius: return flashStat.FlashbangRadius;
            case StatType.FlashbangSpeed: return flashStat.FlashbangSpeed;
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
        weaponstat.FovRange.SetBaseValue(defaultStatWeaponSetup.FovRange);
        weaponstat.FovRadius.SetBaseValue(defaultStatWeaponSetup.FovRadius);
        weaponstat.FireRate.SetBaseValue(defaultStatWeaponSetup.FireRate);
        weaponstat.BulletSpeed.SetBaseValue(defaultStatWeaponSetup.BulletSpeed);
        weaponstat.ShotgunRange.SetBaseValue(defaultStatWeaponSetup.ShotgunRange);
        weaponstat.ShotgunRadius.SetBaseValue(defaultStatWeaponSetup.ShotgunRadius);
        flashStat.FlashbangDuration.SetBaseValue(defaultFlashStatSetup.FlashbangDuration);
        flashStat.FlashbangRadius.SetBaseValue(defaultFlashStatSetup.FlashbangRadius);
        flashStat.FlashbangSpeed.SetBaseValue(defaultFlashStatSetup.FlashSpeed);
    }
}
