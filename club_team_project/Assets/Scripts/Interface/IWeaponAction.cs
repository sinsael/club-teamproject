using UnityEngine;

public interface IWeaponAction
{
    void Initialize(Weapon weapon, Entity_Stat stats);
    void OnAttackInput();
    void PerformAttack();
}
