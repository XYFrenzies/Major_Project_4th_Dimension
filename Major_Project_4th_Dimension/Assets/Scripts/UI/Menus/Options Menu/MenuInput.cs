using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class MenuInput : MonoBehaviour
{
    [SerializeField]private CinemachinePOV playerNonZoom;
    [SerializeField]private CinemachinePOV playerZoom;
    private float xAxisNonZoom;
    private float yAxisNonZoom;
    private float xAxisZoomed;
    private float yAxisZoomed;
    private bool m_mouseActive;
    //Sensitivety X
    //Sensitivety Y
    private void Awake()
    {
        xAxisNonZoom = GlobalVariables.Instance.verticalSensitivityNonZoom;
        yAxisNonZoom = GlobalVariables.Instance.horizontalSensitivityNonZoom;
        xAxisZoomed = GlobalVariables.Instance.verticalSensitivity;
        yAxisZoomed = GlobalVariables.Instance.horizontalSensitivity;
        if (GlobalVariables.Instance.GetMouseActive())
        {
            CheckInput.Instance.SetMouse();
            m_mouseActive = true;
        }
        else if (GlobalVariables.Instance.GetGamepadActive())
        {
            CheckInput.Instance.SetController();
            m_mouseActive = false;
        }


        //player.;
    }
    public void SaveValues()
    {
        playerNonZoom.m_HorizontalAxis.m_MaxSpeed = yAxisNonZoom;
        playerNonZoom.m_VerticalAxis.m_MaxSpeed = xAxisNonZoom;
        playerZoom.m_HorizontalAxis.m_MaxSpeed = yAxisZoomed;
        playerZoom.m_VerticalAxis.m_MaxSpeed = xAxisZoomed;
        GlobalVariables.Instance.verticalSensitivityNonZoom = xAxisNonZoom;
        GlobalVariables.Instance.horizontalSensitivityNonZoom = yAxisNonZoom;
        GlobalVariables.Instance.verticalSensitivity = xAxisZoomed;
        GlobalVariables.Instance.horizontalSensitivity = yAxisZoomed;
        GlobalVariables.Instance.SaveMouseIsOn(m_mouseActive);
        GlobalVariables.Instance.SaveGamepadIsOn(!m_mouseActive);
    }
    public void ControllerInput() 
    {
        CheckInput.Instance.SetController();
        m_mouseActive = false;
    }
    public void PCInput() 
    {
        CheckInput.Instance.SetMouse();
        m_mouseActive = true;
    }
    public void NonZoomedVerticalInput(float value) 
    {
        xAxisNonZoom = value;
    }
    public void NonZoomedHorizontalInput(float value)
    {
        yAxisNonZoom = value;
    }
    public void ZoomedVerticalInput(float value)
    {
        xAxisZoomed = value;
    }
    public void ZoomedHorizontalInput(float value)
    {
        yAxisZoomed = value;
    }
}
