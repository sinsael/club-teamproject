public class Interaction_Document : Interaction_Obj
{
    public override void OnInteract()
    {
        base.OnInteract();

        DocumentManager.instance.CollectDocument();

        Destroy(gameObject);
    }
}
