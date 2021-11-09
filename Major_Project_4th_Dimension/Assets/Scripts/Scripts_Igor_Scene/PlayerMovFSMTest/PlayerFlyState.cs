using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyState : PlayerBaseState
{
    public PlayerFlyState(PlayerStateManager psm) : base(psm)
    {

    }

    public override void EnterState()
    {

        PSManager.animator.SetBool("IsFlying", true);
        PSManager.arm.lineRenderer.enabled = true;

    }

    public override void ExitState()
    {

        PSManager.animator.SetBool("IsFlying", false);
        PSManager.arm.lineRenderer.enabled = false;
        PSManager.isFlying = false;
        //PSManager.headRig.weight = 1f;
        PSManager.rb.useGravity = true;
        PSManager.lookAction.Enable();

    }

    public override void UpdateLogic()
    {
        PSManager.lookAction.Disable();
        Fly(PSManager.flyToTarget);
    }

    public override void UpdatePhysics()
    {
    }

    public void Fly(Vector3 target)
    {
        PSManager.isFlying = true;
        PSManager.rb.useGravity = false;
        PSManager.flyingSpeed = Mathf.Clamp(Vector3.Distance(PSManager.transform.position, target), PSManager.hookShotMinSpeed, PSManager.hookShotMaxSpeed);
        PSManager.transform.position = Vector3.MoveTowards(PSManager.transform.position, target, PSManager.flyingSpeed * PSManager.flyingSpeedMultiplier * Time.deltaTime);
        //armRig.weight = 0f;
        PSManager.headRig.weight = 0f;
        //if (Vector3.Distance(transform.position, target) < arm.distToTarget90)
        //{

        //    //animator.SetBool("IsFlying", false);
        //}

        if (Vector3.Distance(PSManager.transform.position, target) < PSManager.distanceToHookShotHitPoint)
        {
            //chainShoot.fly = false;
            //chainShoot.ReturnHand();
            //pmStateMan.currentState = State.Normal;
            PSManager.ChangeState(PSManager.fallingState);
            //armRig.weight = 1f;
            //Debug.Log(chainShoot.currentHookShotState);

        }
    }
}
