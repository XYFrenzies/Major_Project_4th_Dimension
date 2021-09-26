using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmPullState : ArmBaseState
{
    bool isShooting = false;
    float initialSpringForce;
    private PlayerInput playerInput;
    private InputAction shootAction;
    public ArmPullState(ArmStateManager arm) : base(arm)
    {

    }

    public override void EnterState()
    {
        //Debug.Log("Entered Pull state");

        playerInput = armStateMan.GetComponent<PlayerInput>();
        shootAction = playerInput.actions["HookShot"];

        shootAction.performed += Shoot;
        shootAction.canceled += NotShoot;
        isShooting = true;
        armStateMan.newGrappleHandle = Object.Instantiate(armStateMan.grappleHandle, armStateMan.hitObject.transform);
        armStateMan.newGrappleHandle.transform.localPosition = armStateMan.localPoint;
        armStateMan.newGrappleHandle.GetComponent<FixedJoint>().connectedBody = armStateMan.hitObject.GetComponent<Rigidbody>();
        //hand.transform.SetParent(armStateMan.newGrappleHandle.transform);
        //hand.transform.localPosition = Vector3.zero;
        armStateMan.hitPoint = armStateMan.newGrappleHandle.transform.position;
        armStateMan.springJoint.connectedBody = armStateMan.newGrappleHandle.GetComponent<Rigidbody>();
        armStateMan.springJoint.connectedAnchor = Vector3.zero;
        //float distance = Vector3.Distance(armStateMan.transform.position, armStateMan.newGrappleHandle.transform.position);
        armStateMan.springJoint.minDistance = 2.5f;
        armStateMan.springJoint.maxDistance = 2.5f;
        initialSpringForce = armStateMan.springJoint.spring;
        //armStateMan.lineRenderer.enabled = true;

        //currentHookShotState = HookShotState.Pull;
        armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.Normal;
    }

    public override void ExitState()
    {
        shootAction.performed -= Shoot;
        shootAction.canceled -= NotShoot;
        //hand.transform.SetParent(armStateMan.transform);
        Object.Destroy(armStateMan.newGrappleHandle);
        armStateMan.springJoint.spring = initialSpringForce;
        armStateMan.springJoint.connectedAnchor = Vector3.zero;

        armStateMan.springJoint.maxDistance = 0f;
        armStateMan.springJoint.minDistance = 0f;
        //ReturnHand();
        armStateMan.pull = false;
        armStateMan.hitObject = null;
        //armStateMan.lineRenderer.enabled = false;
    }

    public override void UpdateState()
    {

        if (!isShooting)
        {
            armStateMan.SwitchState(armStateMan.shootState);

        }
        armStateMan.hitPoint = armStateMan.newGrappleHandle.transform.position;
        armStateMan.springJoint.spring += Time.deltaTime;
    }

    public void Shoot(InputAction.CallbackContext context)
    {
        isShooting = true;
    }
    private void NotShoot(InputAction.CallbackContext context)
    {
        isShooting = false;

    }
}
