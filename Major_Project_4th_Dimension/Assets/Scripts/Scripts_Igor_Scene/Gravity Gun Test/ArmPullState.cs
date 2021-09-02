using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmPullState : ArmBaseState
{
    private PlayerInput playerInput;
    private InputAction shootAction;
    public ArmPullState(ArmStateManager arm) : base(arm)
    {

    }

    public override void EnterState()
    {
        playerInput = armStateMan.GetComponent<PlayerInput>();

        shootAction = playerInput.actions["HookShot"];
        Debug.Log("Entered Pull state");

        armStateMan.newGrappleHandle = Object.Instantiate(armStateMan.grappleHandle, armStateMan.hitObject.transform);
        armStateMan.newGrappleHandle.transform.localPosition = armStateMan.localPoint;
        armStateMan.newGrappleHandle.GetComponent<FixedJoint>().connectedBody = armStateMan.hitObject.GetComponent<Rigidbody>();
        //hand.transform.SetParent(armStateMan.newGrappleHandle.transform);
        //hand.transform.localPosition = Vector3.zero;
        armStateMan.springJoint.connectedBody = armStateMan.newGrappleHandle.GetComponent<Rigidbody>();
        armStateMan.springJoint.connectedAnchor = Vector3.zero;
        float distance = Vector3.Distance(armStateMan.transform.position, armStateMan.newGrappleHandle.transform.position);
        armStateMan.springJoint.minDistance = 2.5f;
        armStateMan.springJoint.maxDistance = 2.5f;
        armStateMan.lineRenderer.enabled = true;

        //currentHookShotState = HookShotState.Pull;
        armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.Normal;
    }

    public override void ExitState()
    {

        //hand.transform.SetParent(armStateMan.transform);
        Object.Destroy(armStateMan.newGrappleHandle);
        armStateMan.springJoint.connectedAnchor = Vector3.zero;

        armStateMan.springJoint.maxDistance = 0f;
        armStateMan.springJoint.minDistance = 0f;
        //ReturnHand();
        armStateMan.pull = false;
        armStateMan.hitObject = null;
        armStateMan.lineRenderer.enabled = false;
    }

    public override void UpdateState()
    {

        if (shootAction.triggered)
        {
            armStateMan.SwitchState(armStateMan.shootState);

        }
        armStateMan.hitPoint = armStateMan.newGrappleHandle.transform.position;

        armStateMan.DrawLineRenderer();
    }
}
