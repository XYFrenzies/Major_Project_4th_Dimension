using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmStateManager : MonoBehaviour
{



    ArmBaseState currentState;
    public Camera cam;
    public float hookShotRange = 50f;
    public LayerMask layerMask;
    public PlayerControllerCinemachineLook2 pc;

    // States
    public ArmShootState shootState = null;
    public ArmGrappleState grapplestate = null;
    public ArmPickUpState pickUpState = null;
    public ArmPullState pullState = null;
    public ArmPutDownState putDownState = null;


    //[HideInInspector]
    //public ArmThrowObjectState throwObjectState = new ArmThrowObjectState();

    public void Awake()
    {
        cam = Camera.main;

        pc = GetComponent<PlayerControllerCinemachineLook2>();


        shootState = new ArmShootState(this);
        grapplestate = new ArmGrappleState(this);
        pickUpState = new ArmPickUpState(this);
        pullState = new ArmPullState(this);
        putDownState = new ArmPutDownState(this);
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
