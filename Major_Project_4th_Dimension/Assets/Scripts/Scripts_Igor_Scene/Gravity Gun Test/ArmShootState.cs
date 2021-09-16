using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmShootState : ArmBaseState
{
    //private PlayerInput playerInput;
    //private InputAction shootAction;
    //private InputAction throwAction;

    private bool shooting;

    public ArmShootState(ArmStateManager arm) : base(arm)
    {

    }


    public override void EnterState()
    {
        //playerInput = armStateMan.GetComponent<PlayerInput>();

        //shootAction = playerInput.actions["HookShot"];
        //throwAction = playerInput.actions["ThrowObject"];

        armStateMan.shootAction.performed += ShootingArm;
        armStateMan.shootAction.canceled += UnShootingArm;

        //armStateMan.throwAction.performed += ThrowObject;
        // called once when switch from some other state to this state.

        //Debug.Log("Shoot enter");

    }


    public override void ExitState()
    {
        //Debug.Log("Shoot state exited");
        // called once when switching from this state to another state
        armStateMan.shootAction.performed -= ShootingArm;
        armStateMan.shootAction.canceled -= UnShootingArm;
        //armStateMan.throwAction.performed -= ThrowObject;
        shooting = false;

    }

    public override void UpdateState()
    {
        Debug.DrawRay(armStateMan.shootPoint.position, armStateMan.shootPoint.forward, Color.green);
        //if (shootAction.triggered)
        //{
        //    ThrowHookShot();
        //}
        if (shooting)
            ShootArm();
    }


    public void ShootArm(/*InputAction.CallbackContext context*/)
    {
        //Debug.Log("Fired hook shot");
        RaycastHit hit;

        Ray ray = armStateMan.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));


        if (armStateMan.isObjectHeld)
        {

            if (Physics.Raycast(ray, out hit, armStateMan.shootRange, ~armStateMan.holdObjectLayerMask))
            {
                armStateMan.hitPoint = hit.point;
            }
            else // put back at point of chain's full length
            {
                armStateMan.hitPoint = ray.origin + (armStateMan.cam.transform.forward * armStateMan.shootRange);
            }

            OnHookShotHit(armStateMan.putDownState);

            armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.HookShotThrown;

            return;
        }
        if (Physics.Raycast(ray, out hit, armStateMan.shootRange, ~armStateMan.holdObjectLayerMask))
        {
            armStateMan.hitPoint = hit.point;
            armStateMan.hitObject = hit.collider.gameObject;

            if (hit.transform.CompareTag("CanHookShotTowards")) // hit grapple point
            {
                armStateMan.distToTarget90 = Vector3.Distance(armStateMan.transform.position, hit.point);
                armStateMan.distToTarget90 *= armStateMan.percentageOfDistToTarget;
                //Debug.Log("Can hook shot towards");
                OnHookShotHit(armStateMan.grappleState);

            }
            else if (hit.transform.CompareTag("MoveableToMe")) // pick up object
            {
                shooting = false;

                //Debug.Log("can pick up");
                OnHookShotHit(armStateMan.pickUpState);

            }
            else if (hit.transform.CompareTag("BigPullObject")) // pull object towards me
            {
                //Debug.Log("can pull to me");

                armStateMan.localPoint = armStateMan.hitObject.transform.InverseTransformPoint(hit.point);

                OnHookShotHit(armStateMan.pullState);
            }
            else // hit object but cant pick up, pull or grapple
            {
                //Debug.Log("Hit other thing");

            }


        }
        else
        {
            //Debug.Log("missed");
            armStateMan.hitPoint = ray.origin + (armStateMan.cam.transform.forward * armStateMan.shootRange);
        }

        //armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.HookShotThrown;


    }


    public void ShootingArm(InputAction.CallbackContext context)
    {
        //if (context.phase != InputActionPhase.Performed)
        //{
        //    return;
        //}
        //else
        //{
        if (armStateMan.lineRenderer != null)
        {
            shooting = true;
            armStateMan.lineRenderer.enabled = true;
        }
            //Debug.Log("Shooting arm");
        //}

    }
    private void UnShootingArm(InputAction.CallbackContext context)
    {
        //if (context.phase != InputActionPhase.Canceled)
        //{
        //    return;
        //}
        //else
        // {
        if (armStateMan.lineRenderer != null)
        {
            shooting = false;
            armStateMan.lineRenderer.enabled = false;
        }
            //Debug.Log("Unshooting arm");
        //}
    }

    public void ThrowObject(InputAction.CallbackContext context)
    {
        //Debug.Log("Throwing object");
        if (armStateMan.isObjectHeld)
        {
            RaycastHit hit;

            Ray ray = armStateMan.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

            if (Physics.Raycast(ray, out hit, armStateMan.shootRange, ~armStateMan.holdObjectLayerMask))
            {
                armStateMan.hitPoint = hit.point;
            }
            else // put back at point of chain's full length
            {
                armStateMan.hitPoint = ray.origin + (armStateMan.cam.transform.forward * armStateMan.shootRange);
            }

            Vector3 dir = armStateMan.hitPoint - armStateMan.holdPoint.position;
            armStateMan.hitObject.layer = LayerMask.NameToLayer("Default");
            armStateMan.hitObject.GetComponent<Rigidbody>().isKinematic = false;
            armStateMan.hitObject.transform.SetParent(null);
            armStateMan.isObjectHeld = false;
            Rigidbody rb = armStateMan.hitObject.GetComponent<Rigidbody>();
            rb.useGravity = true;
            rb.AddForce(dir.normalized * armStateMan.throwForce, ForceMode.Impulse);
        }
    }

    public void OnHookShotHit(ArmBaseState state)
    {
        armStateMan.SwitchState(state);
    }

}
