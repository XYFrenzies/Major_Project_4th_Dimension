using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObject : MonoBehaviour
{
    public int id;
    public float speed = 15f;
    public Transform holdPoint;
    public bool isPulling = false;
    public bool isPushing = false;
    public ArmStateManager arm;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        GameEvents.current.onPullObject += PullOn;
        GameEvents.current.onStopPullObject += PullOff;
        
        
    }
    // Update is called once per frame
    void Update()
    {
        if (isPulling)
        {
            transform.position = Vector3.MoveTowards(transform.position, holdPoint.position, speed * Time.deltaTime);
            if(Vector3.Distance(transform.position, holdPoint.position) <= 2f)
            {
                isPulling = false;
                arm.SwitchState(arm.pauseState);
            }
        }

        if (isPushing)
        {
            transform.position = Vector3.MoveTowards(transform.position, arm.hitPoint, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, arm.hitPoint) <= 2f)
            {
                arm.isPutDown = true;
                arm.SwitchState(arm.pauseState);
            }
        }
    }

    public void PullOn(int id)
    {
        if (id == this.id)
        {
            isPulling = true;
            rb.isKinematic = true;
            rb.useGravity = false;
            Debug.Log("Pull on");
        }
    }
    private void PullOff()
    {
        isPulling = false;
        rb.isKinematic = false;
        rb.useGravity = true;
        Debug.Log("Pull off");

    }
}
