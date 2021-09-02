using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmStateManager : MonoBehaviour
{

    [HideInInspector]
    public LineRenderer lineRenderer;

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
    public ArmShootState shootState = null;
    public ArmGrappleState grappleState = null;
    public ArmPickUpState pickUpState = null;
    public ArmPullState pullState = null;
    public ArmPutDownState putDownState = null;


    //[HideInInspector]
    //public ArmThrowObjectState throwObjectState = new ArmThrowObjectState();

    public void Awake()
    {
        cam = Camera.main;
        lineRenderer = GetComponent<LineRenderer>();
        springJoint = GetComponent<SpringJoint>();

        player = GetComponent<PlayerControllerCinemachineLook2>();
        lineRenderer.enabled = false;
        shootState = new ArmShootState(this);
        grappleState = new ArmGrappleState(this);
        pickUpState = new ArmPickUpState(this);
        pullState = new ArmPullState(this);
        putDownState = new ArmPutDownState(this);

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
