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
    bool alreadyPlayedSound = false;

    public ArmPauseState(ArmStateManager arm) : base(arm)
    {

    }
    public override void EnterState()
    {

        armStateMan.shotArm = false;

        if (armStateMan.lights.Count != 0)
        {
            armStateMan.lights[0].SetActive(true);
            armStateMan.lights[1].SetActive(false);
            armStateMan.lights[2].SetActive(false);
            armStateMan.lights[3].SetActive(false);
        }
        amount = armStateMan.armCoolDownTime * 0.25f;
        initialAmount = amount;

    }

    public override void ExitState()
    {
        //Debug.Log("Exited pause state");
        amount = 0f;
        index = 1;
        timer = 0f;
        alreadyPlayedSound = false;
    }

    public override void UpdateState()
    {
        if (!armStateMan.source.isPlaying && !alreadyPlayedSound)
        { 
            SoundPlayer.Instance.PlaySoundEffect("Recharge", armStateMan.source);
            alreadyPlayedSound = true;
        }

        timer += Time.deltaTime;
        if (timer >= amount)
        {
            amount += initialAmount;
            if (index < 4)
            {
                if (armStateMan.lights.Count != 0)
                {
                    armStateMan.lights[index].SetActive(true);
                    armStateMan.lights[index - 1].SetActive(false);
                }
            }

            index++;

        }


        if (timer >= armStateMan.armCoolDownTime)
        {
            armStateMan.SwitchState(armStateMan.idleState);
        }

    }
}
