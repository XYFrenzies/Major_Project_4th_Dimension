using UnityEngine;
using UnityEngine.InputSystem;


public class GravArm : MonoBehaviour
{
    public SwitchCam1 switchCam;
    LineRenderer lineRenderer;
    public Animator anim;


    private Vector3 hookshotPosition;

    public Transform shootPoint;
    public Transform holdPoint;
    public Transform hand;
    public Transform handStartPos;

    private GameObject objectToPickUpOrDrop;

    private Camera cam;
    private PlayerControllerCinemachineLook2 player;
    public float hookShotRange = 50f;


    private Vector3 localPoint;
    private PlayerInput playerInput;
    private InputAction hookshotAction;
    //private InputAction throwAction;


    private GameObject objectToPull;
    [HideInInspector]
    public HookShotState currentHookShotState;

    [HideInInspector]
    public bool isObjectHeld = false;
    [HideInInspector]
    public bool isThrow = false;
    private bool pullCheck = false;
    [HideInInspector]
    public bool pull = false;
    private bool pickup = false;
    [HideInInspector]
    public bool fly = false;
    private bool place = false;

    private bool missed = false;


    public GameObject grappleHandle;
    private GameObject newGrappleHandle;
    public SpringJoint springJoint;
    //Vector3 grappleLocal;
    public LayerMask layerMask;


    public enum HookShotState
    {
        Normal,
        Throw,
        Pull,
        Pickup,
        Place,
        Fly,
        ReturnHand,
    }

    private void Awake()
    {
        player = GetComponent<PlayerControllerCinemachineLook2>();
        cam = Camera.main;
        currentHookShotState = HookShotState.Normal;
        lineRenderer = GetComponent<LineRenderer>();
        handStartPos.position = hand.position;
        springJoint = GetComponent<SpringJoint>();
        playerInput = GetComponent<PlayerInput>();
        hookshotAction = playerInput.actions["HookShot"];
    }

    private void OnEnable()
    {
        hookshotAction.performed += context => ThrowHookShot(context);
    }

    private void OnDisable()
    {
        hookshotAction.performed -= context => ThrowHookShot(context);

    }


    private void FixedUpdate()
    {

        switch (currentHookShotState)
        {
            default:
            case HookShotState.Normal:
                lineRenderer.enabled = false; ;

                //ThrowHookShot();
                break;
            case HookShotState.Throw:
                lineRenderer.enabled = true;
                StationaryHand();
                HandleHookShotThrow();
                break;

            case HookShotState.Fly:
                lineRenderer.enabled = true;
                StationaryHand();
                //HandleHookshotMovement();
                break;

            case HookShotState.Pickup:
                lineRenderer.enabled = true;
                StationaryHand();
                PickUp(objectToPickUpOrDrop);
                break;

            case HookShotState.Pull:
                lineRenderer.enabled = true;
                StationaryHand();
                PullObject(objectToPull);
                break;

            case HookShotState.Place:
                lineRenderer.enabled = true;
                StationaryHand();
                PlaceObject(hookshotPosition);
                break;

            case HookShotState.ReturnHand:
                lineRenderer.enabled = true;
                StationaryHand();
                ReturnHand();
                break;

        }
        //Debug.Log(currentHookShotState);

    }


    public void ThrowHookShot(InputAction.CallbackContext context)
    {
        //isRetrieve = false;
        RaycastHit hit;

        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (pull)
        {
            pullCheck = false;

            return;
        }

        // Check if object is being held here
        if (isObjectHeld)
        {

            if (Physics.Raycast(ray, out hit, hookShotRange, ~layerMask))
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
            player.currentState = PlayerControllerCinemachineLook2.State.HookShotThrown;
            //if (switchCam.isAimOn) // arm already up
            //{
            //    player.currentState = PlayerControllerCinemachineLook2.State.HookShotThrown;
            //    currentHookShotState = HookShotState.Place;
            //}
            //else // arm is down. Needs to go up first to then be able to fire hookshot
            //{

            //    switchCam.StartShoot();
            //}
            return;
        }
        //Debug.DrawRay(shootPoint.position, ray.direction * 50f, Color.red, 2f);
        //if (Physics.SphereCast(ray.origin, 1f, cam.transform.forward, out hit, hookShotRange))
        if (Physics.Raycast(ray, out hit, hookShotRange, ~layerMask))
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
                localPoint = objectToPull.transform.InverseTransformPoint(hit.point);
                pullCheck = true;
                pull = true;

                Debug.Log("Pullcheck: " + pullCheck);

            }
            else // hit object but cant pick up, pull or grapple
            {
                Debug.Log("Hit other thing");
                missed = true;

            }


        }
        else // missed everything
        {
            Debug.Log("missed everything");
            hookshotPosition = ray.origin + (cam.transform.forward * hookShotRange);
            missed = true;


        }

        {

            //Vector3 lineOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
            //Debug.DrawLine(lineOrigin, cam.transform.forward * hookShotRange, Color.green);

        }
       // if (switchCam.isAimOn) // arm already up
        //{

            player.currentState = PlayerControllerCinemachineLook2.State.HookShotThrown;
            currentHookShotState = HookShotState.Throw;
        //}
        //else // arm is down. Needs to go up first to then be able to fire hookshot
        //{

            //switchCam.StartShoot();
        //}
        //anim.SetBool("IsShooting", true);
    }
    public void HandleHookShotThrow()
    {

        ShootHand();
    }

    public void PullObject(GameObject pullObject)
    {

        if (!pullCheck)
        {
            hand.transform.SetParent(transform);
            Destroy(newGrappleHandle);
            springJoint.connectedAnchor = Vector3.zero;

            springJoint.maxDistance = 0f;
            springJoint.minDistance = 0f;
            ReturnHand();
            pull = false;
            objectToPull = null;
        }

    }

    public void PickUp(GameObject pickupObject)
    {

        Rigidbody rb = pickupObject.GetComponent<Rigidbody>();
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        pickupObject.transform.position = Vector3.MoveTowards(pickupObject.transform.position, holdPoint.position, 50f * Time.deltaTime);
        ReturnHand();
        if (Vector3.Distance(pickupObject.transform.position, holdPoint.position) <= 2f)
        {
            pickupObject.layer = LayerMask.NameToLayer("Hold");
            pickupObject.GetComponent<Rigidbody>().isKinematic = true;
            pickupObject.transform.SetParent(holdPoint);
            isObjectHeld = true;

            player.currentState = PlayerControllerCinemachineLook2.State.Normal;

        }
    }

    public void PlaceObject(Vector3 target)
    {
        if (objectToPickUpOrDrop != null)
        {
            objectToPickUpOrDrop.GetComponent<Rigidbody>().isKinematic = false;
            objectToPickUpOrDrop.transform.SetParent(null);
            ShootHand();
            objectToPickUpOrDrop.transform.position = Vector3.MoveTowards(objectToPickUpOrDrop.transform.position, target, 50f * Time.deltaTime);
            //rb.MovePosition(target * 5f * Time.deltaTime);
            if (Vector3.Distance(objectToPickUpOrDrop.transform.position, target) <= 1f)
            {

                Rigidbody rb = objectToPickUpOrDrop.GetComponent<Rigidbody>();
                rb.useGravity = true;
                isObjectHeld = false;
                objectToPickUpOrDrop.layer = LayerMask.NameToLayer("Default");
                objectToPickUpOrDrop = null;
                place = false;
                currentHookShotState = HookShotState.ReturnHand;
            }
        }
    }

   

    public void ShootHand()
    {

        hand.position = hookshotPosition;
        if (Vector3.Distance(hand.position, hookshotPosition) <= 1f)
        {
            Debug.Log("reached target");

            if (missed)
            {
                missed = false;
                currentHookShotState = HookShotState.ReturnHand;
            }
            if (fly)
                player.currentState = PlayerControllerCinemachineLook2.State.HookShotFlying;
            if (pickup)
                currentHookShotState = HookShotState.Pickup;
            if (pull)
            {
                newGrappleHandle = Instantiate(grappleHandle, objectToPull.transform);
                newGrappleHandle.transform.localPosition = localPoint;
                newGrappleHandle.GetComponent<FixedJoint>().connectedBody = objectToPull.GetComponent<Rigidbody>();
                hand.transform.SetParent(newGrappleHandle.transform);
                hand.transform.localPosition = Vector3.zero;
                springJoint.connectedBody = newGrappleHandle.GetComponent<Rigidbody>();
                springJoint.connectedAnchor = Vector3.zero;
                float distance = Vector3.Distance(transform.position, newGrappleHandle.transform.position);
                springJoint.minDistance = 2.5f;
                springJoint.maxDistance = 2.5f;

                currentHookShotState = HookShotState.Pull;
                player.currentState = PlayerControllerCinemachineLook2.State.Normal;
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
            player.currentState = PlayerControllerCinemachineLook2.State.Normal;
            //hand.gameObject.SetActive(false);
            anim.SetBool("IsShooting", false);
            //if (!switchCam.isAimOn) // if player is not still aiming, put the arm down
            //{
            //    switchCam.StopShoot();

            //}
        }
    }


    public void StationaryHand()
    {
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, shootPoint.position);
        lineRenderer.SetPosition(1, hand.position);
    }

}
