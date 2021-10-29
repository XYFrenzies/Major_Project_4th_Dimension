using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerBaseState
{
    //private PlayerMovementSM pmStateMan;

    public PlayerIdleState(PlayerStateManager psm) : base(psm)
    {
        //pmStateMan = stateMachine;
    }

    public override void EnterState()
    {
        //base.EnterState();
        //Debug.Log("Entered idle state");
        PSManager.moveAction.Enable();

    }

    public override void ExitState()
    {
        //Debug.Log("Exited idle state");

    }

    public override void UpdateLogic()
    {
        //base.UpdateLogic();
        PSManager.CalculateMove();
        PSManager.CheckIfRotating();
        CheckForMovement();
        PSManager.GroundCheck();

    }

    public override void UpdatePhysics()
    {

    }

    public void CheckForMovement()
    {
        PSManager.inputs = PSManager.moveAction.ReadValue<Vector2>();
        PSManager.lookInputs = PSManager.lookAction.ReadValue<Vector2>();
        if (Mathf.Abs(PSManager.inputs.x) > Mathf.Epsilon || Mathf.Abs(PSManager.inputs.y) > Mathf.Epsilon || Mathf.Abs(PSManager.lookInputs.x) > Mathf.Epsilon || Mathf.Abs(PSManager.lookInputs.y) > Mathf.Epsilon)
            PSManager.ChangeState(PSManager.moveLookState);
    }
}
