using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullObject : MonoBehaviour
{
    public int id;
    public Transform holdPoint;
    bool isPulling = false;
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
            transform.position = Vector3.MoveTowards(transform.position, holdPoint.position, 5f * Time.deltaTime);
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
