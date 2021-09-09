using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmPauseState : ArmBaseState
{
    float timer = 0;

    public ArmPauseState(ArmStateManager arm) : base(arm)
    {

    }
    public override void EnterState()
    {
        Debug.Log("Entered pause state");


    }

    public override void ExitState()
    {
        Debug.Log("Exited pause state");

    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);
        if (timer >= armStateMan.armCoolDownTime)
        {
            timer = 0;
            //canShoot = true;
            armStateMan.SwitchState(armStateMan.shootState);
        }
       
    }
}
