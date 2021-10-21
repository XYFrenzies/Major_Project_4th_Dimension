using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    private PlayerMovementSM pmStateMan;

    public PlayerIdleState(PlayerMovementSM stateMachine) : base(stateMachine)
    {
        pmStateMan = stateMachine;
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Entered idle state");

    }

    public override void ExitState()
    {
        Debug.Log("Exited idle state");

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        CheckForMovement();
    }

    public override void UpdatePhysics()
    {

    }

    public void CheckForMovement()
    {
        pmStateMan.inputs = pmStateMan.moveAction.ReadValue<Vector2>();
        pmStateMan.lookInputs = pmStateMan.lookAction.ReadValue<Vector2>();
        if (Mathf.Abs(pmStateMan.inputs.x) > Mathf.Epsilon || Mathf.Abs(pmStateMan.inputs.y) > Mathf.Epsilon || Mathf.Abs(pmStateMan.lookInputs.x) > Mathf.Epsilon || Mathf.Abs(pmStateMan.lookInputs.y) > Mathf.Epsilon)
            pmStateMan.ChangeState(pmStateMan.moveLookState);
    }
}
