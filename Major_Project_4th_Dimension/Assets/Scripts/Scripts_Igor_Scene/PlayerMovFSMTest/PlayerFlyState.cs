using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlyState : PlayerBaseState
{
    //private PlayerMovementSM pmStateMan;

    public PlayerFlyState(PlayerStateManager psm) : base(psm)
    {
        //pmStateMan = stateMachine;
    }

    public override void EnterState()
    {
        Debug.Log("Entered fly state");
    }

    public override void ExitState()
    {
        Debug.Log("Exited fly state");

    }

    public override void UpdateLogic()
    {

    }

    public override void UpdatePhysics()
    {
        Fly(PSManager.flyToTarget);
    }

    public void Fly(Vector3 target)
    {
        PSManager.isFlying = true;
        PSManager.rb.useGravity = false;
        PSManager.animator.SetBool("IsFlying", true);
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
            PSManager.animator.SetBool("IsFlying", false);
            PSManager.rb.useGravity = true;
            //chainShoot.fly = false;
            //chainShoot.ReturnHand();
            //pmStateMan.currentState = State.Normal;
            PSManager.ChangeState(PSManager.fallingState);
            PSManager.isFlying = false;
            PSManager.arm.lineRenderer.enabled = false;
            //armRig.weight = 1f;
            PSManager.headRig.weight = 1f;
            //Debug.Log(chainShoot.currentHookShotState);

        }
    }
}
