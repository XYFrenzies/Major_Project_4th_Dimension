using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPickUpState : ArmBaseState
{
    public ArmPickUpState(ArmStateManager arm) : base(arm)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Entered Pickup state");
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        Rigidbody rb = armStateMan.hitObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        armStateMan.hitObject.transform.position = Vector3.MoveTowards(armStateMan.hitObject.transform.position, armStateMan.holdPoint.position, 50f * Time.deltaTime);
        //ReturnHand();
        if (Vector3.Distance(armStateMan.hitObject.transform.position, armStateMan.holdPoint.position) <= 2f)
        {
            armStateMan.hitObject.layer = LayerMask.NameToLayer("Hold");
            armStateMan.hitObject.GetComponent<Rigidbody>().isKinematic = true;
            armStateMan.hitObject.transform.SetParent(armStateMan.holdPoint);
            armStateMan.isObjectHeld = true;

            armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.Normal;
            armStateMan.SwitchState(armStateMan.shootState);

        }
    }
}
