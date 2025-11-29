using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    GameStart,
    GamePause,
    GamePlay,
    GameOver,
    GameClear
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    // 게임 전체에서 공유되는 변수 (처음 켰을 때만 true)
    public static bool isfirsteLoad = true;

    public GameState currentGameState { get; private set; }
    public GameObject GameStartUI;
    public GameObject GamePauseUI;

    // 플레이어 조작을 멈추기 위한 참조
    public MonoBehaviour playerController;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Update()
    {
        // ESC 키로 일시정지/해제
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (currentGameState == GameState.GamePlay)
            {
                ChangeGameState(GameState.GamePause);
            }
            else if (currentGameState == GameState.GamePause)
            {
                ChangeGameState(GameState.GamePlay);
            }
        }

        // 인질을 다 구했으면 클리어 (HostageManager가 있을 때만)
        if (HostageManager.instance != null && HostageManager.instance.AllSaveHostage)
        {
            ChangeGameState(GameState.GameClear);
        }

        // (테스트용) 스페이스바로 강제 클리어
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ChangeGameState(GameState.GameClear);
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // 1. 플레이어 찾기 (태그 이용)
        if (playerController == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                // 플레이어 스크립트 이름을 정확히 안다면 MonoBehaviour 대신 그 이름(예: PlayerController)을 쓰세요
                playerController = playerObj.GetComponent<MonoBehaviour>();
        }

        // 2. UI 오브젝트 찾기 (Canvas 자식에서 찾기)
        GameObject canvas = GameObject.Find("Canvas");
        if (canvas != null)
        {
            Transform startUITr = canvas.transform.Find("GameStart");
            if (startUITr != null) GameStartUI = startUITr.gameObject;

            Transform pauseUITr = canvas.transform.Find("Pause");
            if (pauseUITr != null) GamePauseUI = pauseUITr.gameObject;
        }
        else
        {
            // 캔버스 못 찾았으면 그냥 이름으로라도 찾기 시도
            if (GameStartUI == null) GameStartUI = GameObject.Find("GameStart");
            if (GamePauseUI == null) GamePauseUI = GameObject.Find("Pause");
        }

        // 3. 씬 로드 시 페이드 인 연출 시작
        Time.timeScale = 1f; // 페이드가 보이도록 시간 흐르게 설정


        FadeManager.Instance.FadeIn();
        // 페이드가 끝난 후 실행될 로직
        if (isfirsteLoad)
        {
            ChangeGameState(GameState.GameStart);
        }
        else
        {
            ChangeGameState(GameState.GamePlay);
        }
    }

    public void OnPlayerDead()
    {
        ChangeGameState(GameState.GameOver);
    }

    public void ChangeGameState(GameState newState)
    {
        currentGameState = newState;

        CloseAllUI();

        switch (currentGameState)
        {
            case GameState.GameStart:
                if (GameStartUI != null) GameStartUI.SetActive(true);
                if (playerController != null) playerController.enabled = false; // 플레이어 조작 불가
                Time.timeScale = 0f; // 시간 정지
                break;

            case GameState.GamePause:
                if (GamePauseUI != null) GamePauseUI.SetActive(true);
                Time.timeScale = 0f; // 시간 정지
                break;

            case GameState.GamePlay:
                if (playerController != null) playerController.enabled = true; // 플레이어 조작 가능
                Time.timeScale = 1f; // 시간 정상화
                isfirsteLoad = false; // 이제 처음 아님
                break;

            case GameState.GameOver:
                Time.timeScale = 1f;
                if (FadeManager.Instance != null)
                {
                    FadeManager.Instance.FadeOut(() =>
                    {
                        // 현재 씬 재시작
                        isfirsteLoad = false;
                        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    });
                }
                else SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                break;

            case GameState.GameClear:
                Time.timeScale = 1f;
                if (FadeManager.Instance != null)
                {
                    FadeManager.Instance.FadeOut(() =>
                    {
                        // 다음 씬으로 이동 로직
                        int currentIndex = SceneManager.GetActiveScene().buildIndex;
                        int nextIndex = currentIndex + 1;

                        // 인질 구조 상태 초기화 (필요하다면)
                        if (HostageManager.instance != null) HostageManager.instance.AllSaveHostage = false;

                        if (nextIndex < SceneManager.sceneCountInBuildSettings)
                        {
                            SceneManager.LoadScene(nextIndex);
                        }
                        else
                        {
                            // 마지막 씬이면 처음으로
                            SceneManager.LoadScene(0);
                        }
                    });
                }
                break;
        }
    }

    private void CloseAllUI()
    {
        if (GameStartUI != null) GameStartUI.SetActive(false);
        if (GamePauseUI != null) GamePauseUI.SetActive(false);
    }
}