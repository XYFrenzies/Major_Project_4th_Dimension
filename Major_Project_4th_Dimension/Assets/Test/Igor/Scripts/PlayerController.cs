using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private const float NORMAL_FOV = 60f;
    private const float HOOKSHOT_FOV = 100f;

    public float moveSpeed = 10.0f;
    public Rigidbody rb;
    public Animator anim;
    public Camera cam;
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
    private bool didHookShotMiss;

    // Look
    public float lookSensitivity = 2.0f;
    public float minXLook;
    public float maxXLook;
    public Transform camAnchor;
    public bool invertXRotation; // For inverting the controls
    private float currentXRot;

    private Vector2 m_Move;
    private Vector2 m_Look;
    private State currentState;

    public void OnMove(InputAction.CallbackContext context)
    {
        m_Move = context.ReadValue<Vector2>();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        m_Look = context.ReadValue<Vector2>();
    }

    public void OnHookShot(InputAction.CallbackContext context)
    {

        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        HookShot();

    }

    private enum State
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
        anim = GetComponent<Animator>();
        line = GetComponent<LineRenderer>();
        cam = Camera.main;
        camFOV = cam.GetComponent<CameraFOV>();
        currentState = State.Normal;
        Cursor.lockState = CursorLockMode.Locked;
        shootPoint.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {

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
                break;
            case State.HookShotFlying:
                HookShotMovement();
                //Look(m_Look);
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

        //anim.SetFloat("Xpos", direction.x); 
        //anim.SetFloat("Ypos", direction.y);
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
    }

    public void HookShot()
    {
        Vector3 lineOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Debug.DrawLine(lineOrigin, cam.transform.forward * hookShotRange, Color.green);

        //int canGrappleToLayerMask = 1 << 6;

        //int canPullTowardsSelf = 1 << 7;

        Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        RaycastHit hit;

        //line.SetPosition(0, shootPoint.position);

        if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, hookShotRange))
        {
            //line.SetPosition(1, hit.point);
            hookShotHitPoint = hit.point;
            didHookShotMiss = false;
            hookShotSize = 0f;
            shootPoint.gameObject.SetActive(true);
            shootPoint.localScale = Vector3.zero;
            currentState = State.HookShotThrown;
        }
        else
        {
            didHookShotMiss = true;
            hookShotHitPoint = rayOrigin + (cam.transform.forward * hookShotRange);
            hookShotSize = 0f;
            shootPoint.gameObject.SetActive(true);
            shootPoint.localScale = Vector3.zero;
            currentState = State.HookShotThrown;

        }


    }

    public void HandleHookShotThrown()
    {
        shootPoint.LookAt(hookShotHitPoint);
        hookShotSize += hookShotThrowSpeed * Time.deltaTime;
        shootPoint.localScale = new Vector3(1, 1, hookShotSize);

        if (hookShotSize >= Vector3.Distance(transform.position, hookShotHitPoint))
        {
            if (!didHookShotMiss)
            {
                currentState = State.HookShotFlying;
                camFOV.SetCameraFOV(HOOKSHOT_FOV);
            }
            else if (didHookShotMiss)
            {
                currentState = State.HookShotMissed;
            }
        }
    }

    public void HookShotMovement()
    {
        shootPoint.LookAt(hookShotHitPoint);
        rb.useGravity = false;
        flyingSpeed = Mathf.Clamp(Vector3.Distance(transform.position, hookShotHitPoint), hookShotMinSpeed, hookShotMaxSpeed);
        transform.position = Vector3.MoveTowards(transform.position, hookShotHitPoint, flyingSpeed * flyingSpeedMultiplier * Time.deltaTime);

        hookShotSize = Vector3.Distance(transform.position, hookShotHitPoint);
        shootPoint.localScale = new Vector3(1, 1, hookShotSize);

        if (Vector3.Distance(transform.position, hookShotHitPoint) < distanceToHookShotHitPoint)
        {
            currentState = State.Normal;
            shootPoint.gameObject.SetActive(false);
            camFOV.SetCameraFOV(NORMAL_FOV);

            rb.useGravity = true;
        }
    }

    public void HookShotMiss()
    {
        shootPoint.LookAt(hookShotHitPoint);
        if (hookShotSize >= 0f)
            hookShotSize -= hookShotThrowSpeed * Time.deltaTime;
        shootPoint.localScale = new Vector3(1, 1, hookShotSize);

        if (hookShotSize <= 0f)
        {
            shootPoint.gameObject.SetActive(false);
            currentState = State.Normal;
        }


    }

}
