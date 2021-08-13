
using UnityEngine;
using UnityEngine.InputSystem;



public class PlayerControllerNew : MonoBehaviour
{
    private const float NORMAL_FOV = 60f;
    private const float HOOKSHOT_FOV = 100f;

    [Header("Movement")]
    public float moveSpeed = 10.0f;
    //public float jumpForce = 10.0f;

    private Rigidbody rb;

    private Camera cam;
    private CameraFOV camFOV;

    [Header("Hook Shot")]
    public float hookShotRange = 50f;
    public float hookShotThrowSpeed = 70f;
    private float flyingSpeed;
    public float flyingSpeedMultiplier = 2f;
    public float hookShotMinSpeed = 10f;
    public float hookShotMaxSpeed = 40f;
    public float distanceToHookShotHitPoint = 1f;


    // Look
    [Header("Player Look")]
    public float lookSensitivity = 2.0f;
    public float minXLook = -60f;
    public float maxXLook = 60f;
    public Transform camAnchor;
    public bool invertXRotation; // For inverting the controls
    private float currentXRot;

    public Animator anim;
    private Vector2 m_Move;
    private Vector2 m_Look;
    [HideInInspector]
    public State currentState;

    [HideInInspector]
    public ChainShootStartAgain chainShoot;
    [HideInInspector]
    public Vector3 flyToTarget;


    public void OnMove(InputAction.CallbackContext context)
    {
        m_Move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        m_Look = context.ReadValue<Vector2>();
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
        camFOV = cam.GetComponent<CameraFOV>();
        currentState = State.Normal;
        Cursor.lockState = CursorLockMode.Locked;
        chainShoot = GetComponent<ChainShootStartAgain>();


    }


    // Update is called once per frame
    void FixedUpdate()
    {
        switch (currentState)
        {
            default:
            case State.Normal:
                Move(m_Move);
                Look(m_Look);
                break;

            case State.HookShotThrown:
                rb.velocity = Vector3.zero; // If player is moving while firing, player will continue to move for a short time.
                                            // This stops player from moving while hookshot if firing
                                            //chainShoot.HandleHookShotThrow();

                break;

            case State.HookShotFlying:
                camFOV.SetCameraFOV(HOOKSHOT_FOV);
                Fly(flyToTarget);

                break;

        }
    }

    public void Move(Vector2 direction)
    {
        Vector3 dir = transform.right * direction.x + transform.forward * direction.y;
        dir *= moveSpeed;
        dir.y = rb.velocity.y;
        rb.velocity = dir;
        //transform.position += dir * moveSpeed * Time.deltaTime;

        anim.SetFloat("xPos", direction.x);
        anim.SetFloat("yPos", direction.y);
        //anim.SetBool("IsLanding", false);

    }

    public void Look(Vector2 direction)
    {
        transform.eulerAngles += Vector3.up * direction.x * lookSensitivity;

        // Inverts the controls
        if (invertXRotation)
            currentXRot += direction.y * lookSensitivity;
        else
            currentXRot -= direction.y * lookSensitivity;

        currentXRot = Mathf.Clamp(currentXRot, minXLook, maxXLook); // Stops player from being able too look to far up and down

        Vector3 clampedAngle = camAnchor.eulerAngles;
        clampedAngle.x = currentXRot;

        camAnchor.eulerAngles = clampedAngle;

        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));


        RaycastHit hit;

        if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, hookShotRange))
        {

            if (hit.collider.CompareTag("CanHookShotTowards"))
            {
                GameEvents.current.GrapplePointVisible(hit.collider.GetComponent<GrapplePoint>().id);
            }

        }
        else
        {
            GameEvents.current.GrapplePointNotVisible();

        }
    }

    public void Fly(Vector3 target)
    {
        //rb.useGravity = false;
        anim.SetBool("IsFlying", true);
        flyingSpeed = Mathf.Clamp(Vector3.Distance(transform.position, target), hookShotMinSpeed, hookShotMaxSpeed);
        transform.position = Vector3.MoveTowards(transform.position, target, flyingSpeed * flyingSpeedMultiplier * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < distanceToHookShotHitPoint)
        {
            anim.SetBool("IsFlying", false);
            //rb.useGravity = true;
            chainShoot.fly = false;
            chainShoot.ReturnHand();
            currentState = State.Normal;

            camFOV.SetCameraFOV(NORMAL_FOV);

        }
    }

}
