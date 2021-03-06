using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.Animations.Rigging;
using System;
using UnityEngine.SceneManagement;

public class PlayerStateManager : MonoBehaviour
{
    public PlayerBaseState currentState;

    [HideInInspector] public PlayerInput playerInput;

    [HideInInspector] public bool isFlying = false;

    [Header("Movement")]
    public float moveSpeed = 10.0f;
    [Header("Hook Shot")]
    public float hookShotRange = 50f;
    public float hookShotThrowSpeed = 70f;
    [HideInInspector]
    public float flyingSpeed;
    public float flyingSpeedMultiplier = 2f;
    public float hookShotMinSpeed = 10f;
    public float hookShotMaxSpeed = 40f;
    public float distanceToHookShotHitPoint = 2f;

    [HideInInspector] public InputAction moveAction;
    [HideInInspector] public InputAction lookAction;
    [HideInInspector] private InputAction interactAction;
    [HideInInspector] private InputAction oasisAction;

    [HideInInspector]
    public ArmStateManager arm;

    [Header("Other Stuff")]
    public Animator animator;
    public Rig armRig;
    public Rig headRig;
    public Animator conveyorSwitchAnimator;

    private IEnumerator myRotCo;

    static public PlayerStateManager player;
    [HideInInspector] public Camera cam;
    [HideInInspector] public Vector3 direction = Vector3.zero;
    [HideInInspector] public Vector2 inputs;
    [HideInInspector] public Vector2 lookInputs;

    [HideInInspector] public Rigidbody rb;
    public CinemachineVirtualCamera cinemachineVCam;
    public CinemachineVirtualCamera cinemachineVCamAim;

    [HideInInspector] public CinemachinePOV vCam;
    [HideInInspector] public CinemachinePOV vCamAim;


    public GameEvent interacting;
    [SerializeField] private GameEvent interactingConveyorSwitch;
    bool isPlayerCloseEnough = false;
    private bool isPlayerCloseToConveyorBelt = false;
    public bool hasLanded = false;
    public bool isPushing = false;

    [HideInInspector]
    public Vector3 flyToTarget;

    public PlayerIdleState idleState = null;
    public PlayerMoveLookState moveLookState = null;
    public PlayerFallingState fallingState = null;
    public PlayerFlyState flyState = null;
    public PlayerLandingState landingState = null;
    public PlayerPickUpOrPutDownState pickUpOrPutDownState = null;
    public PlayerPullingState pullingState = null;
    public PlayerMissState missState = null;
    public PlayerDeathState deathState = null;
    public PlayerHangState hangState = null;
    [SerializeField] private GameObject m_gameUI = null;
    [HideInInspector]public bool conveyorPressed = false;

    [HideInInspector] public AudioSource source;
    private void OnEnable()
    {
        lookAction.Enable();
        moveAction.Enable();
        interactAction.Enable();
        interactAction.performed += Interact;
        oasisAction.Enable();
        oasisAction.performed += LoadOasisScene;
    }

    private void OnDisable()
    {
        lookAction.Disable();
        moveAction.Disable();
        interactAction.Disable();
        interactAction.performed -= Interact;
        oasisAction.Disable();
        oasisAction.performed -= LoadOasisScene;
    }

    private void LoadOasisScene(InputAction.CallbackContext obj)
    {
        SceneManager.LoadScene("Oasis");
    }

    // Is the player falling
    private void Awake()
    {
        idleState = new PlayerIdleState(this);
        moveLookState = new PlayerMoveLookState(this);
        flyState = new PlayerFlyState(this);
        fallingState = new PlayerFallingState(this);
        landingState = new PlayerLandingState(this);
        pickUpOrPutDownState = new PlayerPickUpOrPutDownState(this);
        pullingState = new PlayerPullingState(this);
        missState = new PlayerMissState(this);
        deathState = new PlayerDeathState(this);
        hangState = new PlayerHangState(this);
        player = this;
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;

        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        interactAction = playerInput.actions["Interact"];
        oasisAction = playerInput.actions["Oasis"];

        rb = GetComponent<Rigidbody>();
        //animator = GetComponent<Animator>();
        arm = GetComponent<ArmStateManager>();

        //cinemachineVCam = GetComponent<CinemachineVirtualCamera>();
        vCam = cinemachineVCam.GetCinemachineComponent<CinemachinePOV>();
        vCamAim = cinemachineVCamAim.GetCinemachineComponent<CinemachinePOV>();
        //vCam.m_HorizontalAxis.m_MaxSpeed = 2f;
        source = GetComponent<AudioSource>();
    }

    public bool Grounded
    {
        get
        {
            return animator.GetBool("IsGrounded");
        }
        set
        {
            animator.SetBool("IsGrounded", value);
        }
    }

    public bool Falling
    {
        get
        {
            if (!Grounded)
                return true;
            else
                return false;
        }

    }


    void Start()
    {
        currentState = GetInitialState();
        if (currentState != null)
            currentState.EnterState();
    }

    void Update()
    {
        if (currentState != null)
            currentState.UpdateLogic();
        //Debug.Log(currentState);
    }

    void FixedUpdate()
    {
        if (currentState != null)
            currentState.UpdatePhysics();



    }

    public void ChangeState(PlayerBaseState nextState)
    {
        if (currentState != null)
            currentState.ExitState();

        currentState = nextState;

        if (currentState != null)
            currentState.EnterState();
    }

    protected virtual PlayerBaseState GetInitialState()
    {
        return idleState;
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        if (isPlayerCloseEnough)
        {
            interacting.Raise();
            Debug.Log("Player interacted");
        }
        else if (!conveyorPressed)
        {
            interactingConveyorSwitch.Raise();
            conveyorPressed = true;
            m_gameUI.transform.Find("Press E to interact").gameObject.SetActive(false);
            conveyorSwitchAnimator.SetBool("IsConveyorOn", true);
        }
    }

    public void ChangeIsPlayerCloseEnough()
    {
        isPlayerCloseEnough = !isPlayerCloseEnough;
        //Debug.Log(isPlayerCloseEnough);
    }

    public void PlayerIsCloseToSwitchConveyor()
    {
        //if (!conveyorPressed)
        isPlayerCloseToConveyorBelt = !isPlayerCloseToConveyorBelt;
        //if (conveyorPressed)
        //isPlayerCloseToConveyorBelt = false;
    }

    public bool GroundCheck()
    {
        Ray ray = new Ray(transform.position, Vector3.down); // Shoot a ray down

        RaycastHit hit;

        if (Physics.SphereCast(ray, 0.25f, out hit, 1.5f)) // If the ray hits the ground
        {
            Grounded = true; // is the player on the ground?
            //animator.SetBool("IsGrounded", false);
            return true;
        }

        Grounded = false;
        return false;

    }

    public void DeathCheck()
    {
        //animator.SetBool("IsDead", true);
        ChangeState(deathState);
    }
    public void NotDying()
    {
        animator.SetBool("IsDead", false);
    }

    public void CalculateMove()
    {
        var camForward = cam.transform.forward;
        camForward.y = 0.0f;
        camForward.Normalize();

        var camRight = cam.transform.right;
        camRight.y = 0.0f;
        camRight.Normalize();

        direction.x = 0f;
        direction.z = 0f;

        inputs = moveAction.ReadValue<Vector2>();

        direction += inputs.y * camForward;
        direction += inputs.x * camRight;

        animator.SetFloat("xPos", inputs.x, 0.2f, Time.deltaTime);
        animator.SetFloat("yPos", inputs.y, 0.2f, Time.deltaTime);

        animator.SetFloat("Mouse", lookAction.ReadValue<Vector2>().x);

        //Debug.Log("xPos: " + inputs.x + "| yPos: " + inputs.y + "| Mouse: " + lookAction.ReadValue<Vector2>().x);

        animator.SetBool("PlayerStill", (inputs.x == 0 && inputs.y == 0));

    }

    public void Move()
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);

    }
    public void RotatePlayerModel()
    {
        //pmStateMan.lookInputs = pmStateMan.lookAction.ReadValue<Vector2>().normalized;

        transform.rotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);
    }


    IEnumerator StopRotationAnim()
    {
        yield return new WaitForSeconds(0.2f);
        if (lookAction.ReadValue<Vector2>().magnitude < 0.1)
        {
            animator.SetBool("IsRotLeft", false);
            animator.SetBool("IsRotRight", false);
        }
    }

    public void CheckIfRotating()
    {
        //if (lookAction.ReadValue<Vector2>().x == 0f)
        //    animator.SetBool("IsRotLeft", false); animator.SetBool("IsRotRight", false);
        if (lookAction.ReadValue<Vector2>().magnitude < 0.001f)
        {
            //myRotCo = StopRotationAnim();
            //StartCoroutine(StopRotationAnim());
            animator.SetBool("IsRotLeft", false);
            animator.SetBool("IsRotRight", false);
        }

        if (lookAction.ReadValue<Vector2>().magnitude > 0.002f)
        {
            if (lookAction.ReadValue<Vector2>().x < 0f)
            {
                animator.SetBool("IsRotLeft", true);
                animator.SetBool("IsRotRight", false);
                //Debug.Log(StationaryMouseCheck());
                //StopCoroutine(myRotCo);
            }

            else if (lookAction.ReadValue<Vector2>().x > 0f)
            {
                animator.SetBool("IsRotRight", true);
                animator.SetBool("IsRotLeft", false);
                //StopCoroutine(myRotCo);
            }
        }


        //if (lookAction.ReadValue<Vector2>().x != 0f)
        //if (lookAction.ReadValue<Vector2>().x < 0.5f) //left
        //{
        //    animator.SetBool("IsRotLeft", true);
        //    animator.SetBool("IsRotRight", false);
        //}
        //else if (lookAction.ReadValue<Vector2>().magnitude > 0.1f) //right
        //{

        //    animator.SetBool("IsRotLeft", false);
        //    animator.SetBool("IsRotRight", true);             
        //}

    }
    double limitCountDown = 0.1;
    double countDown = 0;
    float xAccumulator;
    const float Snappiness = 10.0f;

    public bool StationaryMouseCheck()
    {
        float inputX = lookAction.ReadValue<Vector2>().x;
        xAccumulator = Mathf.Lerp(xAccumulator, inputX, Snappiness * Time.deltaTime);

        if (xAccumulator < 0.0001)
        {
            countDown += Time.deltaTime;
        }
        else if (xAccumulator > 0.0001)
            countDown = 0;
        if (countDown >= limitCountDown)
        {

            return false;
        }
        return true;
    }

    public void LookAtGrapplePoints()
    {
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));


        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, hookShotRange))
        {

            if (hit.collider.CompareTag("CanHookShotTowards"))
            {
                GameEvents.current.GrapplePointVisible(hit.collider.GetComponent<GrapplePoint>().id);
            }
            else
            {
                if (GameEvents.current != null)
                    GameEvents.current.GrapplePointNotVisible();

            }

        }
        else
        {
            if (GameEvents.current != null)
                GameEvents.current.GrapplePointNotVisible();

        }
    }


    private void OnCollisionStay(Collision collision)
    {
        Ray ray = new Ray(transform.position, transform.forward); // Shoot a ray down
        RaycastHit hit;
        Debug.DrawRay(ray.origin, ray.direction * 5f, Color.green);

        if (collision.collider.CompareTag("BigPullObject"))
            if (inputs.y > 0f)
            {
                if (Physics.SphereCast(ray, 0.25f, out hit, 1.5f))
                {
                    // if (!hit.collider.CompareTag("BigPullObject"))
                    collision.collider.attachedRigidbody.constraints = RigidbodyConstraints.FreezeRotation;
                    animator.SetBool("IsPushing", true);
                    isPushing = true;

                }
            }
            else
            {
                isPushing = false;
                animator.SetBool("IsPushing", false);
                collision.collider.attachedRigidbody.constraints = RigidbodyConstraints.None;
                collision.collider.attachedRigidbody.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;


            }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("BigPullObject"))
        {
            animator.SetBool("IsPushing", false);
            isPushing = false;

        }
    }
}


