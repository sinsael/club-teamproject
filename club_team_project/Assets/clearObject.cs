using UnityEngine;

public class clearObject : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.ChangeGameState(GameState.GameClear);
        }
    }
}
