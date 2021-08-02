//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class ChainController : MonoBehaviour
//{
//    [HideInInspector]
//    //public Rigidbody rb;
//    private Transform shootPoint;
//    private Transform myHand;
//    private Transform holdObjectPoint;
//    private GameObject objectToPickUp;
//    private PlayerController player;
//    public GameEvent passObjectToHoldPoint;
//    public GameEvent readyToDropObject;

//    //public bool hitObject;
//    private Vector3 target;
//    private float mySpeed;
//    [HideInInspector]
//    public Vector3 posToMoveTowards;
//    private bool canGrapple;
//    private bool canPickUp;
//    public List<GameObject> myChainLinks = new List<GameObject>();
//    private int currentLink = 0;
//    private float timer = 0f;
//    public float timeBetweenLinks = 0.2f;
//    private bool isExtending = true;
//    private bool shouldPauseTime = false;
//    private bool firstTime = true;
//    private bool hasTouchedObject = false;
//    [HideInInspector]
//    public bool isCloseToHoldPoint = false;
//    public bool isObjectBeingHeld = false;

//    private void Awake()
//    {
//        // rb = GetComponent<Rigidbody>();
//        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
//        holdObjectPoint = GameObject.FindGameObjectWithTag("HoldPoint").transform;
//    }

//    // Start is called before the first frame update
//    void Start()
//    {

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (firstTime)
//        {
//            //if (!canGrapple && !canPickUp && Vector3.Distance(transform.position, target) <= 1f) // missed
//            //{

//            //    posToMoveTowards = shootPoint.position;
//            //    isExtending = false;
//            //    firstTime = false;
//            //}

//            if (canGrapple && !canPickUp && Vector3.Distance(transform.position, target) <= 1f) // hit something we can grapple to. Chain needs to stop at point
//            {

//                posToMoveTowards = transform.position;
//                isExtending = false;
//                firstTime = false;

//                //shouldPauseTime = true;
//                //Invoke("UnPause", 1f);
//                // make player fly here
//                player.flyToTarget = target;
//                player.currentState = PlayerController.State.HookShotFlying;


//            }
//            else if (!canGrapple && !canPickUp && Vector3.Distance(transform.position, target) <= 1f) // chain retracts if hit object not grappleable or missed everything
//            {
//                posToMoveTowards = shootPoint.position;
//                isExtending = false;
//                firstTime = false;
//                readyToDropObject.Raise();
//                if (objectToPickUp != null)
//                    objectToPickUp.GetComponent<Rigidbody>().useGravity = true;
//            }
//            else if (!canGrapple && canPickUp && Vector3.Distance(transform.position, target) <= 1f) // pick up object
//            {
//                posToMoveTowards = shootPoint.position;
//                isExtending = false;
//                firstTime = false;
//                hasTouchedObject = true; // chain has come close to object to pick up
//            }
//        }

//        // if !objectbeingheld then do this if statement
//        if (objectToPickUp != null && hasTouchedObject) // 
//        {
//            objectToPickUp.GetComponent<Rigidbody>().useGravity = false;
//            // Move the object to the shoot point
//            objectToPickUp.transform.position = Vector3.MoveTowards(objectToPickUp.transform.position, posToMoveTowards, mySpeed * Time.deltaTime);
//            // If the object gets close to the shoot point, make its parent the shoot point
//            if (Vector3.Distance(objectToPickUp.transform.position, holdObjectPoint.position) <= 2f)
//            {
//                //isCloseToHoldPoint = true;
//                //passObjectToHoldPoint.Raise();
//                //objectToPickUp.GetComponent<Collider>().enabled = false;
//                objectToPickUp.layer = LayerMask.NameToLayer("Chain");
//                objectToPickUp.GetComponent<Rigidbody>().isKinematic = true;
//                objectToPickUp.transform.SetParent(holdObjectPoint);
                
//            }
//        }

//        transform.position = Vector3.MoveTowards(transform.position, posToMoveTowards, mySpeed * Time.deltaTime);

//        if (!shouldPauseTime)
//            timer += Time.deltaTime;

//        if (timer >= timeBetweenLinks)
//        {
//            if (isExtending)
//            {
//                myHand.gameObject.SetActive(false);
//                player.currentState = PlayerController.State.HookShotThrown;
//                if (currentLink < myChainLinks.Count - 1)
//                {
//                    myChainLinks[currentLink].SetActive(true);
//                    currentLink++;
//                    timer = 0f;
//                }
//            }
//            else
//            {
//                if (currentLink >= 0)
//                {
//                    myChainLinks[currentLink].SetActive(false);
//                    currentLink--;
//                    timer = 0f;
//                }
//                if (currentLink == 0)
//                {
//                    myHand.gameObject.SetActive(true);
//                    player.currentState = PlayerController.State.Normal;


//                    Destroy(gameObject);
//                }

//            }

//        }
//    }

//    public void UnPause()
//    {
//        player.flyToTarget = target;
//        player.canFly = true;
//        shouldPauseTime = false;
//    }

//    public void Initialise(Vector3 hitPoint, float speed, Transform sPoint, Transform hand, bool grapple, bool pickup)
//    {
//        target = hitPoint;
//        //hitObject = wasSomethingHit;
//        mySpeed = speed;
//        shootPoint = sPoint;
//        myHand = hand;
//        canGrapple = grapple;
//        canPickUp = pickup;
//    }

//    public void Initialise(Vector3 hitPoint, float speed, Transform sPoint, Transform hand, GameObject pickUpObject, bool grapple, bool pickup)
//    {
//        target = hitPoint;

//        mySpeed = speed;
//        shootPoint = sPoint;
//        myHand = hand;
//        objectToPickUp = pickUpObject;
//        canGrapple = grapple;
//        canPickUp = pickup;
//    }



//    //private void OnCollisionEnter(Collision collision)
//    //{
//    //    if (hitGrappleableObject)
//    //    {
//    //        posToMoveTowards = transform.position;
//    //    }
//    //    else
//    //    {
//    //        posToMoveTowards = shootPoint.position;
//    //    }
//    //}

//}
