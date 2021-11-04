using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPullingState : PlayerBaseState
{
    //private PlayerMovementSM pmStateMan;

    public PlayerPullingState(PlayerStateManager psm) : base(psm)
    { 
        //pmStateMan = stateMachine;
    }

    public override void EnterState()
    {
        //base.EnterState();
        //Debug.Log("Entered pulling state");
        //PSManager.animator.SetBool("IsPulling", true);
        PSManager.animator.SetBool("IsShooting", true);
        PSManager.animator.SetBool("IsPulling", true);

    }

    public override void ExitState()
    {
        //Debug.Log("Exited pulling state");
        //PSManager.animator.SetBool("IsPulling", false);
        PSManager.animator.SetBool("IsShooting", false);
        PSManager.animator.SetBool("IsPulling", false);


    }

    public override void UpdateLogic()
    {
        //base.UpdateLogic();
        PSManager.CalculateMove();
        //PSManager.lookAction.Disable();

    }

    public override void UpdatePhysics()
    {
        PSManager.Move();
        PSManager.RotatePlayerModel();


    }
}
