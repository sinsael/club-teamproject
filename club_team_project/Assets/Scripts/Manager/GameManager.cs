<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
ï»¿using UnityEngine;
=======
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
>>>>>>> Stashed changes
=======
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
>>>>>>> Stashed changes
=======
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
>>>>>>> Stashed changes
=======
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
>>>>>>> Stashed changes
=======
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
>>>>>>> Stashed changes
=======
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
>>>>>>> Stashed changes
=======
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
>>>>>>> Stashed changes
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

    public static bool isfirsteLoad = true;
    public GameState currentGameState { get; private set; }
    public GameObject GameStartUI;
    public GameObject GamePauseUI;


    public MonoBehaviour playerController;

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
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

        if (HostageManager.instance.AllSaveHostage)
        {
            ChangeGameState(GameState.GameClear);
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeGameState (GameState.GameClear);
        }
    }

    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
        // í˜¹ì‹œ ëª¨ë¥¼ ì—°ê²° ëŠê¹€ ë°©ì§€ë¥¼ ìœ„í•´ ë‹¤ì‹œ ì°¾ê¸° ì‹œë„
        if (playerController == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                playerController = playerObj.GetComponent<MonoBehaviour>(); // í˜¹ì€ êµ¬ì²´ì ì¸ í´ë˜ìŠ¤ëª…
        }
=======
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
        GameObject canvas = GameObject.Find("Canvas");



        // 2. ¾ÀÀÌ ·ÎµåµÇ¸é ÀÏ´Ü ÆäÀÌµå ÀÎÀ» ¹«Á¶°Ç ½ÇÇàÇÕ´Ï´Ù.
        // Áß¿ä: ÆäÀÌµå°¡ ³¡³­ µÚ¿¡ °ÔÀÓ »óÅÂ¸¦ º¯°æÇØ¾ß ¾ÈÀüÇÕ´Ï´Ù.
        FadeManager.Instance.FadeIn();
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes

        if (isfirsteLoad)
        {
            // ÀÌÁ¦ Ã³À½ ·Îµù »óÅÂ·Î º¯°æ (¿©±â¼­ ½Ã°£ÀÌ ¸ØÃã)
            ChangeGameState(GameState.GameStart);
        }
        else
        {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
            // [ìˆ˜ì •] ë°”ë¡œ ì‹¤í–‰í•˜ì§€ ì•Šê³ , ì½”ë£¨í‹´ì„ í†µí•´ í•œ í”„ë ˆì„ ëŒ€ê¸° í›„ ì‹¤í–‰
            StartCoroutine(WaitAndFadeIn());
        }
    }

    IEnumerator WaitAndFadeIn()
    {
        // í•œ í”„ë ˆì„(ì•½ 0.016ì´ˆ)ì„ ì‰½ë‹ˆë‹¤. ì´ ë™ì•ˆ FadeManagerê°€ í™•ì‹¤íˆ ë¡œë”©ë©ë‹ˆë‹¤.
        yield return null;

        // ì•ˆì „ ì¥ì¹˜: ê¸°ë‹¤ë ¸ëŠ”ë°ë„ ì—†ìœ¼ë©´ ì—ëŸ¬ ë¡œê·¸ ì¶œë ¥
        if (FadeManager.Instance != null)
        {
            FadeManager.Instance.FadeIn();
            ChangeGameState(GameState.GamePlay);
        }
        else
        {
            Debug.LogError("FadeManagerê°€ ì—¬ì „íˆ ì—†ìŠµë‹ˆë‹¤! DontDestroyOnLoad ì„¤ì •ì„ í™•ì¸í•´ì£¼ì„¸ìš”.");
            // UIë¼ë„ ë„ê¸° ìœ„í•´ ê°•ì œ ë³€ê²½
            ChangeGameState(GameState.GamePlay);
=======
            // Ã³À½ÀÌ ¾Æ´Ï¸é ¹Ù·Î ÇÃ·¹ÀÌ
            ChangeGameState(GameState.GamePlay);
        }

        if (canvas != null)
        {
            // 2. ºÎ¸ğÀÇ transform.Find´Â ÀÚ½ÄÀÌ ²¨Á® ÀÖ¾îµµ Ã£¾Æ³À´Ï´Ù.
            Transform startUITr = canvas.transform.Find("GameStart");

            if (startUITr != null)
                GameStartUI = startUITr.gameObject;
            Transform PauseUI = canvas.transform.Find("Pause");

            if (PauseUI != null)
                GamePauseUI = PauseUI.gameObject;

>>>>>>> Stashed changes
        }
    }

=======
            // Ã³À½ÀÌ ¾Æ´Ï¸é ¹Ù·Î ÇÃ·¹ÀÌ
            ChangeGameState(GameState.GamePlay);
        }
=======
            // Ã³À½ÀÌ ¾Æ´Ï¸é ¹Ù·Î ÇÃ·¹ÀÌ
            ChangeGameState(GameState.GamePlay);
        }
>>>>>>> Stashed changes
=======
            // Ã³À½ÀÌ ¾Æ´Ï¸é ¹Ù·Î ÇÃ·¹ÀÌ
            ChangeGameState(GameState.GamePlay);
        }
>>>>>>> Stashed changes
=======
            // Ã³À½ÀÌ ¾Æ´Ï¸é ¹Ù·Î ÇÃ·¹ÀÌ
            ChangeGameState(GameState.GamePlay);
        }
>>>>>>> Stashed changes
=======
            // Ã³À½ÀÌ ¾Æ´Ï¸é ¹Ù·Î ÇÃ·¹ÀÌ
            ChangeGameState(GameState.GamePlay);
        }
>>>>>>> Stashed changes
=======
            // Ã³À½ÀÌ ¾Æ´Ï¸é ¹Ù·Î ÇÃ·¹ÀÌ
            ChangeGameState(GameState.GamePlay);
        }
>>>>>>> Stashed changes

        if (canvas != null)
        {
            // 2. ºÎ¸ğÀÇ transform.Find´Â ÀÚ½ÄÀÌ ²¨Á® ÀÖ¾îµµ Ã£¾Æ³À´Ï´Ù.
            Transform startUITr = canvas.transform.Find("GameStart");

            if (startUITr != null)
                GameStartUI = startUITr.gameObject;
            Transform PauseUI = canvas.transform.Find("Pause");

            if (PauseUI != null)
                GamePauseUI = PauseUI.gameObject;

        }
    }

<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes

    public void OnPlayerDead()
    {
        // ìƒíƒœëŠ” ë³€ê²½í•˜ë˜ (ì…ë ¥ ë§‰ê¸° ìš©ë„), UIëŠ” ë„ìš°ì§€ ì•ŠìŒ
        ChangeGameState(GameState.GameOver);
    }


    public void ChangeGameState(GameState newState)
    {
        currentGameState = newState;

        CloseAllUI();

        // 2. ìƒíƒœì— ë”°ë¼ í•„ìš”í•œ ì²˜ë¦¬(UI ì¼œê¸°, ì‹œê°„ ì •ì§€ ë“±)ë¥¼ 'í•œ ë²ˆë§Œ' ìˆ˜í–‰í•©ë‹ˆë‹¤.
        switch (currentGameState)
        {
            case GameState.GameStart:
                if (GameStartUI != null) GameStartUI.SetActive(true);

                playerController.enabled = false;
                Time.timeScale = 0f; // ì‹œê°„ ì •ìƒí™”
                break;

            case GameState.GamePause:
                if (GamePauseUI != null) GamePauseUI.SetActive(true);
                Time.timeScale = 0f; // ê²Œì„ ì¼ì‹œ ì •ì§€
                break;

            case GameState.GamePlay:
<<<<<<< Updated upstream
                Time.timeScale = 1f; // ì‹œê°„ ì •ìƒí™”
                playerController.enabled = true;
=======
                Time.timeScale = 1f; // ½Ã°£ Á¤»óÈ­
                isfirsteLoad = false;
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
                break;

            case GameState.GameOver:
                Time.timeScale = 1f; // ê²Œì„ ì •ì§€
                FadeManager.Instance.FadeOut(() =>
                {
                    isfirsteLoad = false;
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                });
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
<<<<<<< Updated upstream
=======
=======
=======
=======
=======
=======
=======
                break;
            case GameState.GameClear:
                Time.timeScale = 1f;
                FadeManager.Instance.FadeOut(() =>
                {
                    int currentIndex = SceneManager.GetActiveScene().buildIndex;
                    int nextIndex = currentIndex + 1;
                    if (nextIndex < SceneManager.sceneCountInBuildSettings)
                    {
                        // ´ÙÀ½ ¾ÀÀÌ Á¸ÀçÇÏ¸é ·Îµå
                        SceneManager.LoadScene(nextIndex);
                    }
                    else
                    {
                        // ´ÙÀ½ ¾ÀÀÌ ¾øÀ¸¸é(¸¶Áö¸· ¾ÀÀÌ¸é) Ã³À½(0¹ø)À¸·Î µ¹¾Æ°¨
                        SceneManager.LoadScene(0);
                    }
                });
>>>>>>> Stashed changes
                break;
            case GameState.GameClear:
                Time.timeScale = 1f;
                FadeManager.Instance.FadeOut(() =>
                {
                    int currentIndex = SceneManager.GetActiveScene().buildIndex;
                    int nextIndex = currentIndex + 1;
                    if (nextIndex < SceneManager.sceneCountInBuildSettings)
                    {
                        // ´ÙÀ½ ¾ÀÀÌ Á¸ÀçÇÏ¸é ·Îµå
                        SceneManager.LoadScene(nextIndex);
                    }
                    else
                    {
                        // ´ÙÀ½ ¾ÀÀÌ ¾øÀ¸¸é(¸¶Áö¸· ¾ÀÀÌ¸é) Ã³À½(0¹ø)À¸·Î µ¹¾Æ°¨
                        SceneManager.LoadScene(0);
                    }
                });
>>>>>>> Stashed changes
                break;
            case GameState.GameClear:
                Time.timeScale = 1f;
                FadeManager.Instance.FadeOut(() =>
                {
                    int currentIndex = SceneManager.GetActiveScene().buildIndex;
                    int nextIndex = currentIndex + 1;
                    if (nextIndex < SceneManager.sceneCountInBuildSettings)
                    {
                        // ´ÙÀ½ ¾ÀÀÌ Á¸ÀçÇÏ¸é ·Îµå
                        SceneManager.LoadScene(nextIndex);
                    }
                    else
                    {
                        // ´ÙÀ½ ¾ÀÀÌ ¾øÀ¸¸é(¸¶Áö¸· ¾ÀÀÌ¸é) Ã³À½(0¹ø)À¸·Î µ¹¾Æ°¨
                        SceneManager.LoadScene(0);
                    }
                });
>>>>>>> Stashed changes
                break;
            case GameState.GameClear:
                Time.timeScale = 1f;
                FadeManager.Instance.FadeOut(() =>
                {
                    int currentIndex = SceneManager.GetActiveScene().buildIndex;
                    int nextIndex = currentIndex + 1;
                    if (nextIndex < SceneManager.sceneCountInBuildSettings)
                    {
                        // ´ÙÀ½ ¾ÀÀÌ Á¸ÀçÇÏ¸é ·Îµå
                        SceneManager.LoadScene(nextIndex);
                    }
                    else
                    {
                        // ´ÙÀ½ ¾ÀÀÌ ¾øÀ¸¸é(¸¶Áö¸· ¾ÀÀÌ¸é) Ã³À½(0¹ø)À¸·Î µ¹¾Æ°¨
                        SceneManager.LoadScene(0);
                    }
                });
>>>>>>> Stashed changes
                break;
            case GameState.GameClear:
                Time.timeScale = 1f;
                FadeManager.Instance.FadeOut(() =>
                {
                    int currentIndex = SceneManager.GetActiveScene().buildIndex;
                    int nextIndex = currentIndex + 1;
                    if (nextIndex < SceneManager.sceneCountInBuildSettings)
                    {
                        // ´ÙÀ½ ¾ÀÀÌ Á¸ÀçÇÏ¸é ·Îµå
                        SceneManager.LoadScene(nextIndex);
                    }
                    else
                    {
                        // ´ÙÀ½ ¾ÀÀÌ ¾øÀ¸¸é(¸¶Áö¸· ¾ÀÀÌ¸é) Ã³À½(0¹ø)À¸·Î µ¹¾Æ°¨
                        SceneManager.LoadScene(0);
                    }
                });
>>>>>>> Stashed changes
                break;
            case GameState.GameClear:
                Time.timeScale = 1f;
                FadeManager.Instance.FadeOut(() =>
                {
                    int currentIndex = SceneManager.GetActiveScene().buildIndex;
                    int nextIndex = currentIndex + 1;
                    if (nextIndex < SceneManager.sceneCountInBuildSettings)
                    {
                        // ´ÙÀ½ ¾ÀÀÌ Á¸ÀçÇÏ¸é ·Îµå
                        SceneManager.LoadScene(nextIndex);
                    }
                    else
                    {
                        // ´ÙÀ½ ¾ÀÀÌ ¾øÀ¸¸é(¸¶Áö¸· ¾ÀÀÌ¸é) Ã³À½(0¹ø)À¸·Î µ¹¾Æ°¨
                        SceneManager.LoadScene(0);
                    }
                });
>>>>>>> Stashed changes
                break;
            case GameState.GameClear:
                Time.timeScale = 1f;
                FadeManager.Instance.FadeOut(() =>
                {
                    int currentIndex = SceneManager.GetActiveScene().buildIndex;
                    int nextIndex = currentIndex + 1;
                    if (nextIndex < SceneManager.sceneCountInBuildSettings)
                    {
                        // ´ÙÀ½ ¾ÀÀÌ Á¸ÀçÇÏ¸é ·Îµå
                        SceneManager.LoadScene(nextIndex);
                    }
                    else
                    {
                        // ´ÙÀ½ ¾ÀÀÌ ¾øÀ¸¸é(¸¶Áö¸· ¾ÀÀÌ¸é) Ã³À½(0¹ø)À¸·Î µ¹¾Æ°¨
                        SceneManager.LoadScene(0);
                    }
                });
>>>>>>> Stashed changes
                break;

            case GameState.GameClear:
                Time.timeScale = 1f;
                FadeManager.Instance.FadeOut(() =>
                {
                    int currentIndex = SceneManager.GetActiveScene().buildIndex;
                    int nextIndex = currentIndex + 1;
                    HostageManager.instance.AllSaveHostage = false;
                    SceneManager.LoadScene(nextIndex);
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
