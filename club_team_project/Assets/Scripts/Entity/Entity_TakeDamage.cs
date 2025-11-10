using UnityEngine;

public class Entity_TakeDamage : MonoBehaviour
{
    private Weapon weapon;

    [Header("Å¸±ê °¨Áö")]
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius;
    [SerializeField] private LayerMask whatIsTarget;

    private void Awake()
    {
        weapon = GetComponent<Weapon>();
    }

    public void PerformAttakck()
    {
        foreach (var target in GetDetectedColliders())
        {
            ITakeDamage takeDamage = target.GetComponent<ITakeDamage>();

            if (takeDamage == null)
                continue;

            float damage = weapon.entitystat.GetDamage();

            bool targetGotHit = takeDamage.TakeDamage(damage);
        }
    }

    protected Collider2D[] GetDetectedColliders()
    {
        return Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
