using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickUpOrPutDownState : PlayerBaseState
{
    //private PlayerMovementSM pmStateMan;

    public PlayerPickUpOrPutDownState(PlayerStateManager psm) : base(psm)
    {
        //pmStateMan = stateMachine;
    }

    public override void EnterState()
    {
        //Debug.Log("Entered PUOPD state");
        //PSManager.animator.SetBool("IsShooting", true);

    }

    public override void ExitState()
    {
        //Debug.Log("Exited PUOPD state");
        PSManager.animator.SetBool("IsShooting", false);

    }

    public override void UpdateLogic()
    {
        PSManager.lookAction.Disable();
    }

    public override void UpdatePhysics()
    {

    }
}
