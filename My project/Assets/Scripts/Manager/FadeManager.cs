using UnityEngine;
using System.Collections;
using System;
public class FadeManager : MonoBehaviour
{
    public static FadeManager Instance;

    [Header("설정")]
    [Tooltip("페이드 되는 데 걸리는 시간(초)")]
    public float fadeDuration = 1.0f;

    [Header("참조")]
    public CanvasGroup fadeCanvasGroup;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        fadeCanvasGroup.gameObject.SetActive(false);
    }

    // 페이드 인: 화면이 밝아짐 (검은색 -> 투명)
    [ContextMenu("fade in")]
    public void FadeIn(Action onComplete = null)
    {
        StartCoroutine(FadeRoutine(1, 0, onComplete));
    }

    // 페이드 아웃: 화면이 어두워짐 (투명 -> 검은색)
    [ContextMenu("fade out")]
    public void FadeOut(Action onComplete = null)
    {
        StartCoroutine(FadeRoutine(0, 1, onComplete));
    }

    // 실제 페이드 로직을 처리하는 코루틴
    private IEnumerator FadeRoutine(float startAlpha, float endAlpha, Action onComplete)
    {
        fadeCanvasGroup.gameObject.SetActive(true);

        float elapsedTime = 0f;

        // 페이드 중에는 클릭 입력을 막음
        fadeCanvasGroup.blocksRaycasts = true;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            // Mathf.Lerp를 사용해 부드럽게 값 변경
            fadeCanvasGroup.alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            yield return null;
        }

        // 확실하게 최종 값으로 설정
        fadeCanvasGroup.alpha = endAlpha;

        // 페이드 인이 끝났을 때만 클릭 입력을 다시 허용
        if (endAlpha == 0)
        {
            fadeCanvasGroup.blocksRaycasts = false;
            fadeCanvasGroup.gameObject.SetActive(false);
        }

        if(onComplete != null)
        {
            onComplete.Invoke();
        }
    }
}
