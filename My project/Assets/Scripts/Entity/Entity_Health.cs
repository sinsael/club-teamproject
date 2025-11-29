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

    [SerializeField] private float damageRegenDelay = 3f;

    private float lastDamageTime;
    protected void Awake()
    {
        entity = GetComponentInParent<Entity>();
        entitystat = GetComponentInParent<Entity_Stat>();

        if (entitystat == null)
        {
            return;
        }

        if (entitystat == null)
        {
            return;
        }

        isDead = false;

        InvokeRepeating(nameof(RegenerateHealth), 0, regenInterval);
    }

    public void Start()
    {
        currentHealth = entitystat.healthStat.maxHealth.GetValue();

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

        lastDamageTime = Time.time;

        if (currentHealth <= 0)
            Die();
    }

    private void RegenerateHealth()
    {
        if (canRegenerateHealth == false)
            return;

        if (Time.time < lastDamageTime + damageRegenDelay)
        {
            return;
        }

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
        // [수정 2] entity가 연결되어 있는지 확인하고 호출
        if (entity != null)
        {
            entity.EntityDeath();
        }
    }
}
