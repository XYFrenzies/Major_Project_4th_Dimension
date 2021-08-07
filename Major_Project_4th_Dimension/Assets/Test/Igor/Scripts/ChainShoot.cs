using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChainShoot : MonoBehaviour
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


    public GameObject objectToPickUpOrDrop;

    private Camera cam;
    private PlayerControllerNew player;
    public float hookShotRange = 50f;

    public float chainSpeed = 20f;

    public bool canGrapple;
    public bool canPickUp;
    private bool isObjectHeld = false;
    private Vector3 hitPos;
    public Vector3 lookAtPos;
    public Transform posToLookAt;
    private Vector3 currentGrapplePos;
    private float hookShotFlySpeed = 10;


    private GameObject objectToPull;
    private bool pullCheck = false;
    [HideInInspector]
    public bool showLine = false;
    public HookShotState currentHookShotState;

    private bool stop = false;

    public bool pull = false;
    public bool pickup = false;
    public bool fly = false;
    public bool place = false;
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
        if (!objectToPull)
            Debug.Log("Throw");

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
        Fly
    }

    private void Awake()
    {
        player = GetComponent<PlayerControllerNew>();
        cam = Camera.main;
        currentHookShotState = HookShotState.Normal;
        lineRenderer = GetComponent<LineRenderer>();
        initWaveScale = waveScale;

    }

    private void Update()
    {
        //shootPoint.LookAt(posToLookAt);
        //if (pullCheck)
        //{
        //    PullObject(objectToPull);
        //}
        //if (pickup)
        //    PickUp(objectToPickUpOrDrop);
        //if (place)
        //    PlaceObject();
        //if (showLine)
        //{

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
                HandleHookshotMovement();
                break;
            case HookShotState.Pickup:
                PickUp(objectToPickUpOrDrop);
                break;
            case HookShotState.Pull:
                PullObject(objectToPull);
                break;
        }

    }


    public void ThrowHookShot()
    {
        fly = false;
        pull = false;
        place = false;
        RaycastHit hit;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        //Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        // Check if object is being held here
        if (isObjectHeld)
        {
            //if (holdObj.heldObj != null)
            //    objectToPickUpOrDrop = holdObj.heldObj;
            if (Physics.Raycast(ray, out hit, hookShotRange))
            {
                place = true;
                pickup = false;
                hookshotPosition = hit.point;
                // SpawnChain(shootPoint.position, shootPoint.forward, chainSpeed, hit.point, objectToPickUpOrDrop, false, false); // put back object at hit point
            }
            else // put back at point of chain's full length
            {
                pickup = false;
                place = true;
                hookshotPosition = ray.origin + (cam.transform.forward * hookShotRange);
                //SpawnChain(shootPoint.position, shootPoint.forward, chainSpeed, (ray.origin/*rayOrigin*/ + (cam.transform.forward * hookShotRange)/*ray.GetPoint(hookShotRange*/), objectToPickUpOrDrop, false, false);
            }
            return;
        }
        //Debug.DrawRay(shootPoint.position, ray.direction * 50f, Color.red, 2f);
        //if (Physics.SphereCast(rayOrigin,2f , cam.transform.forward ,out hit, hookShotRange))
        if (Physics.Raycast(ray, out hit, hookShotRange))
        {
            //hitPos = hit.point;
            hookshotPosition = hit.point;
            initialLength = Vector3.Distance(transform.position, hookshotPosition);
            lineRenderer.positionCount = maxHookShotDistance;
            currentGrapplePosition = shootPoint.position;
            waveScale = initWaveScale;

            if (hit.transform.CompareTag("CanHookShotTowards")) // hit grapple point
            {
                // currentGrapplePos = shootPoint.position;
                player.flyToTarget = hit.point;
                // showLine = true;
                //player.currentState = PlayerControllerNew.State.HookShotFlying;
                fly = true;
                
                Debug.Log("Can hook shot towards");
                //return;
                // SpawnChain(shootPoint.position, shootPoint.forward, chainSpeed, hit.point, true, false);
            }
            else if (hit.transform.CompareTag("MoveableToMe")) // pick up object
            {
                Debug.Log("can pick up");
                objectToPickUpOrDrop = hit.transform.gameObject;
                isObjectHeld = true;
                pickup = true;
                
                //showLine = true;
                //SpawnChain(shootPoint.position, shootPoint.forward, chainSpeed, hit.point, hit.transform.gameObject, false, true);

            }
            else if (hit.transform.CompareTag("BigPullObject")) // pull object towards me
            {
                Debug.Log("can pull to me");
                objectToPull = hit.transform.gameObject;
                pullCheck = !pullCheck;
                pull = true;

                Debug.Log("Pullcheck: " + pullCheck);

                //pullCheck = true;
                // showLine = true;


                //SpawnChain(shootPoint.position, shootPoint.forward, chainSpeed, hit.point, hit.transform.gameObject, false, true);

            }
            else
            {

                Debug.Log("Hit other thing");
                // SpawnChain(shootPoint.position, shootPoint.forward, chainSpeed, hit.point, false, false); // hit non grapple point but still object
            }


        }
        else // missed everything
        {
            Debug.Log("missed everything");

            //SpawnChain(shootPoint.position, shootPoint.forward, chainSpeed, (ray.origin/*rayOrigin*/ + (cam.transform.forward * hookShotRange)/*ray.GetPoint(hookShotRange*/), false, false);
        }


        {
            //hand.gameObject.SetActive(true);

            //Vector3 lineOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            //Debug.DrawLine(lineOrigin, cam.transform.forward * hookShotRange, Color.green);


            ////int canGrappleToLayerMask = 1 << 6;

            ////int canPullTowardsSelf = 1 << 7;
            //Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));


            //thingToPull = "";

            //if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, hookShotRange))
            //{
            //    hookShotHitPoint = hit.point;
            //    if (hit.rigidbody)
            //        thingToPull = hit.rigidbody.gameObject.tag;

            //    HookShotHitSomething = true;
            //    hookShotSize = 2f;
            //    shootPoint.gameObject.SetActive(true);
            //    shootPoint.localScale = Vector3.zero;
            //    currentState = State.HookShotThrown;
            //}
            //else
            //{
            //    HookShotHitSomething = false;
            //    hookShotHitPoint = rayOrigin + (cam.transform.forward * hookShotRange);
            //    hookShotSize = 2f;
            //    shootPoint.gameObject.SetActive(true);
            //    shootPoint.localScale = Vector3.zero;
            //    currentState = State.HookShotThrown;

            //}
        }

        //player.currentState = PlayerControllerNew.State.HookShotThrown;

        currentHookShotState = HookShotState.Throw;
        //currentHookShotState = HookShotState.Throw;
    }

    public void HandleHookShotThrow()
    {
        CalculateLineRenderer();
        if (waveScale <= 0.01f)
        {
            if (fly)
            {
                player.currentState = PlayerControllerNew.State.HookShotFlying;
                currentHookShotState = HookShotState.Fly;
            }
            if(pickup)
            {
                currentHookShotState = HookShotState.Pickup;
            }
            if(pull)
            {
                currentHookShotState = HookShotState.Pull;

            }
            lineRenderer.positionCount = 2;
        }
        if (stop/*Input.GetButtonDown("HookShot")*/)
        {
            StopHookShot();
        }
        //StopHookShot();
        //player.currentState = PlayerControllerNew.State.Normal;
    }


    private void CalculateLineRenderer()
    {
        Vector3 calculatePoint = hookshotPosition;
        if (isRetrieve)
            calculatePoint = shootPoint.position;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, calculatePoint, Time.deltaTime *
            (isRetrieve ? hookshotReturnFlySpeed : hookshotReturnFlySpeed));

        if (isRetrieve)
            waveScale = (1 - (Vector3.Distance(currentGrapplePosition, calculatePoint) / initialLength)) * initWaveScale;
        else
            waveScale = (Vector3.Distance(currentGrapplePosition, calculatePoint) / initialLength) * initWaveScale;

        for (int i = 0; i < maxHookShotDistance; i++)
        {
            Vector3 dir = i * (currentGrapplePosition - shootPoint.position) / maxHookShotDistance;
            float X = dir.magnitude;
            float Y = Mathf.Sin(X * waveScale);
            float percent = (float)i / maxHookShotDistance;

            Vector3 way = dir + shootPoint.position +
                (shootPoint.rotation * Quaternion.Euler(hookOffset) * new Vector3(Y, 0, 0) * magnitudeOverDistance.Evaluate(percent));
            lineRenderer.SetPosition(i, way);
        }



        { // old
          //Vector3 calculatePoint = hitPos;

            //currentGrapplePos = Vector3.Lerp(currentGrapplePos, calculatePoint, Time.deltaTime * hookShotFlySpeed);
            //for (int i = 0; i < maxHookShotDistance; i++)
            //{
            //    Vector3 dir = i * (currentGrapplePos - shootPoint.position) / maxHookShotDistance;
            //    float x = dir.magnitude;
            //    float y = Mathf.Sin(x * waveScale);

            //    float percent = (float)i / maxHookShotDistance;

            //    Vector3 way = dir + shootPoint.position + (shootPoint.rotation * new Vector3(y, 0, 0) * magnitudeOverDistance.Evaluate(percent));
            //    lineRenderer.SetPosition(i, way);
            //}
        }
    }


    private void HandleHookshotMovement()
    {
        lineRenderer.SetPosition(0, shootPoint.position);
        lineRenderer.SetPosition(1, hookshotPosition);

        if (stop/*Input.GetButtonDown("HookShot")*/)
        {
            StopHookShot();
        }
    }
    private void StopHookShot()
    {
        player.currentState = PlayerControllerNew.State.Normal;
        currentHookShotState = HookShotState.Normal;
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


    public void PullObject(GameObject pullObject)
    {

        if (Vector3.Distance(pullObject.transform.position, player.transform.position) >= stopPullingDistance)
        {

            Rigidbody rb = pullObject.GetComponent<Rigidbody>();
            if (player.GetComponent<Rigidbody>().velocity.sqrMagnitude > 0f)
            {
                lineRenderer.SetPosition(1, pullObject.transform.position);
                //rb.AddForce((player.transform.position - pullObject.transform.position), ForceMode.Force);
                pullObject.transform.position = Vector3.MoveTowards(pullObject.transform.position, player.transform.position, 5f * Time.deltaTime);
                Debug.Log("pulling " + pullObject.name + "Player velocity " + player.GetComponent<Rigidbody>().velocity);

            }
        }

    }

    public void PickUp(GameObject pickupObject)
    {

        Rigidbody rb = pickupObject.GetComponent<Rigidbody>();
        rb.useGravity = false;
        pickupObject.transform.position = Vector3.MoveTowards(pickupObject.transform.position, holdPoint.position, 50f * Time.deltaTime);
        if (Vector3.Distance(pickupObject.transform.position, holdPoint.position) <= 1f)
        {
            pickupObject.layer = LayerMask.NameToLayer("Hold");
            pickupObject.GetComponent<Rigidbody>().isKinematic = true;
            pickupObject.transform.SetParent(holdPoint);
            isObjectHeld = true;

        }
    }

    public void PlaceObject()
    {

        objectToPickUpOrDrop.layer = LayerMask.NameToLayer("Default");
        objectToPickUpOrDrop.GetComponent<Rigidbody>().isKinematic = false;
        objectToPickUpOrDrop.transform.SetParent(null);
        isObjectHeld = false;
        Rigidbody rb = objectToPickUpOrDrop.GetComponent<Rigidbody>();
        rb.useGravity = true;

    }

    //public void ReelIn(GameObject pullObject)
    //{
    //    Rigidbody rb = pullObject.GetComponent<Rigidbody>();
    //    rb.AddForce((player.transform.position - pullObject.transform.position), ForceMode.VelocityChange);
    //    Debug.Log("Reeling");
    //}

    //public void SpawnChain(Vector3 spawnPos, Vector3 direction, float speed, Vector3 hitPoint, bool grapple, bool pickup)
    //{
    //    // Spawn
    //    GameObject spawnedChain = Instantiate(chainPrefab, spawnPos, Quaternion.identity);

    //    // Direction
    //    spawnedChain.transform.forward = direction;

    //    ChainController chainCont = spawnedChain.GetComponent<ChainController>();

    //    chainCont.Initialise(hitPoint, speed, shootPoint, hand, grapple, pickup);
    //    // 
    //    chainCont.posToMoveTowards = hitPoint; /*= (hitPoint - spawnPos).normalized * speed;*/

    //}

    //public void SpawnChain(Vector3 spawnPos, Vector3 direction, float speed, Vector3 hitPoint, GameObject pickUpObject, bool grapple, bool pickup)
    //{
    //    // Spawn
    //    GameObject spawnedChain = Instantiate(chainPrefab, spawnPos, Quaternion.identity);

    //    // Direction
    //    spawnedChain.transform.forward = direction;

    //    ChainController chainCont = spawnedChain.GetComponent<ChainController>();

    //    chainCont.Initialise(hitPoint, speed, shootPoint, hand, pickUpObject, grapple, pickup);
    //    // 
    //    chainCont.posToMoveTowards = hitPoint; /*= (hitPoint - spawnPos).normalized * speed;*/

    //}

    //public void ObjectBeingHeld()
    //{
    //    isObjectHeld = true;
    //}

    //public void ObjectDropped()
    //{
    //    isObjectHeld = false;
    //}

}
