using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class PullObjectToPlayer : MonoBehaviour
{
    public string[] objectsICanPull;
    Rigidbody rb;
    public float lerpSpeed = 30.0f;
    public Transform holder;
    public UnityEvent hookShotOnTrigger;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //if (rb)
        //{
        //    // Move the object to the holder and lerp its motion to make it smooth
        //    rb.MovePosition(Vector3.Lerp(rb.position, holder.position, lerpSpeed));

        //    // Throw the object
        //    //if (Input.GetMouseButtonDown(1))
        //    //{
        //    //    rb.isKinematic = false;
        //    //    rb.AddForce(cam.transform.forward * throwForce, ForceMode.VelocityChange);
        //    //    rb = null;
        //    //}
        //}
    }

    public void OnTriggerEnter(Collider other)
    {
        if (objectsICanPull.Contains(other.gameObject.tag))
        {
            rb = other.gameObject.GetComponent<Rigidbody>();
            if (rb)
            {
                //rb.isKinematic = true;
                if (hookShotOnTrigger != null)
                    hookShotOnTrigger.Invoke();

            }
        }
    }
}
