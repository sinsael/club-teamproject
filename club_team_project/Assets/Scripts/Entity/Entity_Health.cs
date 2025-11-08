using UnityEditor;
using UnityEngine;

public class Entity_Health : MonoBehaviour, ITakeDamage
{
    private Entity entity;
    private Entity_Stat entitystat;

    [SerializeField] protected float currentHealth;
    [SerializeField] protected bool isDead;
    [Header("Health regen")]
    [SerializeField] private float regenInterval = 1;
    [SerializeField] private bool canRegenerateHealth = true;

    protected void Awake()
    {
        entity = GetComponent<Entity>();
        entitystat = GetComponent<Entity_Stat>();

        if (entitystat == null)
        {
            Debug.LogError($"[Entity_Health] {name} 오브젝트에 Entity_Stat이 없습니다!", this);
            return;
        }

        isDead = false;

        InvokeRepeating(nameof(RegenerateHealth), 0, regenInterval);
    }

    public void Start()
    {
        currentHealth = entitystat.GetMaxHealth();

    }

    public bool TakeDamage(float damage)
    {
        if (isDead)
            return false;

        ReduceHealth(damage);

        return true;
    }

    public void ReduceHealth(float damage)
    {
        currentHealth = currentHealth - damage;

        if (currentHealth <= 0)
            Die();
    }

    private void RegenerateHealth()
    {
        if (canRegenerateHealth == false)
            return;

        float regenAmount = entitystat.healthStat.healthRegen.GetValue();
        IncreaseHealth(regenAmount);
    }

    public void IncreaseHealth(float healAmount)
    {
        if (isDead)
            return;

        float newHealth = currentHealth + healAmount;
        float maxHealth = entitystat.GetMaxHealth();

        currentHealth = Mathf.Min(newHealth, maxHealth);
    }

    protected virtual void Die()
    {
        isDead = true;
        entity?.EntityDeath();
    }
}
