using UnityEngine;

public class MuzzleFlashEffect : MonoBehaviour
{
    public float lifetime = 0.1f; // 0.1초

    void Start()
    {
        // 생성되자마자 스스로 'lifetime' 초 뒤에 파괴되도록 예약
        Destroy(gameObject, lifetime);
    }
}
