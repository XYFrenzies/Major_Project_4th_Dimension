using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingImpactTestScript : MonoBehaviour
{
    Rigidbody rb;
    public float distToGround;
    public bool isGrounded;
    private bool wasFalling;
    private bool wasGrounded;
    private Animator anim;
    private GameObject cinemachine;

    private float fallDistance;
    private float startFallPos;

    public float minFallDist = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        distToGround = GetComponent<CapsuleCollider>().bounds.extents.y;
        cinemachine = GameObject.Find("CM StateDrivenCamera1");
        anim = cinemachine.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f))
        {
            isGrounded = true;
        } else
        {
            isGrounded = false;
        }

        if(isGrounded == true)
        {
            anim.Play("RegCam");
        }

        if(isGrounded == false)
        {
            anim.Play("FallCam");
        }
        

        if (!wasFalling && isFalling) startFallPos = transform.position.y;
        
        if(!wasGrounded && isGrounded)
        {
            doThing();
        }

        wasGrounded = isGrounded;
    }

    void doThing()
    {
        if(startFallPos - transform.position.y > minFallDist)
        {
            Debug.Log("ow");
        }
        Debug.Log("Player Fell " + (startFallPos - transform.position.y));
    }

    bool isFalling { get { return (!isGrounded && rb.velocity.y < 0); } }
}
