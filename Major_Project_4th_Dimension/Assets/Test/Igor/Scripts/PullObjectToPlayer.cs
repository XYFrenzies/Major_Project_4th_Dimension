using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class PullObjectToPlayer : MonoBehaviour
{

    public string[] objectsICanPull;
    public string[] bigObjectsICanPull;
    public Transform player;
    private Rigidbody rb;
    //public float lerpSpeed = 30.0f;
    public Transform holder;
    public UnityEvent hookShotOnTrigger;
    PlayerController playerCont;
    Camera cam;
    public float throwForce = 10f;

    // Start is called before the first frame update
    void Awake()
    {
        
        //Debug.Log(myHealth.value);
        cam = Camera.main;
        if (playerCont == null)
            playerCont = GameObject.FindObjectOfType<PlayerController>();
        if (playerCont.ThrowObjectEvent == null)
            playerCont.ThrowObjectEvent = new UnityEvent();
        if (playerCont.PlaceObjectEvent == null)
            playerCont.PlaceObjectEvent = new UnityEvent();

        playerCont.ThrowObjectEvent.AddListener(ThrowObject);
        playerCont.PlaceObjectEvent.AddListener(PlaceObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (rb)
        {
            //    // Move the object to the holder and lerp its motion to make it smooth
            rb.MovePosition(holder.position);

            //    // Throw the object
            //    //if (Input.GetMouseButtonDown(1))
            //    //{
            //    //    rb.isKinematic = false;
            //    //    rb.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
            //    //    rb = null;
            //    //}
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (playerCont.isObjectHeld)
            playerCont.currentState = PlayerController.State.HookShotMissed;
        if (!playerCont.isObjectHeld)
        {
            if (objectsICanPull.Contains(other.gameObject.tag))
            {
                rb = other.gameObject.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.isKinematic = true;

                    //rb.transform.SetParent(holder.transform, true);

                    // rb.transform.localScale = Vector3.one;
                    //rb.transform.localPosition = Vector3.zero;
                    if (hookShotOnTrigger != null)
                        hookShotOnTrigger.Invoke();

                }
            }
            else if (bigObjectsICanPull.Contains(other.gameObject.tag))
            {
                rb = other.gameObject.GetComponent<Rigidbody>();
                rb.AddForce(player.position - transform.position, ForceMode.Impulse);
            }

        }
    }

    // Listens to the event invoked in PlayerController
    public void ThrowObject()
    {
        rb.isKinematic = false;
        rb.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
        rb = null;

    }

    public void PlaceObject()
    {
        if (rb)
        {
            rb.isKinematic = false;
            rb.velocity = Vector3.zero;
            rb = null;
        }

    }
}
