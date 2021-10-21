using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveLookState : PlayerBaseState
{
    private PlayerMovementSM pmStateMan;

    public PlayerMoveLookState(PlayerMovementSM stateMachine) : base(stateMachine)
    {
        pmStateMan = stateMachine;
    }



    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Enter move state");
        //pmStateMan.moveAction.Disable();
        pmStateMan.lookAction.Disable();
    }

    public override void ExitState()
    {

    }

    public override void UpdateLogic()
    {
        base.UpdateLogic();

        CalculateMove();
        CheckForNoMovement();
    }

    public override void UpdatePhysics()
    {
        Move();
        Look();
    }

    public void Move()
    {
        pmStateMan.rb.MovePosition(pmStateMan.rb.position + pmStateMan.direction * pmStateMan.moveSpeed * Time.fixedDeltaTime);

    }

    public void CalculateMove()
    {
        var camForward = pmStateMan.cam.transform.forward;
        camForward.y = 0.0f;
        camForward.Normalize();

        var camRight = pmStateMan.cam.transform.right;
        camRight.y = 0.0f;
        camRight.Normalize();

        pmStateMan.direction.x = 0f;
        pmStateMan.direction.z = 0f;

        pmStateMan.inputs = pmStateMan.moveAction.ReadValue<Vector2>();

        pmStateMan.direction += pmStateMan.inputs.y * camForward;
        pmStateMan.direction += pmStateMan.inputs.x * camRight;

        //pmStateMan.animator.SetFloat("xPos", pmStateMan.inputs.x, 0.3f, Time.deltaTime);
        //pmStateMan.animator.SetFloat("yPos", pmStateMan.inputs.y, 0.3f, Time.deltaTime);


    }

    public void CheckForNoMovement()
    {
        pmStateMan.inputs = pmStateMan.moveAction.ReadValue<Vector2>();
        pmStateMan.lookInputs = pmStateMan.lookAction.ReadValue<Vector2>();
        if (Mathf.Abs(pmStateMan.inputs.x) < Mathf.Epsilon && Mathf.Abs(pmStateMan.inputs.y) < Mathf.Epsilon && Mathf.Abs(pmStateMan.lookInputs.x) < Mathf.Epsilon && Mathf.Abs(pmStateMan.lookInputs.y) < Mathf.Epsilon)
            pmStateMan.ChangeState(pmStateMan.idleState);
    }

    public void Look()
    {
        pmStateMan.transform.rotation = Quaternion.Euler(0, pmStateMan.cam.transform.eulerAngles.y, 0);
    }
}
