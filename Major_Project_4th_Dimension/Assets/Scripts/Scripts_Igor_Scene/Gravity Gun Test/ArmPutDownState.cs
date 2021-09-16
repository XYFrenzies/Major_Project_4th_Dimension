using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmPutDownState : ArmBaseState
{
    //private PlayerInput playerInput;
    //private InputAction shootAction;
    Rigidbody rb;
    bool isShooting = false;


    public ArmPutDownState(ArmStateManager arm) : base(arm)
    {

    }

    public override void EnterState()
    {
        armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.HookShotThrown;
        armStateMan.shootAction.performed += Shoot;
        armStateMan.shootAction.canceled += NotShoot;
        //Debug.Log("Entered Putdown state");
        //playerInput = armStateMan.GetComponent<PlayerInput>();
        isShooting = true;
        //shootAction = playerInput.actions["HookShot"];
        rb = armStateMan.hitObject.GetComponent<Rigidbody>();
        rb.isKinematic = false;
        armStateMan.hitObject.transform.SetParent(null);
        armStateMan.lineRenderer.enabled = true;

    }

    public override void ExitState()
    {
        armStateMan.hitObject.layer = LayerMask.NameToLayer("Default");
        rb.isKinematic = false;
        rb.useGravity = true;
        armStateMan.isObjectHeld = false;
        armStateMan.hitObject = null;
        armStateMan.lineRenderer.enabled = false;
        armStateMan.initialBeamSpeed = armStateMan.holdInitialBeamSpeedValue;
        armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.Normal;
        armStateMan.shootAction.performed -= Shoot;
        armStateMan.shootAction.canceled -= NotShoot;

    }

    public override void UpdateState()
    {
        //if (armStateMan.hitObject != null)
        //{
        //ShootHand();
        //armStateMan.hitPoint = armStateMan.hitObject.transform.position;

        armStateMan.hitObject.transform.position = Vector3.MoveTowards(armStateMan.hitObject.transform.position, armStateMan.hitPoint, armStateMan.initialBeamSpeed * Time.deltaTime);
        armStateMan.initialBeamSpeed += armStateMan.beamSpeedAccelModifier / rb.mass;
        //rb.MovePosition(target * 5f * Time.deltaTime);
        if (Vector3.Distance(armStateMan.hitObject.transform.position, armStateMan.hitPoint) <= 2f)
        {

            armStateMan.SwitchState(armStateMan.pauseState);

        }
        //  }
        if (!isShooting) // interrupt put down action and exit before object reaches target point
        {
            armStateMan.SwitchState(armStateMan.pauseState);
        }
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
