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
        armStateMan.player.flyToTarget = armStateMan.hitPoint;
        armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.HookShotFlying;
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        if(armStateMan.player.isFlying == false)
        {
            armStateMan.SwitchState(armStateMan.shootState);
        }
    }



}
