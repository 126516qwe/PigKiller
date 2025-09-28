using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Pig : Enemy
{
    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(this, stateMachine, "idle");
        runState = new Enemy_RunState(this, stateMachine, "run");
        attackState = new Enemy_AttackState(this, stateMachine, "attack");
        battleState = new Enemy_BattleState(this, stateMachine, "battle");
        dieState = new Enemy_DieState(this, stateMachine, "die");
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }
}
