using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinWave : MonoBehaviour
{

    public float waveScale = 1f;
    public float hookshotFlySpeed;
    public int maxHookShotDistance = 100;
    LineRenderer lineRenderer;
    Vector3 currentGrapplePosition;
    public LayerMask whatIsGrappleable;
    private Vector3 hookshotPosition;
    private State state;

    private enum State
    {
        Normal,
        HookshotThrown,
    }


    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = maxHookShotDistance;
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
        }
    }


    private void HandleHookshotStart()
    {
        if(Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, maxHookShotDistance, whatIsGrappleable))
        {
            hookshotPosition = raycastHit.point;
            currentGrapplePosition = transform.position;
            state = State.HookshotThrown;
        }
    }


    private void HandleHookshotThrow()
    {
        CalculateLineRenderer();
    }


    private void CalculateLineRenderer()
    {
        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, hookshotPosition, Time.deltaTime * hookshotFlySpeed);
        for (int i = 0; i < maxHookShotDistance; i++)
        {
            Vector3 dir = i * (currentGrapplePosition - transform.position) / maxHookShotDistance;
            float x = dir.magnitude;
            float y = Mathf.Sin(x * waveScale);
            Vector3 way = dir + transform.position + (transform.rotation * new Vector3(y, 0, 0));
            lineRenderer.SetPosition(i, way);
        }
    }

}
