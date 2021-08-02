using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ChainShoot : MonoBehaviour
{
    public float waveScale = 1f;
    //private HoldObject holdObj;
    public Transform shootPoint;
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

    public void OnHookShot(InputAction.CallbackContext context)
    {

        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        ThrowHookShot();

    }

    private void Awake()
    {
        player = GetComponent<PlayerControllerNew>();
        cam = Camera.main;
        //holdObj = GetComponentInChildren<HoldObject>();
    }

    private void Update()
    {
        shootPoint.LookAt(posToLookAt);
        if(pullCheck)
        {
            PullObject(objectToPull);
        }
    }


    public void ThrowHookShot()
    {
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
                //player.currentState = PlayerControllerNew.State.HookShotFlying;
                Debug.Log("Can hook shot towards");
                //return;
                // SpawnChain(shootPoint.position, shootPoint.forward, chainSpeed, hit.point, true, false);
            }
            else if (hit.transform.CompareTag("MoveableToMe")) // pick up object
            {
                Debug.Log("can pick up");
                //objectToPickUpOrDrop = hit.transform.gameObject;
                //isObjectHeld = true;
                //SpawnChain(shootPoint.position, shootPoint.forward, chainSpeed, hit.point, hit.transform.gameObject, false, true);

            }
            else if (hit.transform.CompareTag("BigPullObject")) // pull object towards me
            {
                Debug.Log("can pull to me");
                objectToPull = hit.transform.gameObject;
                //pullCheck = true;

                
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

        player.currentState = PlayerControllerNew.State.HookShotThrown;

    }

    public void HandleHookShotThrow()
    {
        CalculateLineRenderer();
        
        //StopHookShot();
        player.currentState = PlayerControllerNew.State.Normal;
    }

    private void StopHookShot()
    {
        player.currentState = PlayerControllerNew.State.Normal;
    }

    private void CalculateLineRenderer()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, shootPoint.position);
        lineRenderer.SetPosition(1, hitPos);
        //lineRenderer.SetPosition(1, objectToPull.transform.position);

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

        if(pullObject.CompareTag("BigPullObject"))
        {
            Rigidbody rb = pullObject.GetComponent<Rigidbody>();
            if(player.GetComponent<Rigidbody>().velocity.sqrMagnitude > 0f)
            {
                lineRenderer.SetPosition(1, pullObject.transform.position);
                rb.AddForce(player.transform.position - pullObject.transform.position);
                Debug.Log("pulling " + pullObject.name + "Player velocity " + player.GetComponent<Rigidbody>().velocity);
                
            }
        }
    }

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
