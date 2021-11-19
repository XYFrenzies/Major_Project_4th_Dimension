using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeathState : PlayerBaseState
{
    public PlayerDeathState(PlayerStateManager psm) : base(psm)
    {

    }

    public override void EnterState()
    {
        PSManager.animator.SetBool("IsDead", true);
        PSManager.arm.shootAction.Disable();
        PSManager.arm.shotArm = false;
        PSManager.moveAction.Disable();
    }

    public override void ExitState()
    {

    }

    public override void UpdateLogic()
    {
        PSManager.lookAction.Disable();
    }

    public override void UpdatePhysics()
    {
        PSManager.lookAction.Disable();

    }
}
