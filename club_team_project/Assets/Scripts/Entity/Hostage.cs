using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Hostage : Entity, IInteraction
{
    int hit;

    public void OnSelect()
    {

    }
    public void OnDeselect()
    {
      
    }

    public void OnInteract()
    {
        HostageManager.instance.CollectHostage();
        Destroy(gameObject);
    }

    // 1. 물리 충돌 (Is Trigger 체크 해제됨)
    public void OnCollisionEnter2D(Collision2D collision)
    {
        HandleHit(collision.gameObject);
    }

    // 2. 트리거 충돌 (Is Trigger 체크됨)
    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleHit(collision.gameObject);
    }

    // 충돌 처리 로직 하나로 통일
    void HandleHit(GameObject target)
    {
        // 디버깅용 로그: 무엇과 부딪혔는지 콘솔에 출력
        // Debug.Log($"충돌 감지됨: {target.name}, 레이어: {LayerMask.LayerToName(target.layer)}");

        if (target.layer == LayerMask.NameToLayer("PlayerBullet"))
        {
            hit++;
            Debug.Log($"인질 피격! 현재 맞은 횟수: {hit}"); // 로그 확인

            // 총알 제거 (총알에 별도 파괴 로직이 있다면 생략 가능)
            Destroy(target);

            if (hit >= 2)
            {
                EntityDeath();
            }
        }
    }

    public override void EntityDeath()
    {
        base.EntityDeath();

        HostageManager.instance.HostageDied();
        Destroy(gameObject);
    }
}
