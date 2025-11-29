using UnityEngine;


[CreateAssetMenu(fileName ="WeaponStatSO", menuName = "Scriptable Objects/FlashStatSO")]
public class FlashStatSO : ScriptableObject
{
    [Header("¼¶±¤Åº Àü¿ë")]
    public float FlashbangDuration;
    public float FlashbangRadius;
    public float FlashSpeed;
}
