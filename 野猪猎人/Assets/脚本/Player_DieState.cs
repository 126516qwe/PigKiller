using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DieState : PlayerState
{
    public Player_DieState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }
    
}
