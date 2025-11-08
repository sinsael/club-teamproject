using UnityEngine;

public class WFlash :WeaponController
{
    public override void Initialize(Weapon weapon, Entity_Stat stats)
    {
        base.Initialize(weapon, stats);
        Debug.Log("¼¶±¤Åº Àü·« ÃÊ±âÈ­");
    }

    public override void OnAttackInput()
    {
        // ¼¶±¤Åº ¹ß»ç
    }

    public void stun()
    {
        // ¼¶±¤ È¿°ú Àû¿ë
    }

}
