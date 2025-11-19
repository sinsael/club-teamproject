using UnityEngine;

public class Player_MoveState : PlayerState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();


    }

    public override void Update()
    {
        base.Update();
        if (input.moveinput == Vector2.zero)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        float speed = player.Entity_Stat.GetSpeed();

        player.SetVelocity(input.moveinput.x * speed, input.moveinput.y * speed);
    }
}
