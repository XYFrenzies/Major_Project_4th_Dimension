using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerBaseState
{
    private PlayerMovementSM pmStateMan;

    public PlayerFallingState(PlayerMovementSM stateMachine) : base(stateMachine)
    {
        pmStateMan = stateMachine;
    }

    public override void EnterState()
    {
        base.EnterState();
        pmStateMan.anim.SetBool("Falling", true);
        Debug.Log("Entered falling state");
        pmStateMan.moveAction.Disable();
        pmStateMan.lookAction.Disable();
    }

    public override void ExitState()
    {
        Debug.Log("Exited falling state");

        pmStateMan.moveAction.Enable();
        pmStateMan.lookAction.Enable();
    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();
        pmStateMan.ChangeState(pmStateMan.landingState);

    }

    public override void UpdatePhysics()
    {

    }


}
