using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class PullObjectToPlayer : MonoBehaviour
{
    public string[] objectsICanPull;
    public Rigidbody rb;
    public float lerpSpeed = 30.0f;
    public Transform holder;
    public UnityEvent hookShotOnTrigger;
    PlayerController playerCont;

    // Start is called before the first frame update
    void Awake()
    {
        if (playerCont == null)
            playerCont = GameObject.FindObjectOfType<PlayerController>();
        if (playerCont.myEvent == null)
            playerCont.myEvent = new UnityEvent();

        playerCont.myEvent.AddListener(DoTheEvent);
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
    }

    // Listens to the event invoked in PlayerController
    public void DoTheEvent()
    {
        Debug.Log("Pull Object To Player");

    }
}
