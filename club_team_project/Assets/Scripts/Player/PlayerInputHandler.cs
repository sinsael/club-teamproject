using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    public PlayerInputSet input { get; private set; }

    public Vector2 moveinput { get; set; }

    void Awake()
    {
        input = new PlayerInputSet();
    }

    void OnEnable()
    {
        input.Enable();
        Movementinput();
    }

    void OnDisable()
    {
        input.Disable();
    }

    public void Movementinput()
    {
        input.Player.Move.performed += ctx => moveinput = ctx.ReadValue<Vector2>();
        input.Player.Move.canceled += ctx => moveinput = Vector2.zero;
    }

    public bool interact => input.Player.Interact.WasPressedThisFrame();
    public bool shoot => input.Player.Shoot.WasPressedThisFrame();
    public bool click => input.Player.Click.WasPressedThisFrame();

}
