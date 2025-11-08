using UnityEngine;

public class WShotgun : WeaponController
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
