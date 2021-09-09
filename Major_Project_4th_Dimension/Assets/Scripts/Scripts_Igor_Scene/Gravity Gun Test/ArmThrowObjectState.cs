using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmThrowObjectState : ArmBaseState
{
    private PlayerInput playerInput;
    private InputAction throwAction;

    public ArmThrowObjectState(ArmStateManager arm) : base(arm)
    {

    }

    public override void EnterState()
    {
        Debug.Log("Entered ThrowObject state");
        playerInput = armStateMan.GetComponent<PlayerInput>();
        throwAction = playerInput.actions["ThrowObject"];

        throwAction.performed += context => ThrowObject();
    }

    public override void ExitState()
    {
        throwAction.performed -= context => ThrowObject();

    }

    public override void UpdateState()
    {

    }

    public void ThrowObject()
    {

    }
}
