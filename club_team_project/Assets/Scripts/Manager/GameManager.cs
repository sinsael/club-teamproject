using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public enum GameState
{
    GameStart,
    GamePause,
    GamePlay,
    GameOver
}


public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public static bool isfirsteLoad = true;
    public GameState currentGameState { get; private set; }
    public GameObject GameStartUI;
    public GameObject GamePauseUI;


    public void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
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
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentGameState == GameState.GamePlay)
            {
                ChangeGameState(GameState.GamePause);
            }
            else if (currentGameState == GameState.GamePause)
            {
                ChangeGameState(GameState.GamePlay);
            }
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (isfirsteLoad)
        {
            ChangeGameState(GameState.GameStart);

        }
        else
        {
            FadeManager.Instance.FadeIn();
            ChangeGameState(GameState.GamePlay);

        }
    }

    public void OnPlayerDead()
    {
        // 상태는 변경하되 (입력 막기 용도), UI는 띄우지 않음
        ChangeGameState(GameState.GameOver);
    }


    public void ChangeGameState(GameState newState)
    {
        currentGameState = newState;

        CloseAllUI();

        // 2. 상태에 따라 필요한 처리(UI 켜기, 시간 정지 등)를 '한 번만' 수행합니다.
        switch (currentGameState)
        {
            case GameState.GameStart:
                if (GameStartUI != null) GameStartUI.SetActive(true);
                Time.timeScale = 0f; // 시간 정상화
                break;

            case GameState.GamePause:
                if (GamePauseUI != null) GamePauseUI.SetActive(true);
                Time.timeScale = 0f; // 게임 일시 정지
                break;

            case GameState.GamePlay:
                Time.timeScale = 1f; // 시간 정상화
                break;

            case GameState.GameOver:
                Time.timeScale = 1f; // 게임 정지
                FadeManager.Instance.FadeOut(() =>
                {
                    isfirsteLoad = false;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }); 
                   
                break;
        }
    }

    private void CloseAllUI()
    {
        if (GameStartUI != null) GameStartUI.SetActive(false);
        if (GamePauseUI != null) GamePauseUI.SetActive(false);
    }
}
