using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmStateManager : MonoBehaviour
{



    ArmBaseState currentState;
    
    public ArmShootState shootState = null;
    public ArmGrappleState grapplestate = null;

    public PlayerControllerCinemachineLook2 pc;


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

        pc = GetComponent<PlayerControllerCinemachineLook2>();


        shootState = new ArmShootState(this);
        grapplestate = new ArmGrappleState(this);

        

        //currentState = shootState;
        //currentState.AwakeState(this);
    }

    public void OnEnable()
    {

        
    }

    public void OnDisable()
    {

        

    }


    // Start is called before the first frame update
    public void Start()
    {
        SwitchState(shootState);
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
    }

    public void SwitchState(ArmBaseState state)
    {
        if (currentState != null)
            currentState.ExitState();

        currentState = state;
        
        if(currentState != null)
            currentState.EnterState();
    }

}
