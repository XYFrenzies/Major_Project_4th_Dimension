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
        Debug.Log("Dead");
        PSManager.animator.SetBool("IsDead", true);
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
