using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] private float fadeTime = 0.5f; // 0.5초 동안 서서히 사라짐
    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        // fadeTime 후에 오브젝트 파괴 (안전장치)
        Destroy(gameObject, fadeTime);
    }

    void Update()
    {
        if (sr != null)
        {
            // 알파값(투명도)을 천천히 줄임
            Color color = sr.color;
            color.a -= (Time.deltaTime / fadeTime);
            sr.color = color;
        }
    }
}