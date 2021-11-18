using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShippingContainerSound : MonoBehaviour
{
    public bool grounded = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(grounded);
    }

    public bool GroundCheck()
    {
        Ray ray = new Ray(transform.position, Vector3.down); // Shoot a ray down

        RaycastHit hit;

        if (Physics.SphereCast(ray, 0.25f, out hit, 5f)) // If the ray hits the ground
        {
            grounded = true; // is the player on the ground?

            return true;
        }

        grounded = false;
        return false;

    }
}
