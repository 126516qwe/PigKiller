using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AttackState : Player_MoveState
{
    private float attackVelocityTimer;

    public Player_AttackState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();

    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

    }

}
