using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private PlayerMovementSM pmStateMan;

    public PlayerFallingState(PlayerMovementSM stateMachine) : base(stateMachine)
    {
        pmStateMan = stateMachine;
    }

    public override void EnterState()
    {
        base.EnterState();
        pmStateMan.anim.SetBool("Falling", true);

    }

    public override void ExitState()
    {

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();


    }

    public override void UpdatePhysics()
    {

    }


}
