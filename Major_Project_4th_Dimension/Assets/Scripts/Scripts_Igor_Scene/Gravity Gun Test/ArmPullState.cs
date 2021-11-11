using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmPullState : ArmBaseState
{

    float initialSpringForce;

    float initialMass;
    Rigidbody rb;
    Renderer rend;
    float radius;

    public ArmPullState(ArmStateManager arm) : base(arm)
    {

    }

    public override void EnterState()
    {
        //Debug.Log("Entered Pull state");

        //playerInput = armStateMan.GetComponent<PlayerInput>();
        //shootAction = playerInput.actions["HookShot"];
        //armStateMan.playerInput.actions["HookShot"];

        //armStateMan.shootAction.performed += Shoot;
        //armStateMan.shootAction.canceled += NotShoot;
        //isShooting = true;
        armStateMan.newGrappleHandle = Object.Instantiate(armStateMan.grappleHandle, armStateMan.hitObject.transform);
        armStateMan.newGrappleHandle.transform.localPosition = armStateMan.localPoint;
        armStateMan.newGrappleHandle.GetComponent<FixedJoint>().connectedBody = armStateMan.hitObject.GetComponent<Rigidbody>();

        rend = armStateMan.hitObject.GetComponent<Renderer>();
        radius = rend.bounds.extents.magnitude;

        //hand.transform.SetParent(armStateMan.newGrappleHandle.transform);
        //hand.transform.localPosition = Vector3.zero;
        armStateMan.hitPoint = armStateMan.newGrappleHandle.transform.position;
        //armStateMan.springJoint.connectedBody = armStateMan.newGrappleHandle.GetComponent<Rigidbody>();
        //armStateMan.springJoint.connectedAnchor = Vector3.zero;
        //float distance = Vector3.Distance(armStateMan.transform.position, armStateMan.newGrappleHandle.transform.position);
        //armStateMan.springJoint.minDistance = 2.5f;
        //armStateMan.springJoint.maxDistance = 2.5f;
        //initialSpringForce = armStateMan.springJoint.spring;
        //armStateMan.lineRenderer.enabled = true;
        rb = armStateMan.hitObject.GetComponent<Rigidbody>();
        //initialMass = rb.mass;
        //rb.mass = 0.1f;
        //currentHookShotState = HookShotState.Pull;
       // rb.constraints = RigidbodyConstraints.None;
        //rb.constraints = RigidbodyConstraints.FreezePositionY;
        //rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        ///////////////////////////
        //armStateMan.player.currentState = PlayerControllerCinemachineLook2.State.Normal;
        armStateMan.playerSM.ChangeState(armStateMan.playerSM.pullingState);
        ///////////////////////////
    }

    public override void ExitState()
    {
        //rb.constraints = RigidbodyConstraints.FreezeRotation;

        //armStateMan.shootAction.performed -= Shoot;
        //armStateMan.shootAction.canceled -= NotShoot;
        //hand.transform.SetParent(armStateMan.transform);
        //rb.mass = initialMass;
        Object.Destroy(armStateMan.newGrappleHandle);
        //armStateMan.springJoint.spring = initialSpringForce;
        //armStateMan.springJoint.connectedAnchor = Vector3.zero;

        //armStateMan.springJoint.maxDistance = 0f;
        //armStateMan.springJoint.minDistance = 0f;
        //ReturnHand();
        armStateMan.pull = false;
        armStateMan.hitObject = null;
        //armStateMan.lineRenderer.enabled = false;
        armStateMan.playerSM.ChangeState(armStateMan.playerSM.moveLookState);

    }

    public override void UpdateState()
    {

        if (!armStateMan.shotArm)
        {
            armStateMan.SwitchState(armStateMan.idleState);

        }
        armStateMan.hitPoint = armStateMan.newGrappleHandle.transform.position;
        //armStateMan.springJoint.spring += Time.deltaTime;
        if (armStateMan.transform != null && armStateMan.hitObject != null)
            if (Vector3.Distance(armStateMan.transform.position, armStateMan.hitObject.transform.position) > armStateMan.distanceFromPlayerToStopPlaying + radius)
            {
                if (armStateMan.transform != null && armStateMan.hitObject != null)
                {
                    // if (CheckInFront())
                    rb.AddForceAtPosition(Vector3.Normalize(armStateMan.transform.position - armStateMan.hitObject.transform.position) * armStateMan.pullForce, armStateMan.hitPoint, ForceMode.Force);
                    //else
                    //armStateMan.SwitchState(armStateMan.idleState);

                }
            }
    }

    //public void Shoot(InputAction.CallbackContext context)
    //{
    //    isShooting = true;
    //}
    //private void NotShoot(InputAction.CallbackContext context)
    //{
    //    isShooting = false;

    //}

    public bool CheckInFront()
    {
        Vector3 directionToTarget = armStateMan.transform.position - rb.transform.position;
        float angle = Vector3.Angle(armStateMan.transform.forward, directionToTarget);
        float distance = directionToTarget.magnitude;

        if (Mathf.Abs(angle) < 90 /*&& distance < 100*/)
        {
            //isVisible = false;
            Debug.Log("not visible");
            return false;
        }
        else
        {
            //isVisible = true;
            Debug.Log("visible");
            return true;

        }
    }
}
