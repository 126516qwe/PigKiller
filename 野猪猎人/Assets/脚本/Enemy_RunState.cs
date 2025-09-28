using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Enemy_RunState : Enemy_GroundedState
{
    private float randomTimer =0f;
    private Vector3 randomMove;
    public Enemy_RunState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName)
    {
        GenerateRandomDirection();
    }
    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
         base.Update();
        randomTimer += Time.deltaTime;

        if (randomTimer >= 5f)
        {
            GenerateRandomDirection();
                randomTimer = 0f;
        }
        enemy.SetVelocity( randomMove.x, 0, randomMove.z,enemy.moveSpeed);
    }

    private void GenerateRandomDirection()
    {
        randomMove = new Vector3(Random.Range(-1f,1f),0f,Random.Range(-1f,1f));
    }
}
