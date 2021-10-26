using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    //private PlayerMovementSM pmStateMan;

    public PlayerFallingState(PlayerStateManager psm) : base(psm)
    {
        //pmStateMan = stateMachine;
    }

    public override void EnterState()
    {
        //base.EnterState();
        PSManager.animator.SetBool("IsFlying", true);
        //Debug.Log("Entered falling state");

    }

    public override void ExitState()
    {
        //Debug.Log("Exited falling state");
        PSManager.animator.SetBool("IsFlying", false);

        PSManager.moveAction.Enable();
        PSManager.lookAction.Enable();
    }

    public override void UpdateLogic()
    {
        //base.UpdateLogic();
        PSManager.ChangeState(PSManager.landingState);
        PSManager.moveAction.Disable();
        PSManager.lookAction.Disable();
        PSManager.GroundCheck();
        if (PSManager.Grounded)
        {
            PSManager.animator.SetBool("IsGrounded", true);
            PSManager.ChangeState(PSManager.landingState); 
        }
    }

    public override void UpdatePhysics()
    {

    }


}
