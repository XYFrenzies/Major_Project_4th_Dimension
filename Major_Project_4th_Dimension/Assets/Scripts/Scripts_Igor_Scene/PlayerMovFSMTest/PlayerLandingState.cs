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
        Debug.Log("Entered landing state");
    }

    public override void ExitState()
    {
        Debug.Log("Exited landing state");
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();


    }

    public override void UpdatePhysics()
    {

    }
}
