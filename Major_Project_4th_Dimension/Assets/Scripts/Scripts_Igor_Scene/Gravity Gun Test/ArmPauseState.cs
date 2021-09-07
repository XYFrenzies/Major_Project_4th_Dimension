using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmPauseState : ArmBaseState
{
    Rigidbody rb;
    //private PlayerInput playerInput;
    //private InputAction shootAction;
    PullObject pullObj;
    //bool canShoot = false;
    float timer = 0;

    public ArmPauseState(ArmStateManager arm) : base(arm)
    {

    }
    public override void EnterState()
    {
        Debug.Log("Entered pause state");
        pullObj = armStateMan.hitObject.GetComponent<PullObject>();
        rb = armStateMan.hitObject.GetComponent<Rigidbody>();
        //playerInput = armStateMan.GetComponent<PlayerInput>();
        //shootAction = playerInput.actions["HookShot"];


        if (armStateMan.isPutDown) // put down
        {
            armStateMan.hitObject.layer = LayerMask.NameToLayer("Default");
            armStateMan.hitObject.transform.SetParent(null);

            armStateMan.isPutDown = false;
            pullObj.isPushing = false;
            rb.velocity = Vector3.zero;

            rb.isKinematic = false;
            rb.useGravity = true;

            armStateMan.isObjectHeld = false;
            armStateMan.hitObject = null;

            armStateMan.lineRenderer.enabled = true;
        }
        else // pick up
        {
            armStateMan.hitObject.layer = LayerMask.NameToLayer("Hold");
            armStateMan.hitObject.transform.SetParent(armStateMan.holdPoint);

            rb.isKinematic = true;
            rb.useGravity = false;

            armStateMan.isObjectHeld = true;
            armStateMan.lineRenderer.enabled = false;

        }

    }

    public override void ExitState()
    {
        Debug.Log("Exited pause state");
        //armStateMan.hitObject.transform.SetParent(null);
        //armStateMan.hitObject.layer = LayerMask.NameToLayer("Default");
        //rb.isKinematic = false;
        //rb.useGravity = true;
        //armStateMan.isObjectHeld = false;
        //armStateMan.hitObject = null;
        //armStateMan.lineRenderer.enabled = false;
        //pullObj.isPushing = false;
    }

    public override void UpdateState()
    {
        timer += Time.deltaTime;
        Debug.Log(timer);
        if (timer >= 2f)
        {
            timer = 0;
            //canShoot = true;
            armStateMan.SwitchState(armStateMan.shootState);
        }
        //if (canShoot && Mouse.current.leftButton.isPressed)
        //    if (Mouse.current.leftButton.isPressed)
        //    {
        //        Debug.Log("Hold state shoot action triggered");
        //        RaycastHit hit;

        //        Ray ray = armStateMan.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        //        if (Physics.Raycast(ray, out hit, armStateMan.shootRange, ~armStateMan.layerMask))
        //        {

        //            armStateMan.hitPoint = hit.point;


        //        }
        //        else // put back at point of chain's full length
        //        {

        //            armStateMan.hitPoint = ray.origin + (armStateMan.cam.transform.forward * armStateMan.shootRange);

        //        }
        //        pullObj.isPushing = true;
        //        armStateMan.lineRenderer.enabled = true;

        //        //armStateMan.hitObject.transform.position = Vector3.MoveTowards(armStateMan.hitObject.transform.position, )
        //    }
        //    else
        //    {
        //        pullObj.isPushing = false;
        //        armStateMan.SwitchState(armStateMan.shootState);
        //    }
    }
}
