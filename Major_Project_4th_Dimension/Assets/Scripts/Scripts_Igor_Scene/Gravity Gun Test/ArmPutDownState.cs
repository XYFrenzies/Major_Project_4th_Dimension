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

    }
}
