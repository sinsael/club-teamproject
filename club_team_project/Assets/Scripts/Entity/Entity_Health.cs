using UnityEditor;
using UnityEngine;

public class Entity_Helth : MonoBehaviour, ITakeDamage
{
    private Entity entity;
    private Weapon Wstat;
    private Entity_Stat entitystat;


    [SerializeField] protected float currentHealth;
    [SerializeField] protected bool isDead;
    [Header("Health regen")]
    [SerializeField] private float regenInterval = 1;
    [SerializeField] private bool canRegenerateHealth = true;

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
        Wstat = GetComponent<Weapon>();
        entitystat = GetComponent<Entity_Stat>();

        if (entitystat == null)
        {
            Debug.LogError($"[Entity_Health] {name} 오브젝트에 Entity_Stat이 없습니다!", this);
            return;
        }

        currentHealth = entitystat.GetMaxHealth();
        isDead = false;

        InvokeRepeating(nameof(RegenerateHealth), 0, regenInterval);
    }

    public bool TakeDamage(float damage, Transform damageDealer)
    {
        if (isDead)
            return false;

        ReduceHealth(Wstat.weaponSO.stats.Damage);

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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
