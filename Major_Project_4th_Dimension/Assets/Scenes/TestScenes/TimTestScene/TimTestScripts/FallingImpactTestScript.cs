using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingImpactTestScript : MonoBehaviour
{
    Rigidbody rb;
    public float distToGround;
    public bool isGrounded;
    private Animator anim;
    private GameObject cinemachine;

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
    }
}
