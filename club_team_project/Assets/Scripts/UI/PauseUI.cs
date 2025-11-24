using UnityEngine;
using UnityEngine.UI;
public class PauseUI : MonoBehaviour
{
    [SerializeField] Slider soundSlider;
    [SerializeField] Button resumeButton;

    void Start()
    {
        if(SoundManager.Instance != null)
        {
            soundSlider.value = SoundManager.Instance.GetSFXVolume();
        }

        soundSlider.onValueChanged.AddListener(OnSFXChanged);

        resumeButton.onClick.AddListener(OnResumeClick);
    }

    private void OnSFXChanged(float value)
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.SetSFXVolume(value);
        }
    }

    // '계속하기' 버튼 클릭 시 실행되는 함수
    private void OnResumeClick()
    {
        // GameManager에게 게임 상태를 GamePlay로 바꾸라고 요청
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ChangeGameState(GameState.GamePlay);
        }
    }
}
