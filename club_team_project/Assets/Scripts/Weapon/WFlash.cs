using UnityEngine;

public class WFlash :WeaponControllerBase
{
    public override void Initialize(Weapon weapon, Entity_Stat stats)
    {
        base.Initialize(weapon, stats);
        Debug.Log("¼¶±¤Åº Àü·« ÃÊ±âÈ­");
    }

    public override void OnAttackInput()
    {
        base.OnAttackInput();
    }

    public void stun()
    {
        
    }

}
