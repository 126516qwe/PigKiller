using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_BattleState : Enemy_GroundedState
{
    private Transform playerTransform;
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(playerTransform == null)
            playerTransform = enemy.enemyPlayerDetector.GetPlayerTransform();
    }

    public override void Update()
    {
        base.Update();

        if (WithinAttackRange())
            stateMachine.ChangeState(enemy.attackState);
        else if (WithinAttackRange() == false && enemy.enemyPlayerDetector.IsPlayerInRange() == true)
            MoveTowersPlayers();
        else
            stateMachine.ChangeState(enemy.idleState);
               
    }
    private void MoveTowersPlayers()
    {
        if(playerTransform==null)
            return;
        Vector3 direction = (playerTransform.position - enemy.transform.position);
        enemy.SetVelocity(direction.x, direction.y, direction.z, enemy.battleMoveSpeed);
    }

    private bool WithinAttackRange()
    {
        return DistanceToPlayer() < enemy.attackDistance;
    }

    private float DistanceToPlayer()
    {
        if(playerTransform == null)
            return float.MaxValue;

        return Vector3.Distance(playerTransform.position, enemy.transform.position);
    }
}
