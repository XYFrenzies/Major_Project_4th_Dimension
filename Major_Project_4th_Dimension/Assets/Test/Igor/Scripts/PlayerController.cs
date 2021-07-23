using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.Events;


public class PlayerController : MonoBehaviour
{
    private const float NORMAL_FOV = 60f;
    private const float HOOKSHOT_FOV = 100f;

    public FloatSO playerHealth;
    public GameEvent canSeeGrapplePoint;
    public GameEvent notSeeGrapplePoint;

    public string[] pullObjectTags;
    //public string[] pullObjectTags;

    private PullObjectToPlayer pObjToPlayer;
    private string thingToPull = "";
    public float moveSpeed = 10.0f;
    public float jumpForce = 10.0f;

    private Rigidbody rb;

    public Animator anim;
    private Camera cam;
    private CameraFOV camFOV;
    public Transform shootPoint;
    private LineRenderer line;
    public float hookShotRange = 50f;
    private Vector3 hookShotHitPoint;
    private float hookShotSize;
    public float hookShotThrowSpeed = 70f;
    private float flyingSpeed;
    public float flyingSpeedMultiplier = 2f;
    public float hookShotMinSpeed = 10f;
    public float hookShotMaxSpeed = 40f;
    public float distanceToHookShotHitPoint = 1f;
    private bool HookShotHitSomething;
    public bool isObjectHeld;

    // Look
    public float lookSensitivity = 2.0f;
    public float minXLook;
    public float maxXLook;
    public Transform camAnchor;
    public bool invertXRotation; // For inverting the controls
    private float currentXRot;

    private Vector2 m_Move;
    private Vector2 m_Look;
    public State currentState;
    public float speed = 1.0f;
    public Transform hand;
    public Transform handStartPos;

    public UnityEvent ThrowObjectEvent;
    public UnityEvent PlaceObjectEvent;

    public bool Grounded
    {
        get
        {
            return anim.GetBool("IsGrounded");
        }
        set
        {
            anim.SetBool("IsGrounded", value);
        }
    }

    // Is the player falling
    public bool Falling
    {
        get
        {
            if (!Grounded && !CanJump())
                return true;
            else
                return false;
        }

    }

    public void OnMove(InputAction.CallbackContext context)
    {
        m_Move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        m_Look = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {

        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        Jump();

    }

    public void OnHookShot(InputAction.CallbackContext context)
    {

        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        ThrowHookShot();

    }

    public void OnThrowObject(InputAction.CallbackContext context)
    {

        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        ThrowObject();

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
        //anim = GetComponent<Animator>();
        line = GetComponent<LineRenderer>();
        cam = Camera.main;
        camFOV = cam.GetComponent<CameraFOV>();
        currentState = State.Normal;
        Cursor.lockState = CursorLockMode.Locked;

        if (pObjToPlayer == null)
            pObjToPlayer = GameObject.FindObjectOfType<PullObjectToPlayer>();
        if (pObjToPlayer.hookShotOnTrigger == null)
            pObjToPlayer.hookShotOnTrigger = new UnityEvent();

        pObjToPlayer.hookShotOnTrigger.AddListener(PlayerHoldingObject);
        //shootPoint.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHealth.value = 5;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            default:
            case State.Normal:
                Move(m_Move);
                Look(m_Look);
                break;
            case State.HookShotThrown:
                HandleHookShotThrown();
                rb.velocity = Vector3.zero; // If player is moving while firing, player will continue to move for a short time.
                                            // This stops player from moving while hookshot if firing
                break;
            case State.HookShotFlying:
                HookShotFlyingMovement();
                //Look(m_Look);
                break;
            case State.HookShotPullObjTowards:
                HookShotPullObject();
                break;
            case State.HookShotMissed:
                HookShotMiss();
                break;
        }
    }

    public void Move(Vector2 direction)
    {
        Vector3 dir = transform.right * direction.x + transform.forward * direction.y;
        dir *= moveSpeed;
        dir.y = rb.velocity.y;
        rb.velocity = dir;

        anim.SetFloat("xPos", direction.x); 
        anim.SetFloat("yPos", direction.y);
        anim.SetBool("IsLanding", false);

        CanJump();
        if (Falling)
        {
            Grounded = false;
            anim.SetBool("IsJumping", false);
        }

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
                canSeeGrapplePoint.Raise();
            }
            else
            {
                notSeeGrapplePoint.Raise();

            }
        }
    }

    public void Jump()
    {
        if (CanJump())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            anim.SetBool("IsJumping", true);
            Grounded = false;
        }
    }

    bool CanJump()
    {
        Ray ray = new Ray(transform.position, Vector3.down); // Shoot a ray down
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 1.5f)) // If the ray hits the ground
        {
            Grounded = true; // is the player on the ground?
            anim.SetBool("IsJumping", false);
            return true;
        }

        Grounded = false;
        return false;
    }

    public void ThrowHookShot()
    {
        

        Vector3 lineOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawLine(lineOrigin, cam.transform.forward * hookShotRange, Color.green);

        //int canGrappleToLayerMask = 1 << 6;

        //int canPullTowardsSelf = 1 << 7;
        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        if (isObjectHeld)
        {
            HookShotHitSomething = false;
            hookShotHitPoint = rayOrigin + (cam.transform.forward * hookShotRange);
            
            hookShotSize = 2f;
            shootPoint.gameObject.SetActive(true);
            shootPoint.localScale = Vector3.zero;
            currentState = State.HookShotThrown;
            //PlaceObjectEvent.Invoke();
            // isObjectHeld = false;
            return;
        }

        thingToPull = "";

        if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, hookShotRange))
        {
            hookShotHitPoint = hit.point;
            if (hit.rigidbody)
                thingToPull = hit.rigidbody.gameObject.tag;

            HookShotHitSomething = true;
            hookShotSize = 2f;
            shootPoint.gameObject.SetActive(true);
            shootPoint.localScale = Vector3.zero;
            currentState = State.HookShotThrown;
        }
        else
        {
            HookShotHitSomething = false;
            hookShotHitPoint = rayOrigin + (cam.transform.forward * hookShotRange);
            hookShotSize = 2f;
            shootPoint.gameObject.SetActive(true);
            shootPoint.localScale = Vector3.zero;
            currentState = State.HookShotThrown;

        }


    }

    public void HandleHookShotThrown()
    {
        //anim.SetBool("StartSwinging", true);
        shootPoint.LookAt(hookShotHitPoint);
        hookShotSize += hookShotThrowSpeed * Time.deltaTime;
        shootPoint.localScale = new Vector3(1, 1, hookShotSize);
        //float step = speed * Time.deltaTime;
        hand.position = Vector3.MoveTowards(hand.position, hookShotHitPoint, speed * Time.deltaTime);

        if (hookShotSize >= Vector3.Distance(transform.position, hookShotHitPoint))
        {
            if (HookShotHitSomething)
            {
                if (pullObjectTags.Contains(thingToPull))
                {
                    currentState = State.HookShotPullObjTowards;
                }
                else
                {
                    currentState = State.HookShotFlying;
                    camFOV.SetCameraFOV(HOOKSHOT_FOV);
                }
            }
            else if (!HookShotHitSomething)
            {
                currentState = State.HookShotMissed;
            }
        }
    }

    public void HookShotFlyingMovement()
    {
        //anim.SetBool("StartSwinging", false);

        anim.SetBool("IsFlying", true);
        shootPoint.LookAt(hookShotHitPoint);
        rb.useGravity = false;
        flyingSpeed = Mathf.Clamp(Vector3.Distance(transform.position, hookShotHitPoint), hookShotMinSpeed, hookShotMaxSpeed);
        transform.position = Vector3.MoveTowards(transform.position, hookShotHitPoint, flyingSpeed * flyingSpeedMultiplier * Time.deltaTime);

        hookShotSize = Vector3.Distance(transform.position, hookShotHitPoint);
        shootPoint.localScale = new Vector3(1, 1, hookShotSize);

        //hand.position = Vector3.MoveTowards(hookShotHitPoint, handStartPos.position, speed * Time.deltaTime);
        hand.position = hookShotHitPoint;
        if (Vector3.Distance(transform.position, hookShotHitPoint) < distanceToHookShotHitPoint)
        {
            anim.SetBool("IsFlying", false);
            anim.SetBool("IsLanding", true);
            //anim.SetBool("IsLanding", false);

            hand.position = handStartPos.position;
            currentState = State.Normal;
            shootPoint.gameObject.SetActive(false);
            camFOV.SetCameraFOV(NORMAL_FOV);

            rb.useGravity = true;
        }
    }

    public void HookShotPullObject()
    {
        shootPoint.LookAt(hookShotHitPoint);
        if (hookShotSize >= 2f)
            hookShotSize -= hookShotThrowSpeed * Time.deltaTime;
        shootPoint.localScale = new Vector3(1, 1, hookShotSize);
        hand.position = Vector3.MoveTowards(hand.position, handStartPos.position, speed * Time.deltaTime);

        if (hookShotSize <= 2f)
        {

            shootPoint.gameObject.SetActive(false);
            currentState = State.Normal;
        }
    }

    public void HookShotMiss()
    {
        PlaceObjectEvent.Invoke();
        isObjectHeld = false;
        shootPoint.LookAt(hookShotHitPoint);
        if (hookShotSize >= 2f)
            hookShotSize -= hookShotThrowSpeed * Time.deltaTime;
        shootPoint.localScale = new Vector3(1, 1, hookShotSize);
        hand.position = Vector3.MoveTowards(hand.position, handStartPos.position, speed * Time.deltaTime);

        if (hookShotSize <= 2f)
        {

            shootPoint.gameObject.SetActive(false);
            currentState = State.Normal;
        }

    }

    public void ThrowObject()
    {
        if (isObjectHeld)
        {
            ThrowObjectEvent.Invoke();
            isObjectHeld = false;
            //return;
        }
    }

    // Listening for the event invoked in OnTriggerEnter in PullObjectToplayer
    public void PlayerHoldingObject()
    {
        currentState = State.HookShotPullObjTowards;
        isObjectHeld = true;
    }

}
