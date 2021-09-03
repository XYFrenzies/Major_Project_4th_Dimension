using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPickUpState : ArmBaseState
{
    Rigidbody rb;
    public ArmPickUpState(ArmStateManager arm) : base(arm)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Entered Pickup state");
        rb = armStateMan.hitObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        armStateMan.lineRenderer.enabled = true;
        
    }

    public override void ExitState()
    {

        armStateMan.hitObject.layer = LayerMask.NameToLayer("Hold");
        rb.isKinematic = true;
        armStateMan.hitObject.transform.SetParent(armStateMan.holdPoint);
        armStateMan.isObjectHeld = true;
        armStateMan.lineRenderer.enabled = false;
        armStateMan.initialBeamSpeed = armStateMan.holdInitialBeamSpeedValue;
        rb = null;
        armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.Normal;
    }

    public override void UpdateState()
    {
        armStateMan.hitPoint = armStateMan.hitObject.transform.position;
        armStateMan.hitObject.transform.position = Vector3.MoveTowards(armStateMan.hitObject.transform.position, armStateMan.holdPoint.position, armStateMan.initialBeamSpeed * Time.deltaTime);
        armStateMan.initialBeamSpeed += armStateMan.beamSpeedAccelModifier / rb.mass;
        if (Vector3.Distance(armStateMan.hitObject.transform.position, armStateMan.holdPoint.position) <= 2f)
        {
            armStateMan.SwitchState(armStateMan.shootState);

        }
    }
}
