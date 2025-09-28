using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_IdleState :Enemy_GroundedState
{
    private float idleTimer;
    public Enemy_IdleState(Enemy enemy, StateMachine stateMachine, string stateName) : base(enemy, stateMachine, stateName)
    {
    }

}
