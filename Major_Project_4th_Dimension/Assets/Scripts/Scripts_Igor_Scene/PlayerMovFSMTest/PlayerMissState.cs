using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMissState : PlayerBaseState
{
    public PlayerMissState(PlayerStateManager psm) : base(psm)
    {
        //pmStateMan = stateMachine;
    }

    public override void EnterState()
    {
        //base.EnterState();
        //Debug.Log("Entered miss state");
        //PSManager.animator.SetBool("IsShooting", true);

    }

    public override void ExitState()
    {
        //Debug.Log("Exited miss state");
        //PSManager.animator.SetBool("IsShooting", false);

    }

    public override void UpdateLogic()
    {
        //base.UpdateLogic();
        PSManager.CalculateMove();

        //PSManager.GroundCheck();

    }

    public override void UpdatePhysics()
    {
        PSManager.CheckIfRotating();
        PSManager.RotatePlayerModel();

    }
}
