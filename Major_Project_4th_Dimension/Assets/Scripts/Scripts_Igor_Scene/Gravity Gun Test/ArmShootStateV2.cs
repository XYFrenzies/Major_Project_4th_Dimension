using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmShootStateV2 : ArmBaseState
{
    private PlayerInput playerInput;
    private InputAction shootAction;
    private InputAction shotAction;
    public bool shooting = false;

    public ArmShootStateV2(ArmStateManager arm) : base(arm)
    {

    }


    public override void EnterState()
    {
        playerInput = armStateMan.GetComponent<PlayerInput>();

        shootAction = playerInput.actions["HookShot"];
        shotAction = playerInput.actions["Test"];
        //shootAction.performed += context => ShootArm();
        //shootAction.canceled += context => UnShootArm();
        // called once when switch from some other state to this state.

        Debug.Log("Shoot enter");

    }


    public override void ExitState()
    {
        Debug.Log("Shoot state exited");
        // called once when switching from this state to another state
        //shootAction.performed -= context => ShootArm();

    }

    public override void UpdateState()
    {
        if (Mouse.current.leftButton.isPressed)
            ShootArm();
        else
            UnShootArm();
        //if (Keyboard.current.gKey.isPressed)
        //{
        //    //ThrowHookShot();
        //    Shot();
        //}
        if (shooting)
        {
            Shooting();
        }

    }


    public void ThrowHookShot(/*InputAction.CallbackContext context*/)
    {
        Debug.Log("Fired hook shot");
        RaycastHit hit;

        Ray ray = armStateMan.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));


        if (armStateMan.isObjectHeld)
        {

            if (Physics.Raycast(ray, out hit, armStateMan.shootRange, ~armStateMan.layerMask))
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
        if (Physics.Raycast(ray, out hit, armStateMan.shootRange, ~armStateMan.layerMask))
        {
            armStateMan.hitPoint = hit.point;
            armStateMan.hitObject = hit.collider.gameObject;

            if (hit.transform.CompareTag("CanHookShotTowards")) // hit grapple point
            {

                Debug.Log("Can hook shot towards");
                OnHookShotHit(armStateMan.grappleState);

            }
            else if (hit.transform.CompareTag("MoveableToMe")) // pick up object
            {
                Debug.Log("can pick up");
                OnHookShotHit(armStateMan.pickUpState);

            }
            else if (hit.transform.CompareTag("BigPullObject")) // pull object towards me
            {
                Debug.Log("can pull to me");

                armStateMan.localPoint = armStateMan.hitObject.transform.InverseTransformPoint(hit.point);

                OnHookShotHit(armStateMan.pullState);
            }
            else // hit object but cant pick up, pull or grapple
            {
                Debug.Log("Hit other thing");

            }


        }
        else
        {
            Debug.Log("missed");
            armStateMan.hitPoint = ray.origin + (armStateMan.cam.transform.forward * armStateMan.shootRange);
        }

        //armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.HookShotThrown;


    }


    public void OnHookShotHit(ArmBaseState state)
    {
        armStateMan.SwitchState(state);
    }

    public void ShootArm()
    {
        shooting = true;
        armStateMan.lineRenderer.enabled = true;
        Debug.Log("Shooting arm");
    }
    private void UnShootArm()
    {
        shooting = false;
        armStateMan.lineRenderer.enabled = false;
        if (!armStateMan.isObjectHeld)
            GameEvents.current.StopPullObject();
        //if(armStateMan.isObjectHeld)
        //{
        //    armStateMan.isPutDown = true;
        //    OnHookShotHit(armStateMan.pauseState);
        //}
    }

    public void Shooting()
    {
        RaycastHit hit;

        Ray ray = armStateMan.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        if (armStateMan.isObjectHeld)
        {

            if (Physics.Raycast(ray, out hit, armStateMan.shootRange, ~armStateMan.layerMask))
            {

                armStateMan.hitPoint = hit.point;


            }
            else // put back at point of chain's full length
            {

                armStateMan.hitPoint = ray.origin + (armStateMan.cam.transform.forward * armStateMan.shootRange);

            }
            armStateMan.hitObject.GetComponent<PullObject>().isPushing = true;

            return;
        }

        if (Physics.Raycast(ray, out hit, armStateMan.shootRange, ~armStateMan.layerMask))
        {
            armStateMan.hitPoint = hit.point;
            armStateMan.hitObject = hit.collider.gameObject;

            if (hit.transform.CompareTag("CanHookShotTowards")) // hit grapple point
            {

                Debug.Log("Can hook shot towards");
                OnHookShotHit(armStateMan.grappleState);

            }
            else if (hit.transform.CompareTag("MoveableToMe")) // pick up object
            {
                Debug.Log("can pick up");

                GameEvents.current.PullObject(hit.collider.GetComponent<PullObject>().id);
                //OnHookShotHit(armStateMan.pickUpState);

            }
            else if (hit.transform.CompareTag("BigPullObject")) // pull object towards me
            {
                Debug.Log("can pull to me");

                armStateMan.localPoint = armStateMan.hitObject.transform.InverseTransformPoint(hit.point);

                OnHookShotHit(armStateMan.pullState);
            }
            else // hit object but cant pick up, pull or grapple
            {
                Debug.Log("Hit other thing");
                GameEvents.current.StopPullObject();
            }


        }
        else
        {
            Debug.Log("missed");
            armStateMan.hitPoint = ray.origin + (armStateMan.cam.transform.forward * armStateMan.shootRange);
            GameEvents.current.StopPullObject();

        }
    }
}
