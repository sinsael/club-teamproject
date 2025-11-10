using UnityEngine;

public class Player : Entity
{
    public PlayerInputHandler input { get; private set; }
    public Weapon Weapon { get; private set; }

    public Interaction interaction { get; private set; }
    public Entity_Stat Entity_Stat { get; private set; }
    public Entity_Health Entity_Health { get; private set; }

    //플레이어 상태
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }


    // 캐릭터 마우스 포인터 따라가기
    private Camera maincamera;


    public override void Awake()
    {
        base.Awake();
        input = GetComponent<PlayerInputHandler>();
        interaction = GetComponent<Interaction>();
        Entity_Stat = GetComponent<Entity_Stat>();
        Entity_Health = GetComponent<Entity_Health>();
        Weapon = GetComponent<Weapon>();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        maincamera = Camera.main;
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Shoot();
        HandleMouseRotation();
        Intertable();
        input.SwitchWeaponInput();
    }

    private void HandleMouseRotation()
    {
        if (maincamera == null) return;

        Vector3 mouseScreenPos = Input.mousePosition;

        mouseScreenPos.z = maincamera.WorldToScreenPoint(transform.position).z;

        Vector3 mouseWorldPos = maincamera.ScreenToWorldPoint(mouseScreenPos);

        Vector2 direction = (mouseWorldPos - transform.position).normalized;

        transform.up = direction;
    }

    public void Shoot()
    {
        if (input.shoot)
        {
            Weapon?.HandleAttack();
        }
    }

    private void Intertable()
    {
        if (input.interact)
        {
            interaction.Interact();
        }
    }

    protected void OnDrawGizmos()
    {
        if (interaction == null)
            return;

        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(interaction.InterCheck.position, interaction.InterCheckRadius);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(interaction.interactionCheck.position, interaction.interactionRadius);
    }


}
