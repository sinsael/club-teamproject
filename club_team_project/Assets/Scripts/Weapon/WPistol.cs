using UnityEngine;
public class WPistol : WeaponControllerBase
{
    public override void Initialize(Weapon weapon, Entity_Stat stats)
    {
        base.Initialize(weapon, stats);
        Debug.Log("±«√— √ ±‚»≠");
    }

    public override void OnAttackInput()
    {
        base.OnAttackInput();
    }
}
