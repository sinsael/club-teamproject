using UnityEngine;
using UnityEngine.SceneManagement; // 필수 네임스페이스

[RequireComponent(typeof(Canvas))]
public class AutoCanvasCamera : MonoBehaviour
{
    private Canvas canvas;

    private void Awake()
    {
        canvas = GetComponent<Canvas>();

        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        // 씬이 로드될 때마다 실행될 함수 등록
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // 이벤트 해제 (메모리 누수 방지)
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 씬 로딩이 끝나면 호출되는 함수
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 1. 현재 씬의 메인 카메라를 찾아서 캔버스의 worldCamera에 할당
        // (단, 씬의 카메라 태그가 "MainCamera"여야 함)
        canvas.worldCamera = Camera.main;

        // 혹시 카메라가 없거나 태그가 안 되어있을 경우를 대비한 안전장치
        if (canvas.worldCamera == null)
        {
            Debug.LogWarning("메인 카메라를 찾을 수 없습니다! 카메라 태그가 'MainCamera'인지 확인하세요.");
        }
    }
}