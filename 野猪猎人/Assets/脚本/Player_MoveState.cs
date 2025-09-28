using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MoveState : PlayerState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    public  override void Update()
    {
        base.Update();


        Vector3 inputVector = new Vector3(player.moveInput.x, rb.velocity.y, player.moveInput.y);
        Vector3 fixVelocity = Quaternion.Euler(0, -45f, 0) * inputVector;
        player.SetVelocity(fixVelocity.x, fixVelocity.y, fixVelocity.z, player.moveSpeed);//Íæ¼ÒÒÆ¶¯
    }
}
