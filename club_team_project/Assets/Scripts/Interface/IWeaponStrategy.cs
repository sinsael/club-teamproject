using UnityEngine;

public interface IWeaponStrategy
{
    void Initialize(Weapon weapon, Entity_Stat stats);
    void OnAttackInput();
    void OnReload();
}
