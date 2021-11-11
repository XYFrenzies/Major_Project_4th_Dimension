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
        //Debug.Log("Entered landing state");
        //PSManager.animator.SetBool("IsGrounded", true);
    }

    public override void ExitState()
    {
        //Debug.Log("Exited landing state");
        //PSManager.ChangeState(PSManager.idleState);
        //PSManager.hasLanded = false;
    }

    public override void UpdateLogic()
    {
        //base.UpdateLogic();
        //PSManager.GroundCheck();
        // if(PSManager.Grounded)
        //if (PSManager.hasLanded)
        PSManager.ChangeState(PSManager.idleState);
    }

    public override void UpdatePhysics()
    {

    }
}
