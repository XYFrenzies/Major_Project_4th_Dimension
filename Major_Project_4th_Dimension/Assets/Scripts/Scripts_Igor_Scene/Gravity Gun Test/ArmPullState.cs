using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmPullState : ArmBaseState
{
    public ArmPullState(ArmStateManager arm) : base(arm)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Entered Pull state");
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }
}
