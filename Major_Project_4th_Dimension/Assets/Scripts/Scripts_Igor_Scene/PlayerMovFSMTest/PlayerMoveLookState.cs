using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveLookState : PlayerBaseState
{
    //private PlayerMovementSM pmStateMan;

    public PlayerMoveLookState(PlayerStateManager psm) : base(psm)
    {
        //pmStateMan = stateMachine;
    }

    public override void EnterState()
    {
        //base.EnterState();
        //Debug.Log("Enter move state");
        //pmStateMan.moveAction.Disable();
    }

    public override void ExitState()
    {

    }

    public override void UpdateLogic()
    {
        //base.UpdateLogic();

        PSManager.CalculateMove();
        CheckForNoMovement();
        PSManager.GroundCheck();
        PSManager.LookAtGrapplePoints();
    }

    public override void UpdatePhysics()
    {
        PSManager.Move();
        PSManager.RotatePlayerModel();


    }

    public void CheckForNoMovement()
    {
        PSManager.inputs = PSManager.moveAction.ReadValue<Vector2>();
        PSManager.lookInputs = PSManager.lookAction.ReadValue<Vector2>();
        if (Mathf.Abs(PSManager.inputs.x) < Mathf.Epsilon && Mathf.Abs(PSManager.inputs.y) < Mathf.Epsilon && Mathf.Abs(PSManager.lookInputs.x) < Mathf.Epsilon && Mathf.Abs(PSManager.lookInputs.y) < Mathf.Epsilon)
            PSManager.ChangeState(PSManager.idleState);
    }

}
