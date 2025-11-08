using UnityEngine;

public class WShotgun : WeaponControllerBase
{
    public override void Initialize(Weapon weapon, Entity_Stat stats)
    {
        base.Initialize(weapon, stats);
        Debug.Log("º¶∞« √ ±‚»≠");
    }

    public override void OnAttackInput()
    {
        base.OnAttackInput();
    }
}
