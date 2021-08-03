using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.Events;


public class PlayerControllerNew : MonoBehaviour
{
    private const float NORMAL_FOV = 60f;
    private const float HOOKSHOT_FOV = 100f;



    //public UnityEvent canSeeGrapplePoint;
    //public UnityEvent notSeeGrapplePoint;

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
    public float minXLook = -60f;
    public float maxXLook = 60f;
    public Transform camAnchor;
    public bool invertXRotation; // For inverting the controls
    private float currentXRot;

    private Vector2 m_Move;
    private Vector2 m_Look;
    public State currentState;
    public float speed = 1.0f;
    public Transform hand;
    public Transform handStartPos;

    public ChainShoot chainShoot;

    public Vector3 flyToTarget;
    public bool canFly;

    public void OnMove(InputAction.CallbackContext context)
    {
        m_Move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        m_Look = context.ReadValue<Vector2>();
    }



    //public void OnHookShot(InputAction.CallbackContext context)
    //{

    //    if (context.phase != InputActionPhase.Performed)
    //    {
    //        return;
    //    }
    //    ThrowHookShot();

    //}



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
        chainShoot = GetComponent<ChainShoot>();

        //shootPoint.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        //hand.gameObject.SetActive(false);
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
                rb.velocity = Vector3.zero; // If player is moving while firing, player will continue to move for a short time.
                                            // This stops player from moving while hookshot if firing
                chainShoot.HandleHookShotThrow();
                
                
                break;
            case State.HookShotFlying:
                Fly(flyToTarget);
                //        //HookShotFlyingMovement();
                //        //Look(m_Look);
                break;
                //if (canFly)
                //{
                //    Fly(flyToTarget);
                //}
                //else
                //{
                //    Move(m_Move);
                //    Look(m_Look);
                //}
                //        break;
                //    case State.HookShotThrown:
                //        //HandleHookShotThrown();
                //        rb.velocity = Vector3.zero; // If player is moving while firing, player will continue to move for a short time.
                //                                    // This stops player from moving while hookshot if firing
                //        break;
                //    case State.HookShotFlying:
                //        //HookShotFlyingMovement();
                //        //Look(m_Look);
                //        break;
                //    case State.HookShotPullObjTowards:
                //        //HookShotPullObject();
                //        break;
                //    case State.HookShotMissed:
                //        //HookShotMiss();
                //        break;
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
        //Debug.DrawRay(hand.transform.position, cam.ScreenToWorldPoint(new Vector3(0.5f, 0.5f, 0)) * hookShotRange, Color.red);
        //Debug.DrawLine(lineOrigin, cam.transform.forward * hookShotRange, Color.green);

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
            chainShoot.showLine = false;
            currentState = State.Normal;
            //Debug.Log(rb.useGravity);
        }
    }



    //public void HandleHookShotThrown()
    //{
    //    //anim.SetBool("StartSwinging", true);
    //    shootPoint.LookAt(hookShotHitPoint);
    //    hookShotSize += hookShotThrowSpeed * Time.deltaTime;
    //    shootPoint.localScale = new Vector3(1, 1, hookShotSize);
    //    //float step = speed * Time.deltaTime;
    //    hand.position = Vector3.MoveTowards(hand.position, hookShotHitPoint, speed * Time.deltaTime);

    //    if (hookShotSize >= Vector3.Distance(transform.position, hookShotHitPoint))
    //    {
    //        if (HookShotHitSomething)
    //        {
    //            if (pullObjectTags.Contains(thingToPull))
    //            {
    //                currentState = State.HookShotPullObjTowards;
    //            }
    //            else if(thingToPull.Contains("CanHookShotTowards"))
    //            {
    //                currentState = State.HookShotFlying;
    //                camFOV.SetCameraFOV(HOOKSHOT_FOV);
    //            }
    //            else
    //            {
    //                currentState = State.HookShotMissed;

    //            }
    //        }
    //        else if (!HookShotHitSomething)
    //        {
    //            currentState = State.HookShotMissed;
    //        }
    //    }
    //}

    //public void HookShotFlyingMovement()
    //{
    //    //anim.SetBool("StartSwinging", false);

    //    anim.SetBool("IsFlying", true);
    //    shootPoint.LookAt(hookShotHitPoint);
    //    rb.useGravity = false;
    //    flyingSpeed = Mathf.Clamp(Vector3.Distance(transform.position, hookShotHitPoint), hookShotMinSpeed, hookShotMaxSpeed);
    //    transform.position = Vector3.MoveTowards(transform.position, hookShotHitPoint, flyingSpeed * flyingSpeedMultiplier * Time.deltaTime);

    //    hookShotSize = Vector3.Distance(transform.position, hookShotHitPoint);
    //    shootPoint.localScale = new Vector3(1, 1, hookShotSize);

    //    //hand.position = Vector3.MoveTowards(hookShotHitPoint, handStartPos.position, speed * Time.deltaTime);
    //    hand.position = hookShotHitPoint;
    //    if (Vector3.Distance(transform.position, hookShotHitPoint) < distanceToHookShotHitPoint)
    //    {
    //        anim.SetBool("IsFlying", false);
    //        //anim.SetBool("IsLanding", true);
    //        //anim.SetBool("IsLanding", false);

    //        hand.position = handStartPos.position;
    //        currentState = State.Normal;
    //        shootPoint.gameObject.SetActive(false);
    //        hand.gameObject.SetActive(false);
    //        camFOV.SetCameraFOV(NORMAL_FOV);

    //        rb.useGravity = true;
    //    }
    //}

    //public void HookShotPullObject()
    //{
    //    shootPoint.LookAt(hookShotHitPoint);
    //    if (hookShotSize >= 2f)
    //        hookShotSize -= hookShotThrowSpeed * Time.deltaTime;
    //    shootPoint.localScale = new Vector3(1, 1, hookShotSize);
    //    hand.position = Vector3.MoveTowards(hand.position, handStartPos.position, speed * Time.deltaTime);

    //    if (hookShotSize <= 2f)
    //    {

    //        shootPoint.gameObject.SetActive(false);
    //        currentState = State.Normal;
    //    }
    //}

    //public void HookShotMiss()
    //{
    //    PlaceObjectEvent.Invoke();
    //    isObjectHeld = false;
    //    shootPoint.LookAt(hookShotHitPoint);
    //    if (hookShotSize >= 2f)
    //        hookShotSize -= hookShotThrowSpeed * Time.deltaTime;
    //    shootPoint.localScale = new Vector3(1, 1, hookShotSize);
    //    hand.position = Vector3.MoveTowards(hand.position, handStartPos.position, speed * Time.deltaTime);

    //    if (hookShotSize <= 2f)
    //    {

    //        shootPoint.gameObject.SetActive(false);
    //        hand.gameObject.SetActive(false);
    //        currentState = State.Normal;
    //    }

    //}

    //public void ThrowObject()
    //{
    //    if (isObjectHeld)
    //    {
    //        ThrowObjectEvent.Invoke();
    //        isObjectHeld = false;
    //        hand.gameObject.SetActive(false);
    //        //return;
    //    }
    //}

    //// Listening for the event invoked in OnTriggerEnter in PullObjectToplayer
    //public void PlayerHoldingObject()
    //{
    //    currentState = State.HookShotPullObjTowards;
    //    isObjectHeld = true;
    //}



}
