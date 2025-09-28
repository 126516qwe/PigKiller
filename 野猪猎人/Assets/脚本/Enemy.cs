using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy :Entity
{
    public Enemy_IdleState idleState;
    public Enemy_RunState runState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;
    public Enemy_DieState dieState;
    public Enemy_Health health { get; private set; }

    [Header("Battle details")]
    public float battleMoveSpeed = 3;
    public float attackDistance = 2;

    [Header("�ƶ�����")]
    public float idleTime = 2;
    public float moveSpeed = 1.4f;
    [Range(0, 2)]
    public float moveAnimSpeedMultiplier = 1;
    public float battleAnimSpeedMultiplier = 1;

    public Enemy_PlayerDetector enemyPlayerDetector{get;private set;}

     protected override void Awake()
    {
        base.Awake();

        health = GetComponent<Enemy_Health>();

        enemyPlayerDetector = GetComponentInChildren<Enemy_PlayerDetector>();
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
        health.OnDeath += HandleDeath;
    }

    private void HandleDeath()
    {
        // ֻ�е�ǰ��������״̬ʱ���л�
        if (stateMachine.currentState != dieState)
        {
            stateMachine.ChangeState(dieState);
        }
    }

    private void OnDestroy()
    {
        // ȡ�������¼�
        if (health != null)
            health.OnDeath -= HandleDeath;
    }


}
