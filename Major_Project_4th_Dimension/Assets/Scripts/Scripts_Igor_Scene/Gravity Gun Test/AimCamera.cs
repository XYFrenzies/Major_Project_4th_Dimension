using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class AimCamera : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction aimAction;

    public GameObject mainCamera;
    public GameObject aimCamera;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        aimAction = playerInput.actions["Aim"];
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
