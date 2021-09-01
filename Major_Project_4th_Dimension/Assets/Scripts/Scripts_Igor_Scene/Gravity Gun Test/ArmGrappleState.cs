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
    }

    public override void ExitState()
    {
        
    }

    public override void UpdateState()
    {

    }



}
