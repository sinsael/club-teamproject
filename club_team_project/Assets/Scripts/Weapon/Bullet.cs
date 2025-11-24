using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Header("Bullet Settings")]
    public float damage;
    public float bulletSpeed;
    public float maxRange;

    public WeaponType weaponType;

    Rigidbody2D rb;
    Collider2D col; // 컬라이더도 캐시해둡니다.
    SpriteRenderer sprite; // 총알 그래픽 (SpriteRenderer라고 가정)

    bool hasHit = false; // 중복 충돌 방지 플래그
    Vector2 StartPosition;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        sprite = GetComponent<SpriteRenderer>(); // 총알 그래픽
    }

    void Start()
    {
        StartPosition = transform.position;

        if (rb != null)
        {
            rb.linearVelocity = transform.up * bulletSpeed;
        }

        Destroy(gameObject, 5f); // 5초 후에 총알 파괴 (최대 사거리)
    }

    private void Update()
    {
        // 1. 이미 충돌했으면, 아무것도 하지 않음
        if (hasHit) return;

        // 2. [수정] 사거리(maxRange)가 0 이하로 설정된 경우,
        //    사거리 검사 자체를 '무시'하고 Update를 종료합니다.
        //    (즉, 5초짜리 Destroy 타이머가 작동할 때까지 무한정 날아갑니다)
        if (maxRange <= 0) return;

        // 3. (maxRange가 0보다 클 때만) 사거리 검사를 수행
        float distanceTraveled = Vector2.Distance(StartPosition, transform.position);

        // 4. 이동한 거리가 설정된 최대 사거리(maxRange)를 넘었다면
        if (distanceTraveled >= maxRange)
        {
            SelfDestruct();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (hasHit) return;

        ITakeDamage takeDamage = collision.GetComponent<ITakeDamage>();
        if (takeDamage != null)
        {
            takeDamage.TakeDamage(damage);
        }

        // 파괴 로직을 별도 함수로 분리
        SelfDestruct();
    }

    private void SelfDestruct()
    {
        // 1. 충돌 플래그 ON (중복 실행 방지)
        hasHit = true;

        // 2. 물리적 움직임과 속도 제거
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero;
        }

        // 3. 추가 충돌이 일어나지 않도록 컬라이더 비활성화
        if (col != null)
        {
            col.enabled = false;
        }

        // 4. 총알의 그래픽(스프라이트)만 숨김
        if (sprite != null)
        {
            sprite.enabled = false;
        }

        // 5. 트레일 컴포넌트를 찾음
        TrailRenderer trail = GetComponentInChildren<TrailRenderer>();
        float trailFadeTime = 0f;

        if (trail != null)
        {
            trailFadeTime = trail.time;
        }

        // 6. 트레일이 사라질 시간(trailFadeTime) 후에 파괴 예약
        Destroy(gameObject, trailFadeTime);
    }
}