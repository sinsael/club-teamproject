using UnityEngine;

public class Player : Entity
{
    public PlayerInputHandler input { get; private set; }

    public Interaction interaction { get; private set; }
    //Weapon weapon;
    public Entity_Helth Entity_Helth { get; private set; }
    public AlertSystem alertSystem { get; private set; }

    //플레이어 상태
    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }



    [Header("캐릭터 상세")]
    public int HP;
    public int speed;
    public float RegenDelady;
    public float RegenHP;

    public override void Awake()
    {
        base.Awake();
        input = GetComponent<PlayerInputHandler>();
        interaction = GetComponent<Interaction>();

        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public override void Start()
    {
        base.Start();
        
        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        Intertable();
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
