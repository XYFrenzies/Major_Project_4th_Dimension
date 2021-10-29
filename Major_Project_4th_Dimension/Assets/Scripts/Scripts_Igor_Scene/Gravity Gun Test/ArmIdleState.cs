using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmIdleState : ArmBaseState
{

    public ArmIdleState(ArmStateManager arm) : base(arm)
    {

    }

    public override void EnterState()
    {
        //    armStateMan.shootAction.performed += Shoot;
        //    armStateMan.shootAction.canceled += NotShoot;
        armStateMan.isShootingAnimationReady = false;
    }

    public override void ExitState()
    {
        // armStateMan.shotArm = false;

    }

    public override void UpdateState()
    {
        if (armStateMan.shotArm)
        {
            armStateMan.playerSM.animator.SetBool("IsShooting", true);
        }

        if (armStateMan.isShootingAnimationReady)
            armStateMan.SwitchState(armStateMan.shootState);
    }

    //public void Shoot(InputAction.CallbackContext context)
    //{
    //    armStateMan.shotArm = true;
    //}
    //private void NotShoot(InputAction.CallbackContext context)
    //{
    //    armStateMan.shotArm = false;
    //}
}
