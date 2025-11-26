using UnityEngine;

public class HostageManager : MonoBehaviour
{
    public static HostageManager instance;

    [Header("설정")]
    [SerializeField] private int totalHostage = 5;
    public int currentHostage = 0;

    [SerializeField] private int maxDeathTolerance = 2; // 몇 명 이상 죽으면 게임 오버인지
    public int deadHostageCount = 0;  // 현재 죽은 인질 수

    [Header("연결")]
    [SerializeField] private GameObject exitDoor;

    public bool AllSaveHostage = false;
    public bool Failed { get; private set; }

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
        if (exitDoor != null) exitDoor.SetActive(false);
    }

    public void CollectHostage()
    {
        if (Failed) return; // 이미 실패했으면 집계 안 함

        currentHostage++;
        SoundManager.Instance.PlaySFX(SoundManager.Instance.hostageFoundClip);
        CheckStageClear(); // 상태 체크 함수로 통합
    }

    public void HostageDied()
    {
        if (Failed) return;

        deadHostageCount++;

        // 1. 허용치 이상 죽었는지 즉시 확인 (게임 오버)
        if (deadHostageCount >= maxDeathTolerance)
        {
            TriggerGameOver();
        }
        // 2. 게임 오버가 아니라면, 남은 인질 계산 (혹시 마지막 인질이 죽었을 수도 있으니)
        else
        {
            CheckStageClear();
        }
    }

    // 구출 + 사망 합산하여 스테이지 종료 여부 판단
    private void CheckStageClear()
    {
        // 모든 인질 처리가 끝났는가? (구출 + 사망 == 전체)
        if (currentHostage + deadHostageCount >= totalHostage)
        {
            // 실패 조건(사망자 초과)을 넘지 않았을 때만 클리어
            if (deadHostageCount < maxDeathTolerance)
            {
                UnlockStage();
            }
        }
    }

    private void UnlockStage()
    {
        AllSaveHostage = true;
        SoundManager.Instance.PlaySFX(SoundManager.Instance.allHostagesFoundClip);
    }

    private void TriggerGameOver()
    {
        Failed = true;
        // GameManager.Instance.GameOver(); // 실제 게임 오버 처리
    }
}
