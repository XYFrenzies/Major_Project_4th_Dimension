using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.InputSystem;

public class ArmPickUpState : ArmBaseState
{
    Rigidbody rb;
    Renderer rend;
    float radius;
    bool cancelPickUp = false;
    bool isShooting = false;
    float distanceBetweenPoints = 0f;
    int changePoint = 0;
    Vector3 offset = Vector3.zero;
    //private PlayerInput playerInput;
    //private InputAction shootAction;
    public ArmPickUpState(ArmStateManager arm) : base(arm)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Entered Pickup state");

        //playerInput = armStateMan.GetComponent<PlayerInput>();
        //shootAction = playerInput.actions["HookShot"];
        distanceBetweenPoints = Vector3.Distance(armStateMan.hitObject.transform.position, armStateMan.holdPoint.position);
        //Debug.Log(distanceBetweenPoints);
        //changePoint = (int)(distanceBetweenPoints / armStateMan.lights.Count);
        //armStateMan.shootAction.performed += Shoot;
        //armStateMan.shootAction.canceled += NotShoot;
        isShooting = true;

        /////////////////////////////////////////////
        //armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.HookShotThrown; // old version
        armStateMan.playerSM.ChangeState(armStateMan.playerSM.pickUpOrPutDownState); // new version
                                                                                     /////////////////////////////////////////////


        cancelPickUp = false;
        rb = armStateMan.hitObject.GetComponent<Rigidbody>();
        rend = armStateMan.hitObject.GetComponent<Renderer>();

        armStateMan.parentConstraint = armStateMan.hitObject.GetComponent<ParentConstraint>();
        if (armStateMan.parentConstraint.sourceCount == 0)
            armStateMan.parentConstraint.AddSource(armStateMan.constraintSource);

        radius = rend.bounds.extents.magnitude;
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        //armStateMan.lineRenderer.enabled = true;
    }

    public override void ExitState()
    {
        //armStateMan.shootAction.performed -= Shoot;
        //armStateMan.shootAction.canceled -= NotShoot;
        //Debug.Log("Exited pick up state");
        if (!cancelPickUp) // grab object
        {
            armStateMan.hitObject.layer = LayerMask.NameToLayer("Hold");


            //////////////////////////////// uncomment to use old method
            //rb.isKinematic = true;
            //armStateMan.hitObject.transform.SetParent(armStateMan.holdPoint);
            ///////////////////////////////

            armStateMan.armEffects.DrawObjectHoldingEffect();

            armStateMan.parentConstraint.constraintActive = true; // comment to use old version
            offset.z = radius;
            armStateMan.parentConstraint.SetTranslationOffset(0, offset);
            armStateMan.parentConstraint.weight = 1f;
            armStateMan.isObjectHeld = true;
            //armStateMan.lineRenderer.enabled = false;
            armStateMan.initialBeamSpeed = armStateMan.holdInitialBeamSpeedValue;
            rb = null;

            /////////
            armStateMan.playerSM.armRig.weight = 1f;
            ////////
        }
        else // let go of mouse button before grabbing object
        {
            armStateMan.hitObject.layer = LayerMask.NameToLayer("Default");

            ///////////////////// uncomment to use old version
            //rb.isKinematic = false;
            /////////////


            rb.useGravity = true;
            armStateMan.isObjectHeld = false;
            armStateMan.hitObject = null;
            armStateMan.lineRenderer.enabled = false;
            armStateMan.initialBeamSpeed = armStateMan.holdInitialBeamSpeedValue;
        }

        /////////////////////
        //armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.Normal; // old version
        armStateMan.playerSM.ChangeState(armStateMan.playerSM.idleState); // new version
        ////////////////////
    }

    public override void UpdateState()
    {
        armStateMan.hitPoint = armStateMan.hitObject.transform.position;
        armStateMan.armEffects.DrawLineRenderer();


        armStateMan.hitObject.transform.position = Vector3.MoveTowards(armStateMan.hitObject.transform.position, armStateMan.holdPoint.position,
            armStateMan.initialBeamSpeed * Time.deltaTime);

        armStateMan.initialBeamSpeed += armStateMan.beamSpeedAccelModifier / rb.mass;

        if (!armStateMan.shotArm)
        {
            cancelPickUp = true;
            armStateMan.SwitchState(armStateMan.idleState);
        }
        if (armStateMan.hitObject != null)
            if (Vector3.Distance(armStateMan.hitObject.transform.position, armStateMan.holdPoint.position) <= 1f + radius)
            {
                armStateMan.SwitchState(armStateMan.pauseState);

            }



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
