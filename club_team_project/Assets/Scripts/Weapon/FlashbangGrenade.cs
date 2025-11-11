using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlashbangGrenade : MonoBehaviour
{
    [SerializeField]private float explosionRadius = 5f;
    [SerializeField]private float stunDuration = 3f;
    [SerializeField]private float throwSpeed;
    [SerializeField]private LayerMask whatIsTarget;

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        rb.linearVelocity = transform.right * throwSpeed;
        // ÀÏÁ¤ ½Ã°£ ÈÄ Æø¹ß
        Invoke(nameof(Explode), 2f);
    }

    public void Initialize(float radius, float duraiton, float speed, LayerMask targetMask)
    {
        this.explosionRadius = radius;
        this.stunDuration = duraiton;
        this.throwSpeed = speed;
        this.whatIsTarget = targetMask;
    }

    void Explode()
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(transform.position, explosionRadius, whatIsTarget);
        foreach (Collider2D target in targets)
        {
            ITakeStun stunable = target.GetComponent<ITakeStun>();
            if (stunable != null)
            {
                stunable.OnStun(stunDuration);
            }
        }

        // ¼¶±¤Åº ÆÄÆ¼Å¬ ¿©±â

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
