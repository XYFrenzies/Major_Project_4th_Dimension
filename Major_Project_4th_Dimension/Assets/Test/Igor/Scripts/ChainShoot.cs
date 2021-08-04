using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChainShoot : MonoBehaviour
{
    public float waveScale = 1f;
    //private HoldObject holdObj;
    public Transform shootPoint;
    public Transform holdPoint;
    //public Transform hand;
    public GameObject chainPrefab;
    public GameObject objectToPickUpOrDrop;
    // private PlayerController player;
    private Camera cam;
    private PlayerControllerNew player;
    public float hookShotRange = 50f;
    //public LayerMask grappleLayer;
    public float chainSpeed = 20f;

    public bool canGrapple;
    public bool canPickUp;
    private bool isObjectHeld = false;
    private Vector3 hitPos;
    public Vector3 lookAtPos;
    public Transform posToLookAt;
    private Vector3 currentGrapplePos;
    private float hookShotFlySpeed = 1;
    public LineRenderer lineRenderer;
    private GameObject objectToPull;
    private bool pullCheck = false;
    [HideInInspector]
    public bool showLine = false;
    public HookShotState currentHookShotState;

    public bool pull = false;
    public bool pickup = false;
    public bool fly = false;
    private float stopPullingDistance = 5f;

    public void OnHookShot(InputAction.CallbackContext context)
    {

        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        ThrowHookShot();

    }

    //public void OnPull(InputAction.CallbackContext context)
    //{

    //    if (context.phase != InputActionPhase.Performed)
    //    {
    //        return;
    //    }
    //    if (objectToPull)
    //        ReelIn(objectToPull);

    //}

    public void OnThrow(InputAction.CallbackContext context)
    {

        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        if (!objectToPull)
            Debug.Log("Throw");

    }

    public enum HookShotState
    {
        Normal,
        Pull,
        Pickup,
        Fly
    }

    private void Awake()
    {
        player = GetComponent<PlayerControllerNew>();
        cam = Camera.main;
        currentHookShotState = HookShotState.Normal;
        //holdObj = GetComponentInChildren<HoldObject>();
    }

    private void Update()
    {
        shootPoint.LookAt(posToLookAt);
        if (pullCheck)
        {
            PullObject(objectToPull);
        }
        if (pickup)
            PickUp(objectToPickUpOrDrop);
        if (showLine)
            CalculateLineRenderer();
        //else
        //{
        //    if (lineRenderer != null)
        //        Destroy(lineRenderer);
        //}
    }


    public void ThrowHookShot()
    {
        fly = false;
        pull = false;
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
                // SpawnChain(shootPoint.position, shootPoint.forward, chainSpeed, hit.point, objectToPickUpOrDrop, false, false); // put back object at hit point
            }
            else // put back at point of chain's full length
            {
                //SpawnChain(shootPoint.position, shootPoint.forward, chainSpeed, (ray.origin/*rayOrigin*/ + (cam.transform.forward * hookShotRange)/*ray.GetPoint(hookShotRange*/), objectToPickUpOrDrop, false, false);
            }
            return;
        }

        //if (Physics.SphereCast(rayOrigin,2f , cam.transform.forward ,out hit, hookShotRange))
        if (Physics.Raycast(ray, out hit, hookShotRange))
        {
            hitPos = hit.point;
            currentGrapplePos = shootPoint.position;

            if (hit.transform.CompareTag("CanHookShotTowards")) // hit grapple point
            {
                player.flyToTarget = hit.point;
                showLine = true;
                player.currentState = PlayerControllerNew.State.HookShotFlying;
                fly = true;
                Debug.Log("Can hook shot towards");
                //return;
                // SpawnChain(shootPoint.position, shootPoint.forward, chainSpeed, hit.point, true, false);
            }
            else if (hit.transform.CompareTag("MoveableToMe")) // pick up object
            {
                Debug.Log("can pick up");
                objectToPickUpOrDrop = hit.transform.gameObject;
                //isObjectHeld = true;
                pickup = true;

                showLine = true;
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
                showLine = true;


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

        //player.currentState = PlayerControllerNew.State.HookShotThrown;

    }

    public void HandleHookShotThrow()
    {
        //CalculateLineRenderer();

        //StopHookShot();
        //player.currentState = PlayerControllerNew.State.Normal;
    }

    private void StopHookShot()
    {
        player.currentState = PlayerControllerNew.State.Normal;
    }

    private void CalculateLineRenderer()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, shootPoint.position); // my position
        if (fly)
            lineRenderer.SetPosition(1, hitPos); // fly
        if (pull)
            lineRenderer.SetPosition(1, objectToPull.transform.position); // pull
        if (pickup)
            lineRenderer.SetPosition(1, objectToPickUpOrDrop.transform.position); // pick up


        //currentGrapplePos = Vector3.Lerp(shootPoint.position, hitPos, Time.deltaTime * hookShotFlySpeed);
        //for (int i = 0; i < hookShotRange; i++)
        //{
        //    Vector3 dir = i * (currentGrapplePos - shootPoint.position) / hookShotRange;
        //    float x = dir.magnitude;
        //    float y = Mathf.Sin(x * waveScale);
        //    Vector3 way = dir + shootPoint.position + (shootPoint.rotation * new Vector3(y, 0, 0));
        //    lineRenderer.SetPosition(i, way);
        //}
    }

    public void PullObject(GameObject pullObject)
    {
        //lineRenderer.positionCount = 2;
        //lineRenderer.SetPosition(0, shootPoint.position);
        //lineRenderer.SetPosition(1, hitPos);

        //if (pullObject.CompareTag("BigPullObject"))
        // {
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
        //}
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
