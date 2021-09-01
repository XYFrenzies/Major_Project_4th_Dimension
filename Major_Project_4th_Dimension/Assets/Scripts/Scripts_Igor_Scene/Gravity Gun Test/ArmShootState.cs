using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmShootState : ArmBaseState
{
    private PlayerInput playerInput;
    private InputAction hookshotAction;
    

    public ArmShootState(ArmStateManager arm) : base(arm)
    {
        
    }


    public override void EnterState()
    {
        playerInput = armStateMan.GetComponent<PlayerInput>();

        hookshotAction = playerInput.actions["HookShot"];
        hookshotAction.performed += context => ThrowHookShot(context);
        // called once when switch from some other state to this state.

        Debug.Log("Shoot enter");
        
    }

    public override void ExitState()
    {
        Debug.Log("Shoot state exited");
        // called once when switching from this state to another state
        hookshotAction.performed -= context => ThrowHookShot(context);

    }

    public override void UpdateState()
    {
        // armStateMan.pc.look()
    }


    public void ThrowHookShot(InputAction.CallbackContext context)
    {
        Debug.Log("Fired hook shot");
        //armStateMan.SwitchState(armStateMan.shootState);
        OnHookShotGrab();
    }


    public void OnHookShotGrab()
    {
        armStateMan.SwitchState(armStateMan.grapplestate);
    }

}
