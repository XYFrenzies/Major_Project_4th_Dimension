
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerControllerCinemachineLook2 : MonoBehaviour
{
    private PlayerInput playerInput;
    [HideInInspector]
    public bool isFlying = false;

    [Header("Movement")]
    public float moveSpeed = 10.0f;

    //public CinemachineTouchInputMapper cin;
    private Rigidbody rb;

    private Camera cam;
    private Vector3 direction = Vector3.zero;
    private Vector2 inputs;
    private InputAction moveAction;
    private InputAction lookAction;

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


    [HideInInspector]
    public State currentState;

    //[HideInInspector]
    //public GravArm chainShoot;
    [HideInInspector]
    public Vector3 flyToTarget;

    bool isHookThrown = false;

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

    }

    public void Move()
    {
        rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
    }

    public void CalculateMove()
    {
        direction = Vector3.zero;
        inputs = moveAction.ReadValue<Vector2>();
        direction.x = inputs.x;
        direction.z = inputs.y;
        if (direction != Vector3.zero)
            //transform.forward = direction;
            direction = direction.x * cam.transform.right.normalized + direction.z * cam.transform.forward.normalized;
        direction.y = 0f;

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

        if (Vector3.Distance(transform.position, target) < distanceToHookShotHitPoint)
        {
            animator.SetBool("IsFlying", false);
            rb.useGravity = true;
            //chainShoot.fly = false;
            //chainShoot.ReturnHand();
            currentState = State.Normal;
            isFlying = false;
            //Debug.Log(chainShoot.currentHookShotState);

        }
    }



}
