using UnityEngine;
using UnityEngine.UI;

public class UIManager: MonoBehaviour
{
    private static UIManager Instance;
    [SerializeField] private Image whitePannel;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        whitePannel.gameObject.SetActive(false);
    }
}
