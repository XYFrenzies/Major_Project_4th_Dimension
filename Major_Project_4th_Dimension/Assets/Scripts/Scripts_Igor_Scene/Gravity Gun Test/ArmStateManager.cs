using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmStateManager : MonoBehaviour
{
    private PlayerInput playerInput;
    private InputAction hookshotAction;


    ArmBaseState currentState;
    
    public ArmShootState shootState;
    //[HideInInspector]
    //public ArmGrappleState grapplestate = new ArmGrappleState();
    //[HideInInspector]
    //public ArmPickUpState pickUpState = new ArmPickUpState();
    //[HideInInspector]
    //public ArmPutDownState putDownState = new ArmPutDownState();
    //[HideInInspector]
    //public ArmPullState pullState = new ArmPullState();
    //[HideInInspector]
    //public ArmThrowObjectState throwObjectState = new ArmThrowObjectState();

    public void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        hookshotAction = playerInput.actions["HookShot"];

        currentState = shootState;
        currentState.AwakeState(this);
    }

    public void OnEnable()
    {
        hookshotAction.performed += context => ThrowHookShot(context);

        currentState.OnEnableState(this);
    }

    public void OnDisable()
    {
        hookshotAction.performed -= context => ThrowHookShot(context);

        currentState.OnDisableState(this);

    }


    // Start is called before the first frame update
    public void Start()
    {
        currentState.EnterState(this);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState(this);
    }

    public void SwitchState(ArmBaseState state)
    {
        currentState = state;
        state.EnterState(this);
    }

    public void ThrowHookShot(InputAction.CallbackContext context)
    {
        Debug.Log("AAAAAAAAAAAAAAAAAAAAAAAA");
    }

    public void OnHookShot()
    {
        Debug.Log("BBBBBBBBBBBBBB");
    }
}
