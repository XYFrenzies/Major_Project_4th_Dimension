using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
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
        armStateMan.playerSM.armRig.weight = 0f;


        ///////////////////
        //armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.HookShotThrown;
        armStateMan.playerSM.ChangeState(armStateMan.playerSM.pickUpOrPutDownState);
        ///////////////////

        //armStateMan.shootAction.performed += Shoot;
        //armStateMan.shootAction.canceled += NotShoot;
        //Debug.Log("Entered Putdown state");
        //playerInput = armStateMan.GetComponent<PlayerInput>();

        armStateMan.parentConstraint = armStateMan.hitObject.GetComponent<ParentConstraint>();
        armStateMan.parentConstraint.constraintActive = false;

        isShooting = true;
        //shootAction = playerInput.actions["HookShot"];
        rb = armStateMan.hitObject.GetComponent<Rigidbody>();
        //rb.isKinematic = false;
        armStateMan.hitObject.transform.SetParent(null);
        //armStateMan.lineRenderer.enabled = true;
        armStateMan.armEffects.StopDrawingObjectHoldingEffect();

    }

    public override void ExitState()
    {
        armStateMan.hitObject.layer = LayerMask.NameToLayer("Default");
        //rb.isKinematic = false;
        rb.useGravity = true;
        armStateMan.isObjectHeld = false;
        armStateMan.hitObject = null;
        //armStateMan.lineRenderer.enabled = false;
        armStateMan.initialBeamSpeed = armStateMan.holdInitialBeamSpeedValue;

        //////////////////
        //armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.Normal;
        armStateMan.playerSM.ChangeState(armStateMan.playerSM.idleState);
        //////////////////

        //armStateMan.shootAction.performed -= Shoot;
        //armStateMan.shootAction.canceled -= NotShoot;
        if (armStateMan.parentConstraint.sourceCount != 0)
            armStateMan.parentConstraint.RemoveSource(0);
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
        if (Vector3.Distance(armStateMan.hitObject.transform.position, armStateMan.hitPoint) <= 0.5f)
        {

            armStateMan.SwitchState(armStateMan.pauseState);

        }
        //  }
        if (!armStateMan.shotArm) // interrupt put down action and exit before object reaches target point
        {
            armStateMan.SwitchState(armStateMan.pauseState);
        }
        armStateMan.armEffects.DrawLineRenderer();

    }

    //public void Shoot(InputAction.CallbackContext context)
    //{
    //    isShooting = true;
    //}
    //private void NotShoot(InputAction.CallbackContext context)
    //{
    //    isShooting = false;
    //}
}
