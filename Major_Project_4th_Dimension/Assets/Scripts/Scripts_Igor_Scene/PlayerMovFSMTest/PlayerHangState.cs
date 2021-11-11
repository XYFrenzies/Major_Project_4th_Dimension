using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHangState : PlayerBaseState
{
    float timer = 0f;
    float amount = 0.2f;


    public PlayerHangState(PlayerStateManager psm) : base(psm)
    {

    }

    public override void EnterState()
    {
        //PSManager.animator.SetBool("IsDead", true);
        PSManager.arm.lineRenderer.enabled = true;

    }

    public override void ExitState()
    {
        timer = 0f;
        PSManager.rb.useGravity = true;
        PSManager.animator.SetBool("IsHanging", false);
        PSManager.arm.lineRenderer.enabled = false;

    }

    public override void UpdateLogic()
    {
        PSManager.lookAction.Disable();
        PSManager.moveAction.Disable();

        timer += Time.deltaTime;
        if (timer >= amount)
        {
            PSManager.ChangeState(PSManager.fallingState);

        }

    }

    public override void UpdatePhysics()
    {
        //PSManager.lookAction.Disable();

    }
}
