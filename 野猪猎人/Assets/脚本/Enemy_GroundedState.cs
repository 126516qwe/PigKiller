public class Enemy_GroundedState : EnemyState
{
    public Enemy_GroundedState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {

    }
    public override void Update()
    {
        base.Update();

        if (enemy.enemyPlayerDetector.IsPlayerInRange() == true && enemy.health.isDead == false)
            stateMachine.ChangeState(enemy.battleState);


    }

}
