using UnityEngine;

public class Player_IdleState : PlayerState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter Idle State");
        player.SetVelocity(0f,0f);
    }

    public override void Update()
    {
        base.Update();
        if (player.input.moveinput != Vector2.zero)
        {
            stateMachine.ChangeState(player.moveState);
        }
    }
}
