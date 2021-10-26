using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.Animations.Rigging;

public class PlayerMovementSM : PlayerStateManager
{
    //[HideInInspector] public PlayerInput playerInput;

    //[HideInInspector] public bool isFlying = false;

    //[Header("Movement")]
    //public float moveSpeed = 10.0f;
    //[Header("Hook Shot")]
    //public float hookShotRange = 50f;
    //public float hookShotThrowSpeed = 70f;
    //[HideInInspector]
    //public float flyingSpeed;
    //public float flyingSpeedMultiplier = 2f;
    //public float hookShotMinSpeed = 10f;
    //public float hookShotMaxSpeed = 40f;
    //public float distanceToHookShotHitPoint = 2f;

    //[HideInInspector] public InputAction moveAction;
    //[HideInInspector] public InputAction lookAction;
    //[HideInInspector] private InputAction interactAction;

    //[HideInInspector]
    //public ArmStateManager arm;

    //[Header("Other Stuff")]
    //public Animator animator;
    //public Rig armRig;
    //public Rig headRig;


    //[HideInInspector] public Camera cam;
    //[HideInInspector] public Vector3 direction = Vector3.zero;
    //[HideInInspector] public Vector2 inputs;
    //[HideInInspector] public Vector2 lookInputs;

    //[HideInInspector] public Rigidbody rb;
    //public CinemachineVirtualCamera vCam;


    //public GameEvent interacting;
    //[SerializeField] private GameEvent interactingConveyorSwitch;
    //bool isPlayerCloseEnough = false;
    //private bool isPlayerCloseToConveyorBelt = false;

    //[HideInInspector]
    //public Vector3 flyToTarget;


    //public PlayerIdleState idleState;
    //public PlayerMoveLookState moveLookState;
    //public PlayerFallingState fallingState;
    //public PlayerFlyState flyState;
    //public PlayerLandingState landingState;
    //public PlayerPickUpOrPutDownState pickUpOrPutDownState;
    //public PlayerPullingState pullingState;

    //public bool Grounded
    //{
    //    get
    //    {
    //        return animator.GetBool("IsGrounded");
    //    }
    //    set
    //    {
    //        animator.SetBool("IsGrounded", value);
    //    }
    //}

    //// Is the player falling
    //public bool Falling
    //{
    //    get
    //    {
    //        if (!Grounded)
    //            return true;
    //        else
    //            return false;
    //    }

    //}

    private void Awake()
    {
        //idleState = new PlayerIdleState(this);
        //moveLookState = new PlayerMoveLookState(this);
        //flyState = new PlayerFlyState(this);
        //fallingState = new PlayerFallingState(this);
        //landingState = new PlayerLandingState(this);
        //pickUpOrPutDownState = new PlayerPickUpOrPutDownState(this);
        //pullingState = new PlayerPullingState(this);

        //cam = Camera.main;

        //playerInput = GetComponent<PlayerInput>();
        //moveAction = playerInput.actions["Move"];
        //lookAction = playerInput.actions["Look"];
        //interactAction = playerInput.actions["Interact"];


        //rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
        //arm = GetComponent<ArmStateManager>();

    }

    //private void OnEnable()
    //{
    //    lookAction.Enable();
    //    moveAction.Enable();
    //    interactAction.Enable();
    //    interactAction.performed += Interact;

    //}

    //private void OnDisable()
    //{
    //    lookAction.Disable();
    //    moveAction.Disable();
    //    interactAction.Disable();
    //    interactAction.performed -= Interact;

    //}

    //private void Interact(InputAction.CallbackContext obj)
    //{
    //    if (isPlayerCloseEnough)
    //    {
    //        interacting.Raise();
    //        Debug.Log("Player interacted");
    //    }
    //    else if (isPlayerCloseToConveyorBelt)
    //    {
    //        interactingConveyorSwitch.Raise();
    //    }
    //}

    //public void ChangeIsPlayerCloseEnough()
    //{
    //    isPlayerCloseEnough = !isPlayerCloseEnough;
    //    Debug.Log(isPlayerCloseEnough);
    //}

    //public void PlayerIsCloseToSwitchConveyor()
    //{
    //    isPlayerCloseToConveyorBelt = !isPlayerCloseToConveyorBelt;
    //}

    //protected override PlayerBaseState GetInitialState()
    //{
    //    return moveLookState;
    //}

    //public bool GroundCheck()
    //{
    //    Ray ray = new Ray(transform.position, Vector3.down); // Shoot a ray down
    //    RaycastHit hit;

    //    if (Physics.Raycast(ray, out hit, 1.5f)) // If the ray hits the ground
    //    {
    //        Grounded = true; // is the player on the ground?
    //        //animator.SetBool("IsGrounded", false);
    //        return true;
    //    }

    //    Grounded = false;
    //    return false;

    //}

    //public void DeathCheck()
    //{
    //    animator.SetBool("IsDead", true);
    //}
    //public void NotDying()
    //{
    //    animator.SetBool("IsDead", false);
    //}

    //public void CalculateMove()
    //{
    //    var camForward = cam.transform.forward;
    //    camForward.y = 0.0f;
    //    camForward.Normalize();

    //    var camRight = cam.transform.right;
    //    camRight.y = 0.0f;
    //    camRight.Normalize();

    //    direction.x = 0f;
    //    direction.z = 0f;

    //    inputs = moveAction.ReadValue<Vector2>();

    //    direction += inputs.y * camForward;
    //    direction += inputs.x * camRight;

    //    //pmStateMan.animator.SetFloat("xPos", pmStateMan.inputs.x, 0.3f, Time.deltaTime);
    //    //pmStateMan.animator.SetFloat("yPos", pmStateMan.inputs.y, 0.3f, Time.deltaTime);


    //}

    //public void Move()
    //{
    //    rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

    //}

    //public void Landed()
    //{
    //    Debug.Log("Landed");
    //    animator.SetBool("IsLanding", false);
    //    ChangeState(idleState);
    //}
}
