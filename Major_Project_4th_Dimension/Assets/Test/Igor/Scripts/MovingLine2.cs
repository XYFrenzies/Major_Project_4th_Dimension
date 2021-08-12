using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovingLine2 : MonoBehaviour
{
    public float waveScale = 1f;
    public float hookshotFlySpeed, hookshotReturnFlySpeed;
    float initialLength;
    float initWaveScale = 1f;
    public int maxHookShotDistance = 100;
    public AnimationCurve magnitudeOverDistance;
    LineRenderer lineRenderer;
    Vector3 currentGrapplePosition;
    Vector3 hookOffset;
    bool isRetrieve;
    public LayerMask WhatisGrappleable;
    private Vector3 hookshotPosition;
    private State state;
    private bool stop = false;

    private enum State
    {
        Normal,
        HookshotThrown,
        HookshotFlyingPlayer,
    }

    public void OnTest(InputAction.CallbackContext context)
    {

        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        HandleHookshotStart();

    }

    public void OnTest2(InputAction.CallbackContext context)
    {

        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        stop = true;
        
    }

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        initWaveScale = waveScale;
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            default:
            case State.Normal:
                HandleHookshotStart();
                break;
            case State.HookshotThrown:
                HandleHookshotThrow();
                break;
            case State.HookshotFlyingPlayer:
                HandleHookshotMovement();
                break;

        }


    }


    private void HandleHookshotStart()
    {
        if (/*Input.GetKeyDown(KeyCode.K) && */!isRetrieve)
        {

            if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, maxHookShotDistance, WhatisGrappleable))
            {

                hookshotPosition = raycastHit.point;
                initialLength = Vector3.Distance(transform.position, hookshotPosition);
                lineRenderer.positionCount = maxHookShotDistance;
                currentGrapplePosition = transform.position;
                waveScale = initWaveScale;
                state = State.HookshotThrown;
            }
        }
    }

    private void HandleHookshotThrow()
    {
        CalculateLineRenderer();
        if (waveScale <= 0.05f)
        {
            state = State.HookshotFlyingPlayer;
            lineRenderer.positionCount = 2;
        }
        if (stop/*Input.GetButtonDown("HookShot")*/)
        {
            StopHookshot();
        }
    }

    private void CalculateLineRenderer()
    {
        Vector3 calculatePoint = hookshotPosition;
        if (isRetrieve)
            calculatePoint = transform.position;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, calculatePoint, Time.deltaTime *
            (isRetrieve ? hookshotReturnFlySpeed : hookshotReturnFlySpeed));

        if (isRetrieve)
            waveScale = (1 - (Vector3.Distance(currentGrapplePosition, calculatePoint) / initialLength)) * initWaveScale;
        else
            waveScale = (Vector3.Distance(currentGrapplePosition, calculatePoint) / initialLength) * initWaveScale;

        for (int i = 0; i < maxHookShotDistance; i++)
        {
            Vector3 dir = i * (currentGrapplePosition - transform.position) / maxHookShotDistance;
            float X = dir.magnitude;
            float Y = Mathf.Sin(X * waveScale);
            float percent = (float)i / maxHookShotDistance;

            Vector3 way = dir + transform.position +
                (transform.rotation * Quaternion.Euler(hookOffset) * new Vector3(Y, 0, 0) * magnitudeOverDistance.Evaluate(percent));
            lineRenderer.SetPosition(i, way);
        }
    }

    private void HandleHookshotMovement()
    {
        lineRenderer.SetPosition(0, transform.position);
        lineRenderer.SetPosition(1, hookshotPosition);

        if (stop/*Input.GetButtonDown("HookShot")*/)
        {
            StopHookshot();
        }
    }

    private void StopHookshot()
    {
        state = State.Normal;
        StartCoroutine(RetrieveHook());

    }

    IEnumerator RetrieveHook()
    {
        isRetrieve = true;
        lineRenderer.positionCount = maxHookShotDistance;
        float maxWaveScale = initWaveScale - 0.01f;
        while (waveScale <= maxWaveScale)
        {
            CalculateLineRenderer();
            yield return null;
        }
        isRetrieve = false;
        lineRenderer.positionCount = 0;
        stop = false;
    }
}
