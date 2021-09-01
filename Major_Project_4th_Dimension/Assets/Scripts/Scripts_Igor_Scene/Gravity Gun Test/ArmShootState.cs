using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmShootState : ArmBaseState
{

    public override void AwakeState(ArmStateManager arm)
    {
        Debug.Log("Shoot awake");

    }


    public override void OnEnableState(ArmStateManager arm)
    {
        Debug.Log("Shoot enable");


    }

    public override void OnDisableState(ArmStateManager arm)
    {

        Debug.Log("Shoot disable");

    }

    public override void EnterState(ArmStateManager arm)
    {

        Debug.Log("Shoot enter");

    }

    public override void UpdateState(ArmStateManager arm)
    {

    }
}
