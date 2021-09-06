using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmHoldState : ArmBaseState
{
    Rigidbody rb;
    private PlayerInput playerInput;
    private InputAction shootAction;
    PullObject po;

    public ArmHoldState(ArmStateManager arm) : base(arm)
    {

    }
    public override void EnterState()
    {
        po = armStateMan.hitObject.GetComponent<PullObject>();
        po.isPulling = false;
        Debug.Log("Entered hold state");
        playerInput = armStateMan.GetComponent<PlayerInput>();
        shootAction = playerInput.actions["HookShot"];

        armStateMan.hitObject.layer = LayerMask.NameToLayer("Hold");
        rb = armStateMan.hitObject.GetComponent<Rigidbody>();

        //rb.isKinematic = true;
        armStateMan.hitObject.transform.SetParent(armStateMan.holdPoint);
        rb.velocity = Vector3.zero;

        armStateMan.isObjectHeld = true;
        armStateMan.lineRenderer.enabled = false;
        //rb = null;

    }

    public override void ExitState()
    {
        Debug.Log("Exited hold state");

    }

    public override void UpdateState()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Debug.Log("Hold state shoot action triggered");
            RaycastHit hit;

            Ray ray = armStateMan.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(ray, out hit, armStateMan.shootRange, ~armStateMan.layerMask))
            {

                armStateMan.hitPoint = hit.point;


            }
            else // put back at point of chain's full length
            {

                armStateMan.hitPoint = ray.origin + (armStateMan.cam.transform.forward * armStateMan.shootRange);

            }
            po.isPushing = true;

            //armStateMan.hitObject.transform.position = Vector3.MoveTowards(armStateMan.hitObject.transform.position, )
        }
        else
        {
            po.isPushing = false;
        }
    }
}
