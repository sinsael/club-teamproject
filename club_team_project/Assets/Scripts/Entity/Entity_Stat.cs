using UnityEngine;

public class Entity_Stat : MonoBehaviour
{
    public StatSO defaultStatSetup;
    private Stat stat;

    public HealthStat healthStat;


    public void Awake()
    {
        stat = GetComponent<Stat>();
        healthStat = new HealthStat
        {
            maxHealth = gameObject.AddComponent<Stat>(),
            healthRegen = gameObject.AddComponent<Stat>()
        };
        ApplyDefaultStatSetup();
    }

    public float GetMaxHealth()
    {
        float baseMaxHealth = healthStat.maxHealth.GetValue();
        float finalMaxHealth = baseMaxHealth;

        return finalMaxHealth;
    }

    public Stat GetStatByType(StatType type)
    {
        switch (type)
        {
            case StatType.MaxHealth: return healthStat.maxHealth;
            case StatType.HealthRegen: return healthStat.healthRegen;

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

    }
}
