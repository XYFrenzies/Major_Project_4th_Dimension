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
        Debug.Log("Entered pulling state");
        //pmStateMan.animator.SetBool("IsPulling", true);
    }

    public override void ExitState()
    {
        Debug.Log("Exited pulling state");
        //pmStateMan.animator.SetBool("IsPulling", false);

    }

    public override void UpdateLogic()
    {
        //base.UpdateLogic();
        PSManager.CalculateMove();
        PSManager.lookAction.Disable();

    }

    public override void UpdatePhysics()
    {
        PSManager.Move();

    }
}
