using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPutDownState : ArmBaseState
{
    public ArmPutDownState(ArmStateManager arm) : base(arm)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Entered Putdown state");
        armStateMan.hitObject.GetComponent<Rigidbody>().isKinematic = false;
        armStateMan.hitObject.transform.SetParent(null);
        armStateMan.lineRenderer.enabled = true;

    }

    public override void ExitState()
    {
        armStateMan.hitObject.layer = LayerMask.NameToLayer("Default");
        Rigidbody rb = armStateMan.hitObject.GetComponent<Rigidbody>();
        armStateMan.hitObject.GetComponent<Rigidbody>().isKinematic = false;
        rb.useGravity = true;
        //rb.velocity = Vector3.zero;

        armStateMan.isObjectHeld = false;

        armStateMan.hitObject = null;
        armStateMan.lineRenderer.enabled = false;
        armStateMan.initialBeamSpeed = armStateMan.holdInitialBeamSpeedValue;

        //place = false;
        //currentHookShotState = HookShotState.ReturnHand;
        armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.Normal;


    }

    public override void UpdateState()
    {
        //if (armStateMan.hitObject != null)
        //{
        //ShootHand();
        //armStateMan.hitPoint = armStateMan.hitObject.transform.position;

        armStateMan.hitObject.transform.position = Vector3.MoveTowards(armStateMan.hitObject.transform.position, armStateMan.hitPoint, armStateMan.initialBeamSpeed * Time.deltaTime);
        armStateMan.initialBeamSpeed += armStateMan.beamSpeedAccelModifier;
        //rb.MovePosition(target * 5f * Time.deltaTime);
        if (Vector3.Distance(armStateMan.hitObject.transform.position, armStateMan.hitPoint) <= 2f)
        {

            armStateMan.SwitchState(armStateMan.shootState);

        }
        //  }
    }
}
