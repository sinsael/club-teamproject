using UnityEngine;

public class Player_MoveState : PlayerState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

   public override void Update()
    {
        base.Update();
        Debug.Log("Update Move State");
        if (input.moveinput == Vector2.zero)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();

        player.SetVelocity(input.moveinput.x * player.speed, input.moveinput.y * player.speed);
    }
}
