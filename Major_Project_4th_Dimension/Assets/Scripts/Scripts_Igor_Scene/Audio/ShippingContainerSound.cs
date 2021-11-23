using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippingContainerSound : MonoBehaviour
{
    public bool grounded = false;
    public bool moving = false;
    AudioSource source;
    public ArmStateManager arm;
    public LayerMask mask;
    Rigidbody rb;

    // Start is called before the first frame update
    void Awake()
    {
        source = GetComponent<AudioSource>();
        arm = FindObjectOfType<ArmStateManager>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        MovementCheck();
        if (grounded && !source.isPlaying && (arm.currentState == arm.pullState || arm.playerSM.isPushing))
            SoundPlayer.Instance.PlaySoundEffect("Dragging", source);
        else if (!grounded || (source.isPlaying && (arm.currentState != arm.pullState && !arm.playerSM.isPushing)))
        {
            source.Stop();
        }

    }

    public void GroundCheck()
    {
        //Ray ray = new Ray(transform.position - new Vector3(0f, 1f, 0f), Vector3.down); // Shoot a ray down
        //Debug.DrawRay(ray.origin, ray.direction, Color.green);
        //RaycastHit hit;

        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 0.5f, mask);

        grounded = hitColliders.Length > 0;


        //if (Physics.Raycast(ray, out hit, 3.0f)) // If the ray hits the ground
        ////if (Physics.CheckSphere(transform.position - new Vector3(0f, -1f, 0f), 0.5f, mask))
        //{
        //    grounded = true; // is the player on the ground?

        //    //return true;
        //}
        //else
        //    grounded = false;
        //return false;

    }

    public void MovementCheck()
    {
        var speed = rb.velocity.normalized.sqrMagnitude;

        if (rb.velocity.sqrMagnitude > 0.1f)
        {
            moving = true;
        }
        else
            moving = false;

        Debug.Log(speed);
    }
}
