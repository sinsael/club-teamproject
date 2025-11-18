using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class FlashbangGrenade : MonoBehaviour
{
    [SerializeField]private float explosionRadius = 5f;
    [SerializeField]private float stunDuration = 3f;
    [SerializeField]private float throwSpeed;
    [SerializeField]private LayerMask whatIsTarget;
    [SerializeField]private GameObject flash;


    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Start()
    {
        rb.linearVelocity = transform.up * throwSpeed;
        // 일정 시간 후 폭발
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

        Debug.Log($"[섬광탄] 폭발! 감지된 대상 수: {targets.Length}명"); // 1. 감지 자체가 되는지 확인

        foreach (Collider2D target in targets)
        {
            ITakeStun stunable = target.GetComponent<ITakeStun>();
            if (stunable != null)
            {
                Debug.Log($"[섬광탄] {target.name}에게 스턴 시전!"); // 2. 인터페이스가 있는지 확인
                stunable.OnStun(stunDuration);
            }
            else
            {
                Debug.LogWarning($"[섬광탄] {target.name}을 찾았지만, ITakeStun이 없습니다! (스크립트 선언부 확인필요)");
            }
        }

        // 섬광탄 파티클 여기
        Instantiate(flash, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }

}
