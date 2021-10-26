using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmPauseState : ArmBaseState
{
    float timer = 0f;
    int index = 1;
    float amount = 0f;
    float initialAmount = 0f;
    //bool secondTime = false;

    public ArmPauseState(ArmStateManager arm) : base(arm)
    {

    }
    public override void EnterState()
    {
        //Debug.Log("Entered pause state");

        armStateMan.lights[0].SetActive(true);
        armStateMan.lights[1].SetActive(false);
        armStateMan.lights[2].SetActive(false);
        armStateMan.lights[3].SetActive(false);
        amount = armStateMan.armCoolDownTime * 0.25f;
        initialAmount = amount;
    }

    public override void ExitState()
    {
        //Debug.Log("Exited pause state");
        amount = 0f;
        index = 1;
        timer = 0f;
        //secondTime = false;

    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;
        if (timer >= amount)
        {
            amount += initialAmount;
            if (index < 4)
            {
                armStateMan.lights[index].SetActive(true);
                armStateMan.lights[index - 1].SetActive(false);
            }
            //if (secondTime)



            index++;
            //secondTime = true;
        }


        if (timer >= armStateMan.armCoolDownTime)
        {
            armStateMan.SwitchState(armStateMan.shootState);
        }

    }
}
