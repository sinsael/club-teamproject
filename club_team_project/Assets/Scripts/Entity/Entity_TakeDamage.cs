using UnityEngine;

public class Entity_TakeDamage : MonoBehaviour
{

    [Header("타깃 감지")]
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask WhatisTarget;


    public void PerformFanAttack(float range, float angle, float damage)
    {
        // 1. 먼저, 반경(range) 내의 모든 적을 가져옵니다.
        Collider2D[] targetsInRadius = Physics2D.OverlapCircleAll(attackPoint.position, range, WhatisTarget);

        Vector2 fireDirection = attackPoint.up; // 총구의 정면 방향
        float halfFov = angle / 2f;

        foreach (Collider2D target in targetsInRadius)
        {
            if (target.gameObject == this.gameObject) continue;

            Vector2 directionToTarget = target.transform.position - attackPoint.position;

            // 2. 타겟이 부채꼴 각도(angle) 안에 있는지 확인
            float angleToTarget = Vector2.Angle(fireDirection, directionToTarget);

            if (angleToTarget <= halfFov)
            {
                // 3. 각도 안에 있는 적에게만 '전달받은' damage를 줌
                if (target.TryGetComponent<ITakeDamage>(out var takeDamage))
                {
                    takeDamage.TakeDamage(damage);
                }
            }
        }
    }

    public void PerformCircleAttack(float range, float damage)
    {
        Collider2D[] targetsInRadius = Physics2D.OverlapCircleAll(attackPoint.position, range, WhatisTarget);

        foreach (var target in targetsInRadius)
        {
            if (target.TryGetComponent<ITakeDamage>(out var takeDamage))
            {
                takeDamage.TakeDamage(damage);
            }
        }
    }
}
