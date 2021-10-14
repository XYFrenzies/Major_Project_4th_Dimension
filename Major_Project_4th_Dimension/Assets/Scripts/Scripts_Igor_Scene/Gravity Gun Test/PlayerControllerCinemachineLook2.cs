using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Animations.Rigging;
using System;

public class PlayerControllerCinemachineLook2 : MonoBehaviour
{
    private PlayerInput playerInput;
    [HideInInspector]
    public bool isFlying = false;

    [Header("Movement")]
    public float moveSpeed = 10.0f;
    private Rigidbody rb;

    private Camera cam;
    private Vector3 direction = Vector3.zero;
    private Vector2 inputs;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction interactAction;

    [Header("Hook Shot")]
    public float hookShotRange = 50f;
    public float hookShotThrowSpeed = 70f;
    private float flyingSpeed;
    public float flyingSpeedMultiplier = 2f;
    public float hookShotMinSpeed = 10f;
    public float hookShotMaxSpeed = 40f;
    public float distanceToHookShotHitPoint = 2f;

    [Header("Other Stuff")]
    public Animator animator;
    //private Vector2 m_Move;
    public Rig armRig;
    public Rig headRig;
    //public GameEvent interactedEvent;

    ArmStateManager arm;
    public GameEvent interacting;
    [SerializeField] private GameEvent interactingConveyorSwitch;
    bool isPlayerCloseEnough = false;
    private bool isPlayerCloseToConveyorBelt = false;

    [HideInInspector]
    public State currentState;

    //[HideInInspector]
    //public GravArm chainShoot;
    [HideInInspector]
    public Vector3 flyToTarget;

    bool isHookThrown = false;

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

    // Is the player falling
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

    public enum State
    {
        Normal,
        HookShotThrown,
        HookShotFlying,
        HookShotPullObjTowards,
        HookShotMissed
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cam = Camera.main;
        currentState = State.Normal;
        Cursor.lockState = CursorLockMode.Locked;
        //chainShoot = GetComponent<GravArm>();
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        //var lookAct = new InputAction();
        //lookAct.AddBinding("<Gamepad/rightStick>").WithProcessor("scaleVector2(x=20,y=20)");
        interactAction = playerInput.actions["Interact"];
        arm = GetComponent<ArmStateManager>();
    }

    private void OnEnable()
    {

        interactAction.performed += Interact;
    }

    private void Interact(InputAction.CallbackContext obj)
    {
        if (isPlayerCloseEnough)
        {
            interacting.Raise();
            Debug.Log("Player interacted");
        }
        else if (isPlayerCloseToConveyorBelt)
        {
            interactingConveyorSwitch.Raise();
        }
    }

    public void ChangeIsPlayerCloseEnough()
    {
        isPlayerCloseEnough = !isPlayerCloseEnough;
        Debug.Log(isPlayerCloseEnough);
    }

    public void PlayerIsCloseToSwitchConveyor()
    {
        isPlayerCloseToConveyorBelt = !isPlayerCloseToConveyorBelt;
    }

    private void OnDisable()
    {
        interactAction.performed -= Interact;

    }

    private void Update()
    {
        CalculateMove();
        switch (currentState)
        {
            default:
            case State.Normal:
                moveAction.Enable();
                Look();
                isHookThrown = false;
                break;

            case State.HookShotThrown:
                rb.velocity = Vector3.zero; // If player is moving while firing, player will continue to move for a short time.
                moveAction.Disable();
                lookAction.Disable();
                // This stops player from moving while hookshot if firing
                //chainShoot.HandleHookShotThrow();
                isHookThrown = true;
                break;

        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        switch (currentState)
        {
            default:
            case State.Normal:
                moveAction.Enable();

                Move();
                isHookThrown = false;
                break;
            case State.HookShotThrown:
                rb.velocity = Vector3.zero;
                lookAction.Disable();
                moveAction.Disable();
                isHookThrown = true;

                break;
            case State.HookShotFlying:
                Fly(flyToTarget);

                break;

        }
        GroundCheck();
        Debug.Log(Grounded);
    }

    public void Move()
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
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

        //direction = Vector3.zero;
        inputs = moveAction.ReadValue<Vector2>();
        //direction.x = inputs.x;
        //direction.z = inputs.y;
        //direction.y = 0f;
        //if (direction != Vector3.zero)
        //    direction = direction.x * cam.transform.right.normalized + direction.z * cam.transform.forward.normalized;
        direction += inputs.y * camForward;
        direction += inputs.x * camRight;


        animator.SetFloat("xPos", inputs.x, 0.3f, Time.deltaTime);
        animator.SetFloat("yPos", inputs.y, 0.3f, Time.deltaTime);
        //anim.SetBool("IsLanding", false);

    }

    public void Look()
    {
        if (isHookThrown == false)
            transform.rotation = Quaternion.Euler(0, cam.transform.eulerAngles.y, 0);



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
                GameEvents.current.GrapplePointNotVisible();

            }

        }
        else
        {
            GameEvents.current.GrapplePointNotVisible();

        }
    }

    public void Fly(Vector3 target)
    {
        isFlying = true;
        rb.useGravity = false;
        animator.SetBool("IsFlying", true);
        flyingSpeed = Mathf.Clamp(Vector3.Distance(transform.position, target), hookShotMinSpeed, hookShotMaxSpeed);
        transform.position = Vector3.MoveTowards(transform.position, target, flyingSpeed * flyingSpeedMultiplier * Time.deltaTime);
        //armRig.weight = 0f;
        headRig.weight = 0f;
        //if (Vector3.Distance(transform.position, target) < arm.distToTarget90)
        //{

        //    //animator.SetBool("IsFlying", false);
        //}

        if (Vector3.Distance(transform.position, target) < distanceToHookShotHitPoint)
        {
            animator.SetBool("IsFlying", false);
            rb.useGravity = true;
            //chainShoot.fly = false;
            //chainShoot.ReturnHand();
            currentState = State.Normal;
            isFlying = false;
            arm.lineRenderer.enabled = false;
            //armRig.weight = 1f;
            headRig.weight = 1f;
            //Debug.Log(chainShoot.currentHookShotState);

        }
    }

    bool GroundCheck()
    {
        Ray ray = new Ray(transform.position, Vector3.down); // Shoot a ray down
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 2f)) // If the ray hits the ground
        {
            Grounded = true; // is the player on the ground?
            //animator.SetBool("IsGrounded", false);
            return true;
        }

        Grounded = false;
        return false;

    }

}
