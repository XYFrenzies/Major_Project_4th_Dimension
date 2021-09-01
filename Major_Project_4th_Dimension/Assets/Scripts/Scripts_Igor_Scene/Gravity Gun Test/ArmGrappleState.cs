using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmGrappleState : ArmBaseState
{

    public ArmGrappleState(ArmStateManager arm) : base(arm)
    {

    }
        
    public override void EnterState()
    {
        Debug.Log("Entered grapple state");
        armStateMan.pc.flyToTarget = armStateMan.hitPoint;
        armStateMan.pc.currentState = PlayerControllerCinemachineLook2.State.HookShotFlying;
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        if(armStateMan.pc.isFlying == false)
        {
            armStateMan.SwitchState(armStateMan.shootState);
        }
    }



}
