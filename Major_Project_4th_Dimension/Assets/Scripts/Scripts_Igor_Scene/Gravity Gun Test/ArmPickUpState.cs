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

    }
}
