using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class ArmStateManager : MonoBehaviour
{
    //public Cinemachine3rdPersonAim cineAimCam;
    //public float aimZoomAmount = 2f;
    private PlayerInput playerInput;
    public Transform aimTarget;
    //private InputAction aimAction;
    [HideInInspector]
    public LineRenderer lineRenderer;
    public float initialBeamSpeed = 1f;
    public float beamSpeedAccelModifier = 0.1f;
    [HideInInspector]
    public float holdInitialBeamSpeedValue;
    ArmBaseState currentState;
    [HideInInspector]
    public Vector3 hitPoint;
    [HideInInspector]
    public GameObject hitObject;
    public Transform shootPoint;
    public Transform holdPoint;
    [HideInInspector]
    public Camera cam;
    public float shootRange = 50f;
    public LayerMask layerMask;
    [HideInInspector]
    public PlayerControllerCinemachineLook2 player;
    [HideInInspector]
    public bool isObjectHeld = false;
    [HideInInspector]
    public Vector3 localPoint;
    [HideInInspector]
    public bool pullCheck = false;
    [HideInInspector]
    public bool pull = false;
    public GameObject grappleHandle;
    public SpringJoint springJoint;
    [HideInInspector]
    public GameObject newGrappleHandle;
    // States
    public ArmShootStateV2 shootState = null; // Remove V2 to go back to original
    public ArmGrappleState grappleState = null;
    public ArmPickUpState pickUpState = null;
    public ArmPullState pullState = null;
    public ArmPutDownState putDownState = null;
    public ArmHoldState holdState = null;


    //[HideInInspector]
    //public ArmThrowObjectState throwObjectState = new ArmThrowObjectState();

    public void Awake()
    {
        cam = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        springJoint = GetComponent<SpringJoint>();
        holdInitialBeamSpeedValue = initialBeamSpeed;
        player = GetComponent<PlayerControllerCinemachineLook2>();
        playerInput = GetComponent<PlayerInput>();
        //aimAction = playerInput.actions["Aim"];
        lineRenderer.enabled = false;
        shootState = new ArmShootStateV2(this); // Remove V2 to go back to original
        grappleState = new ArmGrappleState(this);
        pickUpState = new ArmPickUpState(this);
        pullState = new ArmPullState(this);
        putDownState = new ArmPutDownState(this);
        holdState = new ArmHoldState(this);
        aimTarget = GetComponent<AimTargetMove>().target.transform;
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
        Debug.Log("enter state test");
    }

    // Update is called once per frame
    void Update()
    {
        currentState.UpdateState();
        if (lineRenderer.enabled)
            DrawLineRenderer();

        
    }

    public void SwitchState(ArmBaseState state)
    {
        if (currentState != null)
            currentState.ExitState();

        currentState = state;

        if (currentState != null)
            currentState.EnterState();
    }

    public void DrawLineRenderer()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, shootPoint.position);
        if (!isObjectHeld)
            lineRenderer.SetPosition(1, hitPoint);
        else
        {
            lineRenderer.SetPosition(1, hitObject.transform.position);

        }
    }
}
