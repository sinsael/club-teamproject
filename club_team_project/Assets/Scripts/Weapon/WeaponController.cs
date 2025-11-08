using UnityEditor;
using UnityEngine;

public class WeaponController : IWeaponStrategy
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
    }

    public void OnReload()
    {
        throw new System.NotImplementedException();
    }

    //private void OnDrawGizmos()
    //{
    //    Handles.color = color;

    //    Vector2 myUp = transform.rotation * Vector3.up;
    //    Vector2 startDirection = Quaternion.Euler(0, 0, -stats.weaponstat.fov / 2) * myUp;

    //    Handles.DrawSolidArc(transform.position, Vector3.forward, startDirection, fov, radius);
    //}
}
