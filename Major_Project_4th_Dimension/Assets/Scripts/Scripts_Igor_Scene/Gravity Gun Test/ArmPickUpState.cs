using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmPickUpState : ArmBaseState
{
    Rigidbody rb;
    Renderer rend;
    float radius;
    bool cancelPickUp = false;
    bool isShooting = false;

    private PlayerInput playerInput;
    private InputAction shootAction;
    public ArmPickUpState(ArmStateManager arm) : base(arm)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Entered Pickup state");

        playerInput = armStateMan.GetComponent<PlayerInput>();
        shootAction = playerInput.actions["HookShot"];

        shootAction.performed += context => Shoot();
        shootAction.canceled += context => NotShoot();

        armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.HookShotThrown;
        cancelPickUp = false;
        rb = armStateMan.hitObject.GetComponent<Rigidbody>();
        rend = armStateMan.hitObject.GetComponent<Renderer>();
        radius = rend.bounds.extents.magnitude;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        armStateMan.lineRenderer.enabled = true;

    }

    public override void ExitState()
    {
        shootAction.performed -= context => Shoot();
        shootAction.canceled -= context => NotShoot();
        Debug.Log("Exited pick up state");
        if (!cancelPickUp) // grab object
        {
            armStateMan.hitObject.layer = LayerMask.NameToLayer("Hold");
            rb.isKinematic = true;
            armStateMan.hitObject.transform.SetParent(armStateMan.holdPoint);
            armStateMan.isObjectHeld = true;
            armStateMan.lineRenderer.enabled = false;
            armStateMan.initialBeamSpeed = armStateMan.holdInitialBeamSpeedValue;
            rb = null;

        }
        else // let go of mouse button before grabbing object
        {
            armStateMan.hitObject.layer = LayerMask.NameToLayer("Default");
            rb.isKinematic = false;
            rb.useGravity = true;
            armStateMan.isObjectHeld = false;
            armStateMan.hitObject = null;
            armStateMan.lineRenderer.enabled = false;
            armStateMan.initialBeamSpeed = armStateMan.holdInitialBeamSpeedValue;
        }
        armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.Normal;
    }

    public override void UpdateState()
    {
        armStateMan.hitPoint = armStateMan.hitObject.transform.position;

        armStateMan.hitObject.transform.position = Vector3.MoveTowards(armStateMan.hitObject.transform.position, armStateMan.holdPoint.position,
            armStateMan.initialBeamSpeed * Time.deltaTime);

        armStateMan.initialBeamSpeed += armStateMan.beamSpeedAccelModifier / rb.mass;

        if (Vector3.Distance(armStateMan.hitObject.transform.position, armStateMan.holdPoint.position) <= 1f + radius)
        {
            armStateMan.SwitchState(armStateMan.pauseState);

        }

        if (!Mouse.current.leftButton.isPressed)
        {
            cancelPickUp = true;
            armStateMan.SwitchState(armStateMan.pauseState);
        }
    }

    public void Shoot()
    {
        isShooting = true;
    }
    private void NotShoot()
    {
        isShooting = false;
    }
}
