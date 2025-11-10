using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float damage;
    public float bulletSpeed;

    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
       if (rb != null)
        {
            rb.linearVelocity = transform.up * bulletSpeed;
        }

       Destroy(gameObject, 5f); // 5ÃÊ ÈÄ¿¡ ÃÑ¾Ë ÆÄ±«
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        ITakeDamage takeDamage = collision.GetComponent<ITakeDamage>();
        if (takeDamage != null)
        {
            bool targetGotHit = takeDamage.TakeDamage(damage);
        }
        Destroy(gameObject);
    }
}