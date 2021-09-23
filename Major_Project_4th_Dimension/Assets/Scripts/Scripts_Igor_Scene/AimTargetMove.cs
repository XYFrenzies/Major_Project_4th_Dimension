/*
Author:		        Igor Doslov
Date Created:       1/4/2021
Date Modified:      8/4/2021
Purpose:	        Draws a line in scene view to show the ray from the player 
                    and also makes the player aim at the target floating in front of them,
                    which has its position lerped to make aiming movement smooth
*/

using UnityEngine;
using UnityEngine.InputSystem;

public class AimTargetMove : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction lookAction;
    private Vector2 mouseInput;

    public float weaponRange = 200.0f;
    private Camera cam;
    RaycastHit hit;
    public GameObject target;
    public float lerpSpeed = 10.0f;
    public LayerMask layerMask;
    public ArmStateManager arm;

    // Start is called before the first frame update
    void Awake()
    {
        cam = Camera.main;
        lookAction = playerInput.actions["Look"];
        arm = GetComponent<ArmStateManager>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        mouseInput = lookAction.ReadValue<Vector2>();

        Vector3 lastPos = target.transform.position;

        //Vector3 rayOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));
        Ray rayOrigin = new Ray();
        Vector3 lineOrigin = cam.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, 0));

        Debug.DrawRay(lineOrigin, cam.transform.forward * weaponRange, Color.green);

        RaycastHit hit;
        Vector3 aimPoint;
        Ray crosshair = new Ray(cam.transform.position, cam.transform.forward);
        if (Physics.Raycast(crosshair, out hit, weaponRange))
        {
            aimPoint = hit.point;
        }
        else
        {
            aimPoint = crosshair.origin + crosshair.direction * weaponRange;
        }

        Ray ray = new Ray(arm.holdPoint.position, aimPoint - arm.holdPoint.position);

        // Make the target sphere move to the point where the ray hits
        if (Physics.Raycast(ray, out hit, weaponRange, ~layerMask))
        {
            target.transform.position = Vector3.Lerp(lastPos, hit.point, lerpSpeed * Time.fixedDeltaTime);
        }
        else // Makes the target sphere float at a point along the ray at a distance
        {
            target.transform.position = Vector3.Lerp(lastPos, cam.transform.forward * weaponRange, lerpSpeed * Time.fixedDeltaTime);
        }


        // Make the target sphere move to the point where the ray hits
        //if (Physics.Raycast(rayOrigin, cam.transform.forward, out hit, weaponRange, ~layerMask))
        //{
        //    target.transform.position = Vector3.Lerp(lastPos, hit.point, lerpSpeed * Time.fixedDeltaTime);
        //}
        //else // Makes the target sphere float at a point along the ray at a distance
        //{
        //    target.transform.position = Vector3.Lerp(lastPos, cam.transform.forward * weaponRange, lerpSpeed * Time.fixedDeltaTime);
        //}
    }
}
