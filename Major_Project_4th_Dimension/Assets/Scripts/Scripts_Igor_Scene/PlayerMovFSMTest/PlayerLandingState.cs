using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerBaseState
{
    private PlayerMovementSM pmStateMan;

    public PlayerLandingState(PlayerMovementSM stateMachine) : base(stateMachine)
    {
        pmStateMan = stateMachine;
    }

    public override void EnterState()
    {
        base.EnterState();


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
