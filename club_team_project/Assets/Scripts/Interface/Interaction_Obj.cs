using UnityEngine;
using UnityEngine.EventSystems;

public class Interaction_Obj : MonoBehaviour, IInteraction, IPointerClickHandler
{
    SpriteRenderer sr;
    Color currentcol;
    protected bool material = false;

    public virtual void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        currentcol = sr.color;
    }

    public virtual void OnSelect()
    {
        sr.color = Color.green;
        Debug.Log(gameObject.name + " is selected");
    }

    public virtual void OnDeselect()
    {
        sr.color = currentcol;
        Debug.Log(gameObject.name + " is deselected");
    }

    public virtual void OnInteract()
    {
        Debug.Log("Interacted with " + gameObject.name);
    }

    public virtual void OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
