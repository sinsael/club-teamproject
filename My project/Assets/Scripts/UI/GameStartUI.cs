using UnityEngine;

public class GameStart_UI : MonoBehaviour
{
    GameInputSet gameInputSet;
    private void Awake()
    {
        gameInputSet = new GameInputSet();
    }

    private void OnEnable()
    {
        gameInputSet.Game.Enable();
    }

    private void OnDisable()
    {
        gameInputSet.Game.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameInputSet.Game.GameStart.WasPressedThisFrame())
        {
            GameManager.Instance.ChangeGameState(GameState.GamePlay);
        }
    }
}
