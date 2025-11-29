using UnityEngine;

public class ShotGunDoor : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 1. 부딪힌게 총알인지 확인
        Bullet bullet = collision.GetComponent<Bullet>();

        if (bullet != null)
        {
            // 2. 그 총알이 '샷건'인지 확인
            if (bullet.weaponType == WeaponType.shotgun)
            {
                BreakDoor(); // 문 파괴
                Destroy(bullet.gameObject); // 총알도 같이 없애주기
            }
            else
            {
                // 샷건이 아니면? (예: 팅겨나가는 소리 재생 or 무반응)
                Debug.Log("이 문은 샷건으로만 부술 수 있습니다.");
                Destroy(bullet.gameObject); // 총알은 없애줌 (선택사항)
            }
        }
    }

    private void BreakDoor()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.doorBreachClip); 

        Destroy(gameObject); // 문 삭제
    }
}
