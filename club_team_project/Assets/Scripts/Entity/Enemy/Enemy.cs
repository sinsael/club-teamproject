using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : Entity
{
    private Enemy_Ai enemy_Ai;

    private bool isStunned = false;

    public override void Start()
    {
        base.Start();
        enemy_Ai = GetComponent<Enemy_Ai>();
    }

    public override void OnStun(float duration)
    {
        if (isStunned) return; // 이미 기절 중이면 무시 (혹은 시간 갱신 로직 추가 가능)

        StartCoroutine(CoStunRoutine(duration));
    }

    private IEnumerator CoStunRoutine(float duration)
    {
        isStunned = true;
        Debug.Log($"{name} 기절 시작!");
        enemy_Ai.enabled = false;

        // (선택 사항) 물리적으로 밀려나는 것을 방지하고 싶다면 속도 0으로 설정
        if (GetComponent<Rigidbody2D>() != null)
        {
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
        }

        yield return new WaitForSeconds(duration);

        isStunned = false;
        enemy_Ai.enabled = true;
        Debug.Log($"{name} 기절 해제!");
    }

    public override void EntityDeath()
    {
        base.EntityDeath();

        SoundManager.Instance.PlayEnemyDeathSound();
        Destroy(gameObject);
    }
}
