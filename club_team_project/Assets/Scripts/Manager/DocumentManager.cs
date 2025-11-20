
using UnityEngine;

public class DocumentManager : MonoBehaviour
{
    public static DocumentManager instance;

    [Header("설정")]
    [SerializeField] private int totalDocuments = 5;
    public int currentDocuments = 0;

    [Header("연결")]
    [SerializeField] private GameObject exitDoor;

    public bool AllcollectDocuments = false;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if(exitDoor != null) exitDoor.SetActive(false);
    }

    public void CollectDocument()
    {
        currentDocuments++;

        if (currentDocuments >= totalDocuments)
        {
            UnlockStage();
        }
        else
        {
            SoundManager.Instance.PlaySFX(SoundManager.Instance.docFoundClip);
        }
    }

    private void UnlockStage()
    {
        AllcollectDocuments = true;

        SoundManager.Instance.PlaySFX(SoundManager.Instance.allDocsFoundClip);
    
        if(exitDoor != null)
        {
            exitDoor.SetActive(false);
        }
    }
}
