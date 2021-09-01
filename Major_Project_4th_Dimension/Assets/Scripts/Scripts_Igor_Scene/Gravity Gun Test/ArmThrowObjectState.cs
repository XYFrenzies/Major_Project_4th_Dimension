using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmThrowObjectState : ArmBaseState
{
    public ArmThrowObjectState(ArmStateManager arm) : base(arm)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Entered ThrowObject state");
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }
}
