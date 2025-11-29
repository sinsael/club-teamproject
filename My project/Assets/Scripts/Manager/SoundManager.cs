using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    [SerializeField] private AudioSource sfxSource;

    [Header("--- 게임 이벤트 사운드 파일 ---")]
    [Header("1. 미션/스테이지")]
    public AudioClip commonMissionStartClip;
    public AudioClip mission1StartClip;  // 미션 1 시작.mp3
    public AudioClip stage2StartClip;    // 2스테이지 시작.mp3
    public AudioClip missionClearClip;   // 미션 클리어.mp3
    public AudioClip playerDeathClip;    // 사망.mp3

    [Header("2. 수집 (문서/인질)")]
    public AudioClip docFoundClip;       // 문서 발견.mp3
    public AudioClip allDocsFoundClip;   // 문서 모두 발견.mp3
    public AudioClip hostageFoundClip;   // 인질 발견.mp3
    public AudioClip allHostagesFoundClip;// 인질 모두 발견.mp3

    [Header("3. 전투")]
    public AudioClip[] enemyDeathClips;  // [적 처치 1.mp3, 적 처치 2.mp3] 를 배열로 넣기

    [Header("4. 총 사운드")]
    public AudioClip pistolClip;
    public AudioClip rifleClip;
    public AudioClip shotgunClip;
    public AudioClip reloadClip;
    public AudioClip shotgunReloadClip;
    public AudioClip flashClip;

    [Space]
    public AudioClip doorBreachClip;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 기본 효과음 재생
    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip == null) return;
        sfxSource.PlayOneShot(clip, volume);
    }

    // [기능] 적 처치 소리 랜덤 재생 (1번 or 2번 중 하나)
    public void PlayEnemyDeathSound()
    {
        if (enemyDeathClips.Length > 0)
        {
            int randomIndex = Random.Range(0, enemyDeathClips.Length);
            PlaySFX(enemyDeathClips[randomIndex]);
        }
    }


    // [추가] SFX 볼륨 조절 (0.0 ~ 1.0)
    public void SetSFXVolume(float volume)
    {
        if (sfxSource != null)
        {
            sfxSource.volume = volume;
        }
    }

    // [추가] 현재 SFX 볼륨 가져오기 (UI 초기화용)
    public float GetSFXVolume()
    {
        return sfxSource != null ? sfxSource.volume : 1f;
    }
}
