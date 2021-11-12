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
        armStateMan.playerSM.flyToTarget = armStateMan.hitPoint;
        
        //////////
        //armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.HookShotFlying;
        armStateMan.playerSM.ChangeState(armStateMan.playerSM.flyState);
        //////////
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {
        if(armStateMan.playerSM.isFlying == false)
        {
            armStateMan.SwitchState(armStateMan.pauseState);
            armStateMan.playerSM.animator.SetBool("IsShooting", false);

        }
        else
        {
            armStateMan.armEffects.DrawLineRenderer();

        }
    }



}
