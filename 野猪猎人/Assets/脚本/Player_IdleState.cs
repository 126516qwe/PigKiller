public class Player_IdleState : PlayerState
{
    public Player_IdleState(Player player, StateMachine stateMachine, string stateName) : base(player, stateMachine, stateName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (player.moveInput.x != 0 || player.moveInput.y != 0)
            stateMachine.ChangeState(player.runState);

        if (player.playerEnemyDetector.IsEnemyInRange())
            stateMachine.ChangeState(player.attackState);
    }
}
