using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is to set the gameobjects in place.
/// </summary>
public class SetInPlace : MonoBehaviour
{
    [SerializeField] private Vector3 setPosition = new Vector3(-14.59f, 0f, -15.31f);
    [SerializeField] private float speedToLocation = 0.02f;
    private bool isMovingTo = false;
    private bool hasMoved = false;
    private Vector3 areaPos = new Vector3(6f, -2.75f, -14.57f);
    private Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isMovingTo) 
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(areaPos.x, transform.position.y, areaPos.z), Time.deltaTime * speedToLocation);
            if (hasMoved && rb.velocity == Vector3.zero)
            {
                isMovingTo = true;
                rb.constraints = RigidbodyConstraints.FreezeAll;
            }
            hasMoved = true;
        }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        isMovingTo = true;
    }
}
