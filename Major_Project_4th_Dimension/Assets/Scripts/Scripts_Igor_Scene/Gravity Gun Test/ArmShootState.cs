using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmShootState : ArmBaseState
{
    private PlayerInput playerInput;
    private InputAction shootAction;



    public ArmShootState(ArmStateManager arm) : base(arm)
    {

    }


    public override void EnterState()
    {
        playerInput = armStateMan.GetComponent<PlayerInput>();

        shootAction = playerInput.actions["HookShot"];
        //shootAction.performed += context => ThrowHookShot(context);
        // called once when switch from some other state to this state.

        Debug.Log("Shoot enter");

    }

    public override void ExitState()
    {
        Debug.Log("Shoot state exited");
        // called once when switching from this state to another state
        //shootAction.performed -= context => ThrowHookShot(context);

    }

    public override void UpdateState()
    {

        if (shootAction.triggered)
        {
            ThrowHookShot();
            Debug.Log("shot triggered");
        }

    }


    public void ThrowHookShot(/*InputAction.CallbackContext context*/)
    {
        Debug.Log("Fired hook shot");
        RaycastHit hit;

        Ray ray = armStateMan.cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));

        //if (armStateMan.pull)
        //{
        //    armStateMan.pullCheck = false;

        //    return;
        //}

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

            //pickup = false;
            //place = true;
            //currentHookShotState = HookShotState.Place;
            OnHookShotHit(armStateMan.putDownState);

            armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.HookShotThrown;
            //if (switchCam.isAimOn) // arm already up
            //{
            //    player.currentState = PlayerControllerCinemachineLook2.State.HookShotThrown;
            //    currentHookShotState = HookShotState.Place;
            //}
            //else // arm is down. Needs to go up first to then be able to fire hookshot
            //{

            //    switchCam.StartShoot();
            //}
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
                //armStateMan.pullCheck = true;
                //armStateMan.pull = true;
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
        }

        //armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.HookShotThrown;


    }


    public void OnHookShotHit(ArmBaseState state)
    {
        armStateMan.SwitchState(state);
    }

}
