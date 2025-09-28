using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player_RunState :Player_MoveState
{
    public Player_RunState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput.x == 0 && player.moveInput.y == 0)
            stateMachine.ChangeState(player.idleState);

        if (player.playerEnemyDetector.IsEnemyInRange())
            stateMachine.ChangeState(player.attackState);
    }
}
