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
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {
        if (armStateMan.hitObject != null)
        {
            armStateMan.hitObject.GetComponent<Rigidbody>().isKinematic = false;
            armStateMan.hitObject.transform.SetParent(null);
            //ShootHand();
            armStateMan.hitObject.transform.position = Vector3.MoveTowards(armStateMan.hitObject.transform.position, armStateMan.hitPoint, 50f * Time.deltaTime);
            //rb.MovePosition(target * 5f * Time.deltaTime);
            if (Vector3.Distance(armStateMan.hitObject.transform.position, armStateMan.hitPoint) <= 2f)
            {
                
                armStateMan.hitObject.layer = LayerMask.NameToLayer("Default");
                Rigidbody rb = armStateMan.hitObject.GetComponent<Rigidbody>();
                armStateMan.hitObject.GetComponent<Rigidbody>().isKinematic = false;
                rb.useGravity = true;
                armStateMan.isObjectHeld = false;

                armStateMan.hitObject = null;
                //place = false;
                //currentHookShotState = HookShotState.ReturnHand;
                armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.Normal;

                armStateMan.SwitchState(armStateMan.shootState);

            }
        }
    }
}
