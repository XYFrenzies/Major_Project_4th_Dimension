using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations;

public class ArmStateManager : MonoBehaviour
{
    // Exposed properties
    public float pullForce = 120f;
    public float distanceFromPlayerToStopPlaying = 1f;
    //public float throwForce = 30f;
    public float shootRange = 50f;
    public float armCoolDownTime = 1f;
    public LayerMask holdObjectLayerMask;
    public float initialBeamSpeed = 1f;
    public float beamSpeedAccelModifier = 0.1f;
    [Space]
    public List<GameObject> lights;
    public Transform shootPoint;
    public Transform holdPoint;
    public GameObject grappleHandle;
    public SpringJoint springJoint;
    [HideInInspector]
    public float distToTarget90 = 0f;
    public float percentageOfDistToTarget = 0.9f;
    public GameObject blackHoleCentre; // pink one
    public GameObject realisticBlackHole;
    //public GameObject blackHoleDistortion;

    public float scaleModifier = 1f;
    public float modifier = 5f;

    public Vector3 startSize;

    public ShootingCheck shootCheck;
    public bool isShootingAnimationReady = false;

    [HideInInspector] public Transform aimTarget;
    [HideInInspector] public LineRenderer lineRenderer;
    [HideInInspector] public float holdInitialBeamSpeedValue;
    [HideInInspector] public ArmBaseState currentState;
    [HideInInspector] public Vector3 hitPoint;
    [HideInInspector] public GameObject hitObject;
    [HideInInspector] public Camera cam;
    [HideInInspector]
    public PlayerControllerCinemachineLook2 player;
    public PlayerStateManager playerSM;
    [HideInInspector]
    public bool isObjectHeld = false;
    [HideInInspector]
    public Vector3 localPoint;
    [HideInInspector]
    public bool pullCheck = false;
    [HideInInspector]
    public bool pull = false;
    [HideInInspector]
    public GameObject newGrappleHandle;
    [HideInInspector]
    public PlayerInput playerInput;
    [HideInInspector]
    public InputAction shootAction;
    [HideInInspector]
    public InputAction throwAction;
    [HideInInspector]
    public ParentConstraint parentConstraint;
    [HideInInspector]
    public ConstraintSource constraintSource;

    // States
    public ArmShootState shootState = null; // Remove V2 to go back to original
    public ArmGrappleState grappleState = null;
    public ArmPickUpState pickUpState = null;
    public ArmPullState pullState = null;
    public ArmPutDownState putDownState = null;
    public ArmPauseState pauseState = null;

    [HideInInspector]
    public ArmEffects armEffects;

    public void Awake()
    {
        cam = Camera.main;
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["HookShot"];
        throwAction = playerInput.actions["ThrowObject"];
        lineRenderer = GetComponent<LineRenderer>();
        springJoint = GetComponent<SpringJoint>();
        holdInitialBeamSpeedValue = initialBeamSpeed;
        player = GetComponent<PlayerControllerCinemachineLook2>();
        playerSM = GetComponent<PlayerStateManager>();
        lineRenderer.enabled = false;
        shootState = new ArmShootState(this); // Remove V2 to go back to original
        grappleState = new ArmGrappleState(this);
        pickUpState = new ArmPickUpState(this);
        pullState = new ArmPullState(this);
        putDownState = new ArmPutDownState(this);
        pauseState = new ArmPauseState(this);
        aimTarget = GetComponent<AimTargetMove>().target.transform;
        constraintSource.sourceTransform = holdPoint;
        constraintSource.weight = 1f;
        startSize = blackHoleCentre.transform.localScale;
        armEffects = GetComponent<ArmEffects>();
        //parentConstraint.AddSource(constraintSource);
        //parentConstraint.SetSource(0, constraintSource);
    }

    public void OnEnable()
    {

    }

    public void OnDisable()
    {

    }

    public void Start()
    {
        SwitchState(shootState);
    }


    void FixedUpdate()
    {
        currentState.UpdateState();
    }

    public void SwitchState(ArmBaseState state)
    {
        if (currentState != null)
            currentState.ExitState();

        currentState = state;

        if (currentState != null)
            currentState.EnterState();
    }

}
