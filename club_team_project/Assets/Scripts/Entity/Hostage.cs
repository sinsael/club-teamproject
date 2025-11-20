using UnityEngine;
using UnityEngine.EventSystems;

public class Hostage : Entity, IInteraction
{
    public void OnSelect()
    {

    }
    public void OnDeselect()
    {
      
    }

    public void OnInteract()
    {
        HostageManager.instance.CollectHostage();
        Destroy(gameObject);
    }

    public override void EntityDeath()
    {
        base.EntityDeath();

        HostageManager.instance.HostageDied();
        Destroy(gameObject);
    }
}
