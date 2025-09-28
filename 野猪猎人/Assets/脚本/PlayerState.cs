

public class PlayerState : EntityState
{
    protected Player player;
    protected PlayerInputSet input;

    
    public PlayerState(Player player, StateMachine stateMachine, string animBoolName): base(stateMachine,animBoolName)
    {
        this.player = player;

        anim = player.anim;
        rb = player.rb;
        input = player.input;
    }

    public override void Update()
    {
        base.Update();

        if (player.health.isDead == true)
            stateMachine.ChangeState(player.dieState);
    }


}
