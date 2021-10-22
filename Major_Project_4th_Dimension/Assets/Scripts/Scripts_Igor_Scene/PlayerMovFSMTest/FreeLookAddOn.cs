using System.Collections;
using System.Collections.Generic;
using System;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;


public class FreeLookAddOn : MonoBehaviour
{
    //[Range(0f, 10f)] public float LookSpeed = 1f;
    //public bool InvertY = false;
    ////private CinemachineFreeLook _freeLookComponent;
    //private CinemachineVirtualCamera vcam;
    private CinemachinePOV cPOV;
    //public PlayerInput input;
    //private InputAction lookAction;
    //public InputDevice device;
    public Slider slider;

    public void Awake()
    {
        //    lookAction = input.actions["Look"];

        //    //_freeLookComponent = GetComponent<CinemachineFreeLook>();
        //vcam = GetComponent<CinemachineVirtualCamera>();
        cPOV = GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>();
        //    //cPOV.m_HorizontalAxis.m_MaxSpeed = slider.value;

    }

    private void Update()
    {
        //Look();
        ChangeValue();
    }

    //// Update the look movement each time the event is trigger
    //public void Look()
    //{
    //    //cPOV.m_HorizontalAxis.m_MaxSpeed = slider.value;
    //    //Normalize the vector to have an uniform vector in whichever form it came from (I.E Gamepad, mouse, etc)
    //    //Vector2 lookMovement = lookAction.ReadValue<Vector2>().normalized;
    //    //lookMovement.y = InvertY ? -lookMovement.y : lookMovement.y;

    //    // This is because X axis is only contains between -180 and 180 instead of 0 and 1 like the Y axis
    //    //lookMovement.x = lookMovement.x * 180f;
    //    //if(Gamepad.current.IsActuated())
    //    // cPOV.m_HorizontalAxis.m_MaxSpeed = 10f;

    //    //else if(Mouse.current.IsPressed())
    //    //    cPOV.m_HorizontalAxis.m_MaxSpeed = 1f;

    //    //cPOV.m_HorizontalAxis.Value += lookMovement.x * LookSpeed * Time.deltaTime;
    //    //cPOV.m_VerticalAxis.Value += lookMovement.y * LookSpeed * Time.deltaTime;
    //    //cPOV.m_HorizontalAxis.m_MaxSpeed = 0f;
    //    //Ajust axis values using look speed and Time.deltaTime so the look doesn't go faster if there is more FPS
    //    // _freeLookComponent.m_XAxis.Value += lookMovement.x * LookSpeed * Time.deltaTime;
    //    // _freeLookComponent.m_YAxis.Value += lookMovement.y * LookSpeed * Time.deltaTime;


    //}

    public void ChangeValue()
    {

        cPOV.m_HorizontalAxis.m_MaxSpeed = slider.value;
        Debug.Log(slider.value);
    }
}