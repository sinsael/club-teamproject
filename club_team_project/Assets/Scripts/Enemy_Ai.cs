using UnityEngine;
using UnityEditor;
public class Enemy_Ai : MonoBehaviour
{
    private enum EnemyState
    {
        Idle,
        Chasing,
        Shooting
    }

    private EnemyState currentState;

    [Header("타겟 및 속도")]
    [SerializeField] private Transform playerTarget;
    [SerializeField] private float moveSpeed = 5f;

    [Header("시야각 감지")]
    [SerializeField] private float playerAlertradius;
    [SerializeField]float playerAlertfov;
    private float alertThreshold = 0.5f;

    [Header("사격 범위 설정")]
    [SerializeField] private Transform ShootRangeCheck;
    [SerializeField] private float ShootRangeCheckRadius;

    [Header("필터 및 레이어")]
    [SerializeField] private LayerMask WhatIsPlayer;
    [SerializeField] private LayerMask obstacleLayerMask;

    private Collider2D[] PlayerAlertcols = new Collider2D[5];
    private Collider2D[] shootCheckBuffer = new Collider2D[5];
    private ContactFilter2D playerFilter;

    private Weapon weapon;
    private Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        weapon = GetComponent<Weapon>();

        playerFilter = new ContactFilter2D();
        playerFilter.SetLayerMask(WhatIsPlayer);
    }

    void Start()
    {
         alertThreshold = Mathf.Cos(playerAlertfov / 2 * Mathf.Deg2Rad);
        currentState = EnemyState.Idle;

        WeaponHandler handler = GetComponent<WeaponHandler>();
        if(handler != null)
        {
            handler.EquipRifle();
        }
    }

    void Update()
    {
        if (playerTarget == null) return;

        switch (currentState)
        {
            case EnemyState.Idle:
                HandleIdleState();
                break;
            case EnemyState.Chasing:
                HandleChasingState();
                break;
                
            case EnemyState.Shooting:
                HandleShootingState();
                break;
        }
    }

    private void HandleIdleState()
    {
        if (playerAlert())
        {
            Debug.Log("대기");
            currentState = EnemyState.Chasing;
        }
    }

    private void HandleChasingState()
    {
        Debug.Log("추격");
        LookAtPlayer();
        ChasePlayer();

        if (PlayerInShootRange())
        {
            currentState = EnemyState.Shooting;

            StopMovement();
        }
    }

    private void HandleShootingState()
    {
        LookAtPlayer();

        if (!PlayerInShootRange())
        {
            currentState = EnemyState.Chasing;
        }
        else
        {
            Shoot();
        }
    }

    public bool PlayerInShootRange()
    {
        int hitCount = Physics2D.OverlapCircle(
        ShootRangeCheck.position,
        ShootRangeCheckRadius,
        playerFilter,
        shootCheckBuffer
        );

        if (hitCount == 0)
        {
            return false;
        }

        for (int i = 0; i < hitCount; i++)
        {
            Collider2D playerCollider = shootCheckBuffer[i];

            Vector2 origin = (Vector2)ShootRangeCheck.position;
            Vector2 targetPos = (Vector2)playerCollider.transform.position;
            Vector2 direction = (targetPos - origin).normalized;
            float distanceToPlayer = Vector2.Distance(origin, targetPos);

            RaycastHit2D hit = Physics2D.Raycast(origin, direction, distanceToPlayer, obstacleLayerMask);

            if (hit.collider == null)
            {
                return true;
            }
        }

        return false;
    }
    
    public bool playerAlert()
    {
        int hitCount = Physics2D.OverlapCircle(transform.position, playerAlertradius, playerFilter, PlayerAlertcols);

        if (hitCount == 0)
            return false;

        Vector2 myForward = (Vector2)transform.up;

        for (int i = 0; i < hitCount; i++)
        {
            Collider2D target = PlayerAlertcols[i];

            Vector2 targetDir = (Vector2)target.transform.position - (Vector2)transform.position;
            float distanceToTarget = targetDir.magnitude;


            RaycastHit2D hit = Physics2D.Raycast((Vector2)transform.position, targetDir.normalized, distanceToTarget, obstacleLayerMask);


            if (hit.collider != null)
            {
                continue;
            }

            float dot = Vector2.Dot(myForward, targetDir.normalized);

            if (dot >= alertThreshold)
            {
                return true;
            }
        }

        return false;
    }

    private void ChasePlayer()
    {
        if (playerTarget != null)
        {
            Vector2 targetPos = Vector2.MoveTowards(rb.position, playerTarget.position, moveSpeed * Time.deltaTime);
            rb.MovePosition(targetPos);
        }
    }

    private void StopMovement()
    {
        rb.linearVelocity = Vector2.zero;
    }

    private void LookAtPlayer()
    {
        if (playerTarget != null)
        {
            Vector2 direction = playerTarget.position - transform.position;
            // Atan2를 사용하여 각도 계산 (y, x 순서 주의)
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            // 스프라이트가 위쪽(Up)이 앞이라면 -90도 보정 필요
            rb.rotation = angle - 90f;
        }
    }

    private void Shoot()
    {
        if (weapon != null)
        {
            weapon.HandleAttack();
        }
    }
#if UNITY_EDITOR
    public void OnDrawGizmos()
    {
        // 1. 사격 범위 (파란 원)
        if (ShootRangeCheck != null)
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(ShootRangeCheck.position, ShootRangeCheckRadius);
        }
        if (playerAlertfov > 0 && playerAlertradius > 0)
        {
            // 색상을 살짝 투명한 빨간색으로 설정
            Handles.color = new Color(1f, 0f, 0f, 0.2f);

            Vector2 myUp = transform.rotation * Vector3.up;
            Vector2 startDirection = Quaternion.Euler(0, 0, -playerAlertfov / 2) * myUp;

            Handles.DrawSolidArc(transform.position, Vector3.forward, startDirection, playerAlertfov, playerAlertradius);
        }
    }
#endif
}