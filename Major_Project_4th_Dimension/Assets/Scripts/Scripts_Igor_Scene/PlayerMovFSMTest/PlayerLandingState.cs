using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLandingState : PlayerBaseState
{
    //private PlayerMovementSM pmStateMan;

    public PlayerLandingState(PlayerStateManager psm) : base(psm)
    {
        //pmStateMan = stateMachine;
    }

    public override void EnterState()
    {
        //base.EnterState();
        Debug.Log("Entered landing state");
        PSManager.animator.SetBool("IsLanding", true);
    }

    public override void ExitState()
    {
        Debug.Log("Exited landing state");
    }

    public override void UpdateLogic()
    {
        //base.UpdateLogic();


    }

    public override void UpdatePhysics()
    {

    }
}
