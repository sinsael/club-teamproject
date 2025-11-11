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
    [SerializeField] private float alertThreshold = 0.5f;
    float playerAlertfov;

    [Header("사격 범위 설정")]
    [SerializeField] private Transform ShootRangeCheck;
    [SerializeField] private float ShootRangeCheckRadius;

    [Header("필터 및 레이어")]
    [SerializeField] private LayerMask WhatIsPlayer;
    [SerializeField] private LayerMask obstacleLayerMask;

    private Collider2D[] PlayerAlertcols = new Collider2D[5];
    private Collider2D[] shootCheckBuffer = new Collider2D[5];
    private ContactFilter2D playerFilter;

    void Awake()
    {
        playerFilter = new ContactFilter2D();
        playerFilter.SetLayerMask(WhatIsPlayer);
    }

    void Start()
    {
         alertThreshold = Mathf.Cos(playerAlertfov / 2 * Mathf.Deg2Rad);
        currentState = EnemyState.Idle;
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
        ChasePlayer();

        if (PlayerInShootRange())
        {
            currentState = EnemyState.Shooting;

            StopMovement();
        }
    }

    private void HandleShootingState()
    {
        Debug.Log("쏜다");
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
        // 추적 시스템
        Debug.Log("추적");
    }

    private void StopMovement()
    {
        Debug.Log("정지!");
    }

    private void LookAtPlayer()
    {
        // 플레이어 바라보기
    }

    private void Shoot()
    {
        Debug.Log("사격!");
    }

    public void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(ShootRangeCheck.position, ShootRangeCheckRadius);

        Vector2 myUp = transform.rotation * Vector3.up;
        Vector2 startDirection = Quaternion.Euler(0, 0, -playerAlertfov / 2) * myUp;

        Handles.DrawSolidArc(transform.position, Vector3.forward, startDirection, playerAlertfov, playerAlertradius);
    }
}