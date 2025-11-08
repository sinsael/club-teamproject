using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public PlayerInputSet input { get; private set; }
    public Vector2 moveinput { get; set; }
    public WeaponHandler WeaponHandler { get; private set; }

    void Awake()
    {
        input = new PlayerInputSet();
        WeaponHandler = GetComponent<WeaponHandler>();
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
    public void SwitchWeaponInput()
    {
        if (input.Player.SwitchPistol.WasPressedThisFrame())
        {
            WeaponHandler.EquipPistol();
        }
        else if (input.Player.SwitchRifle.WasPressedThisFrame())
        {
            WeaponHandler.EquipRifle();
        }
        else if (input.Player.SwitchShotgun.WasPressedThisFrame())
        {
            WeaponHandler.EquipShotgun();
        }
    }
}
