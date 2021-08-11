using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChainShootStartAgain : MonoBehaviour
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

    public Transform shootPoint;
    public Transform holdPoint;
    public Transform hand;
    public Transform handStartPos;

    public GameObject objectToPickUpOrDrop;

    private Camera cam;
    private PlayerControllerNew player;
    public float hookShotRange = 50f;

    public float chainSpeed = 20f;

    private Vector3 hitPos;
    public Vector3 lookAtPos;
    public Transform posToLookAt;
    private Vector3 currentGrapplePos;
    private float hookShotFlySpeed = 10;


    private GameObject objectToPull;
    [HideInInspector]
    public HookShotState currentHookShotState;


    private bool isObjectHeld = false;
    public bool showLine = false;
    private bool pullCheck = false;
    public bool canPickUp;
    public bool canGrapple;
    private bool stop = false;
    public bool pull = false;
    public bool pickup = false;
    public bool fly = false;
    public bool place = false;
    public bool putDown = false;

    private float stopPullingDistance = 5f;

    public void OnHookShot(InputAction.CallbackContext context)
    {

        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        ThrowHookShot();

    }


    public void OnThrow(InputAction.CallbackContext context)
    {

        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        if (!objectToPull && isObjectHeld)
            ThrowObject();
        //Debug.Log("Throw");

    }

    public void OnTest2(InputAction.CallbackContext context)
    {

        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        stop = true;

    }

    public enum HookShotState
    {
        Normal,
        Throw,
        Pull,
        Pickup,
        Place,
        ThrowObject,
        Fly,
        ReturnHand,
    }

    private void Awake()
    {
        player = GetComponent<PlayerControllerNew>();
        cam = Camera.main;
        currentHookShotState = HookShotState.Normal;
        lineRenderer = GetComponent<LineRenderer>();
        initWaveScale = waveScale;
        handStartPos.position = hand.position;
    }

    private void Update()
    {
        //shootPoint.LookAt(posToLookAt);
        //if (pullCheck)
        //{
        //    PullObject(objectToPull);
        //}


        switch (currentHookShotState)
        {
            default:
            case HookShotState.Normal:
                //ThrowHookShot();
                break;
            case HookShotState.Throw:
                HandleHookShotThrow();
                break;
            case HookShotState.Fly:
                //HandleHookshotMovement();
                break;
            case HookShotState.Pickup:
                PickUp(objectToPickUpOrDrop);
                break;
            case HookShotState.Pull:
                PullObject(objectToPull);
                break;
            case HookShotState.Place:
                PlaceObject(hookshotPosition);
                break;
            case HookShotState.ReturnHand:
                ReturnHand();
                break;
                //case HookShotState.ThrowObject:
                //    ThrowObject();
                //    break;
        }
        Debug.Log(currentHookShotState);

    }


    public void ThrowHookShot()
    {
        isRetrieve = false;
        RaycastHit hit;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));


        // Check if object is being held here
        if (isObjectHeld)
        {

            if (Physics.Raycast(ray, out hit, hookShotRange))
            {

                hookshotPosition = hit.point;

            }
            else // put back at point of chain's full length
            {

                hookshotPosition = ray.origin + (cam.transform.forward * hookShotRange);

            }

            pickup = false;
            //place = true;
            currentHookShotState = HookShotState.Place;
            player.currentState = PlayerControllerNew.State.HookShotThrown;
            return;
        }
        //Debug.DrawRay(shootPoint.position, ray.direction * 50f, Color.red, 2f);
        //if (Physics.SphereCast(ray.origin, 1f, cam.transform.forward, out hit, hookShotRange))
        if (Physics.Raycast(ray, out hit, hookShotRange))
        {
            //hitPos = hit.point;
            hookshotPosition = hit.point;
            //initialLength = Vector3.Distance(transform.position, hookshotPosition);

            //currentGrapplePosition = shootPoint.position;


            if (hit.transform.CompareTag("CanHookShotTowards")) // hit grapple point
            {

                player.flyToTarget = hit.point;
                fly = true;
                Debug.Log("Can hook shot towards");

            }
            else if (hit.transform.CompareTag("MoveableToMe")) // pick up object
            {
                Debug.Log("can pick up");
                objectToPickUpOrDrop = hit.transform.gameObject;
                //isObjectHeld = true;
                pickup = true;

            }
            else if (hit.transform.CompareTag("BigPullObject")) // pull object towards me
            {
                Debug.Log("can pull to me");
                objectToPull = hit.transform.gameObject;

                pullCheck = !pullCheck;
                pull = true;

                Debug.Log("Pullcheck: " + pullCheck);

            }
            else // hit object but cant pick up, pull or grapple
            {

                Debug.Log("Hit other thing");
                // hookshotPosition = hit.point;
                //initialLength = Vector3.Distance(transform.position, hookshotPosition);

                //currentGrapplePosition = shootPoint.position;


            }


        }
        else // missed everything
        {
            Debug.Log("missed everything");
            hookshotPosition = ray.origin + (cam.transform.forward * hookShotRange);
           // initialLength = Vector3.Distance(transform.position, hookshotPosition);

           // currentGrapplePosition = shootPoint.position;


        }

        {

            //Vector3 lineOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            //Debug.DrawLine(lineOrigin, cam.transform.forward * hookShotRange, Color.green);

        }

        player.currentState = PlayerControllerNew.State.HookShotThrown;
        currentHookShotState = HookShotState.Throw;
    }
    public void HandleHookShotThrow()
    {

        ShootHand();
    }

    //public void HandleHookShotThrow()
    //{
    //    CalculateLineRenderer();
    //    if (waveScale <= 0.05f)
    //    {
    //        lineRenderer.positionCount = 2;
    //        if (fly)
    //        {
    //            player.currentState = PlayerControllerNew.State.HookShotFlying;
    //            currentHookShotState = HookShotState.Fly;
    //        }
    //        if (pickup)
    //        {
    //            currentHookShotState = HookShotState.Pickup;
    //        }
    //        if (pull)
    //        {
    //            currentHookShotState = HookShotState.Pull;

    //        }
    //        if (putDown)
    //        {
    //            currentHookShotState = HookShotState.Normal;
    //            putDown = false;
    //        }

    //    }
    //    if (stop)
    //    {
    //        StopHookShot();
    //    }

    //}


    //private void CalculateLineRenderer()
    //{
    //    Vector3 calculatePoint = hookshotPosition;
    //    if (isRetrieve)
    //        calculatePoint = shootPoint.position;

    //    currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, calculatePoint, Time.deltaTime *
    //        (isRetrieve ? hookshotReturnFlySpeed : hookshotReturnFlySpeed));

    //    if (isRetrieve)
    //        waveScale = (1 - (Vector3.Distance(currentGrapplePosition, calculatePoint) / initialLength)) * initWaveScale;
    //    else
    //        waveScale = (Vector3.Distance(currentGrapplePosition, calculatePoint) / initialLength) * initWaveScale;

    //    for (int i = 0; i < maxHookShotDistance; i++)
    //    {
    //        Vector3 dir = i * (currentGrapplePosition - shootPoint.position) / maxHookShotDistance;
    //        float X = dir.magnitude;
    //        float Y = Mathf.Sin(X * waveScale);
    //        float percent = (float)i / maxHookShotDistance;

    //        Vector3 way = dir + shootPoint.position +
    //            (shootPoint.rotation * Quaternion.Euler(hookOffset) * new Vector3(Y, 0, 0) * magnitudeOverDistance.Evaluate(percent));
    //        lineRenderer.SetPosition(i, way);
    //    }



    //    { // old
    //      //Vector3 calculatePoint = hitPos;

    //        //currentGrapplePos = Vector3.Lerp(currentGrapplePos, calculatePoint, Time.deltaTime * hookShotFlySpeed);
    //        //for (int i = 0; i < maxHookShotDistance; i++)
    //        //{
    //        //    Vector3 dir = i * (currentGrapplePos - shootPoint.position) / maxHookShotDistance;
    //        //    float x = dir.magnitude;
    //        //    float y = Mathf.Sin(x * waveScale);

    //        //    float percent = (float)i / maxHookShotDistance;

    //        //    Vector3 way = dir + shootPoint.position + (shootPoint.rotation * new Vector3(y, 0, 0) * magnitudeOverDistance.Evaluate(percent));
    //        //    lineRenderer.SetPosition(i, way);
    //        //}
    //    }
    //}


    //private void HandleHookshotMovement()
    //{
    //    lineRenderer.SetPosition(0, shootPoint.position);
    //    lineRenderer.SetPosition(1, hookshotPosition);

    //    if (stop)
    //    {
    //        StopHookShot();
    //    }
    //}
    //private void StopHookShot()
    //{
    //    player.currentState = PlayerControllerNew.State.Normal;
    //    currentHookShotState = HookShotState.Normal;
    //    StartCoroutine(RetrieveHook());
    //}

    //IEnumerator RetrieveHook()
    //{
    //    isRetrieve = true;
    //    lineRenderer.positionCount = maxHookShotDistance;
    //    float maxWaveScale = initWaveScale - 0.05f;
    //    while (waveScale <= maxWaveScale)
    //    {
    //        CalculateLineRenderer();
    //        yield return null;
    //    }
    //    isRetrieve = false;
    //    lineRenderer.positionCount = 0;
    //    stop = false;
    //}


    public void PullObject(GameObject pullObject)
    {

        if (Vector3.Distance(pullObject.transform.position, player.transform.position) >= stopPullingDistance)
        {

            //Rigidbody rb = pullObject.GetComponent<Rigidbody>();
            // if (player.GetComponent<Rigidbody>().velocity.sqrMagnitude > 0f)
            // {
            //lineRenderer.SetPosition(1, pullObject.transform.position);
            pullObject.transform.position = Vector3.MoveTowards(pullObject.transform.position, player.transform.position, 5f * Time.deltaTime);
            pullObject.transform.rotation = Quaternion.LookRotation(pullObject.transform.position - player.transform.position);
            //   Debug.Log("pulling " + pullObject.name + "Player velocity " + player.GetComponent<Rigidbody>().velocity);
            //hand.Translate(hookshotPosition, Space.Self);
            //  }
        }

    }

    public void PickUp(GameObject pickupObject)
    {

        Rigidbody rb = pickupObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        pickupObject.transform.position = Vector3.MoveTowards(pickupObject.transform.position, holdPoint.position, 50f * Time.deltaTime);
        ReturnHand();
        if (Vector3.Distance(pickupObject.transform.position, holdPoint.position) <= 1f)
        {
            pickupObject.layer = LayerMask.NameToLayer("Hold");
            pickupObject.GetComponent<Rigidbody>().isKinematic = true;
            pickupObject.transform.SetParent(holdPoint);
            isObjectHeld = true;

            //currentHookShotState = HookShotState.Normal;
            player.currentState = PlayerControllerNew.State.Normal;

        }
    }

    public void PlaceObject(Vector3 target)
    {
        if (objectToPickUpOrDrop != null)
        {
            objectToPickUpOrDrop.layer = LayerMask.NameToLayer("Default");
            objectToPickUpOrDrop.GetComponent<Rigidbody>().isKinematic = false;
            objectToPickUpOrDrop.transform.SetParent(null);
            ShootHand();
            objectToPickUpOrDrop.transform.position = Vector3.MoveTowards(objectToPickUpOrDrop.transform.position, target, 50f * Time.deltaTime);

            if (Vector3.Distance(objectToPickUpOrDrop.transform.position, target) <= 1f)
            {
                Rigidbody rb = objectToPickUpOrDrop.GetComponent<Rigidbody>();
                rb.useGravity = true;
                isObjectHeld = false;
                objectToPickUpOrDrop = null;
                place = false;
                currentHookShotState = HookShotState.ReturnHand;
            }
        }
    }

    public void ThrowObject()
    {
        currentHookShotState = HookShotState.Normal;
        objectToPickUpOrDrop.layer = LayerMask.NameToLayer("Default");
        objectToPickUpOrDrop.GetComponent<Rigidbody>().isKinematic = false;
        objectToPickUpOrDrop.transform.SetParent(null);
        isObjectHeld = false;
        Rigidbody rb = objectToPickUpOrDrop.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.AddForce(shootPoint.forward * 30f, ForceMode.Impulse);
        Debug.Log(rb.gameObject.name);
        objectToPickUpOrDrop = null;
        pickup = false;
    }

    public void SendHandForward()
    {
        if (!isRetrieve)
            hand.position = Vector3.MoveTowards(hand.position, hookshotPosition, 50f * Time.deltaTime);
        else
            hand.position = Vector3.MoveTowards(hand.position, handStartPos.position, 50f * Time.deltaTime);

        if (Vector3.Distance(hand.position, hookshotPosition) <= 2f)
        {
            Debug.Log("reached target");
            isRetrieve = true;
        }

        if (isRetrieve)
            if (Vector3.Distance(hand.position, handStartPos.position) <= 2f)
            {
                Debug.Log("returned home");

                currentHookShotState = HookShotState.Normal;
            }
    }

    public void ShootHand()
    {
        hand.gameObject.SetActive(true);
        hand.position = Vector3.MoveTowards(hand.position, hookshotPosition, 50f * Time.deltaTime);
        if (Vector3.Distance(hand.position, hookshotPosition) <= 1f)
        {
            Debug.Log("reached target");

            if (fly)
                player.currentState = PlayerControllerNew.State.HookShotFlying;
            if (pickup)
                currentHookShotState = HookShotState.Pickup;
            if (pull)
            {
                currentHookShotState = HookShotState.Pull;
                player.currentState = PlayerControllerNew.State.Normal;
            }
        }
    }

    public void ReturnHand()
    {
        hand.position = Vector3.MoveTowards(hand.position, handStartPos.position, 50f * Time.deltaTime);
        if (Vector3.Distance(hand.position, handStartPos.position) <= 1f)
        {
            Debug.Log("returned home");
            currentHookShotState = HookShotState.Normal;
            player.currentState = PlayerControllerNew.State.Normal;
            hand.gameObject.SetActive(false);

        }
    }


    public void StationaryHand()
    {

    }

}
