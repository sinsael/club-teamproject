using UnityEngine;

public class WRifle :WeaponController
{
    public override void Initialize(Weapon weapon, Entity_Stat stats)
    {
        base.Initialize(weapon, stats);
    }
    public override void OnAttackInput()
    {
       // 3점사 발사
    }
}
