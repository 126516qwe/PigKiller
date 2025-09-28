using UnityEngine;

public class Enemy_DieState : EnemyState
{
    private float dieDuration = 3f;
    private float timer;
    public Enemy_DieState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }
    public override void Enter()
    {
        base.Enter();
        enemy.health.Die();
        timer = dieDuration;
    }
    public override void Update()
    {
        base.Update();
        timer -=Time.deltaTime;
        if (timer < 0)
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }
    }
    public override void Exit()
    {
        base.Exit();
        enemy.health.Relife();
    }

}
