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
        aimAction.performed += _ => StartAimZoom();
        aimAction.canceled += _ => StopAimZoom();

    }

    public void OnDisable()
    {
        aimAction.performed -= _ => StartAimZoom();
        aimAction.canceled -= _ => StopAimZoom();


    }

    public void StartAimZoom()
    {
        if (!aimCamera.activeInHierarchy)
        {
            mainCamera.SetActive(false);
            aimCamera.SetActive(true);
        }
    }

    public void StopAimZoom()
    {
        if (!mainCamera.activeInHierarchy)
        {
            mainCamera.SetActive(true);
            aimCamera.SetActive(false);
        }
    }
}
