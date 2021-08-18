using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class SwitchCam : MonoBehaviour
{
    public Animator anim;
    public PlayerInput playerInput;
    private CinemachineVirtualCamera virtualCamera;
    private InputAction aimAction;
    private int priorityBoostAMount = 10;
    // Start is called before the first frame update
    private void Awake()
    {
        virtualCamera = GetComponent<CinemachineVirtualCamera>();
        aimAction = playerInput.actions["Aim"];
    }

    private void OnEnable()
    {
        aimAction.performed += _ => StartAim();
        aimAction.canceled += _ => StopAim();
    }

    private void OnDisable()
    {
        aimAction.performed -= _ => StartAim();
        aimAction.canceled -= _ => StopAim();
    }

    public void StartAim()
    {
        anim.SetBool("IsAiming", true);
        virtualCamera.Priority += priorityBoostAMount;
    }

    public void StopAim()
    {
        anim.SetBool("IsAiming", false);

        virtualCamera.Priority -= priorityBoostAMount;

    }
}
