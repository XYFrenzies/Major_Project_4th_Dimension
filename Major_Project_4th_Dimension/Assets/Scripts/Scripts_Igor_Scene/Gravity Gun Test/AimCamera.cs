using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;

public class AimCamera : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction aimAction;

    public GameObject mainCamera;
    public GameObject aimCamera;
    private CinemachinePOV playerZoom;
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        aimAction = playerInput.actions["Aim"];
        if (GameObject.FindGameObjectWithTag("PlayerFlag").transform.Find("3rdPersonCinemachineAim").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>() != null)
            playerZoom = GameObject.FindGameObjectWithTag("PlayerFlag").transform.Find("3rdPersonCinemachineAim").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>();
    }

    public void OnEnable()
    {
        aimAction.performed += StartAimZoom;
        aimAction.canceled += StopAimZoom;

    }

    public void OnDisable()
    {
        aimAction.performed -= StartAimZoom;
        aimAction.canceled -= StopAimZoom;


    }

    public void StartAimZoom(InputAction.CallbackContext context)
    {
        if (!aimCamera.activeInHierarchy)
        {
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);
            playerZoom.m_HorizontalAxis.m_MaxSpeed = GlobalVariables.Instance.horizontalSensitivity;
            playerZoom.m_VerticalAxis.m_MaxSpeed = GlobalVariables.Instance.verticalSensitivity;
        }
    }

    public void StopAimZoom(InputAction.CallbackContext context)
    {
        if (!mainCamera.activeInHierarchy)
        {
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
        }
    }
}
