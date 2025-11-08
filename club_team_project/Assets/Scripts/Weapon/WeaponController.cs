public class WeaponControllerBase : IWeaponAction
{
    public Entity_Stat stats;
    public Weapon weapon;

    public virtual void Initialize(Weapon weapon, Entity_Stat stats)
    {
        this.stats = stats;
        this.weapon = weapon;
       
    }

    public virtual void OnAttackInput()
    {
        weapon.RequestAttack();
    }

    public virtual void OnReload()
    {
    }
}
