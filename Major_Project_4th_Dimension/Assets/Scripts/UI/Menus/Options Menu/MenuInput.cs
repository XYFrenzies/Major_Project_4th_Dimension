using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MenuInput : MonoBehaviour
{
    [SerializeField] private Slider nonZoomXSlider;
    [SerializeField] private Slider zoomXSlider;
    [SerializeField] private Slider nonZoomYSlider;
    [SerializeField] private Slider zoomYSlider;
    [SerializeField] private List<GameObject> textObjs;
    private CinemachinePOV playerZoom;
    private CinemachinePOV playerNonZoom;

    private float xAxisNonZoom;
    private float yAxisNonZoom;
    private float xAxisZoomed;
    private float yAxisZoomed;
    private bool m_mouseActive;
    //Sensitivety X
    //Sensitivety Y
    private void Awake()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu")
        {
            nonZoomXSlider.gameObject.SetActive(false);
            zoomXSlider.gameObject.SetActive(false);
            nonZoomYSlider.gameObject.SetActive(false);
            zoomYSlider.gameObject.SetActive(false);
            foreach (var item in textObjs)
            {
                item.SetActive(false);
            }
        }
        else
        {
            nonZoomXSlider.gameObject.SetActive(true);
            zoomXSlider.gameObject.SetActive(true);
            nonZoomYSlider.gameObject.SetActive(true);
            zoomYSlider.gameObject.SetActive(true);
            foreach (var item in textObjs)
            {
                item.SetActive(true);
            }
            if (GameObject.FindGameObjectWithTag("PlayerFlag").transform.Find("3rdPersonCinemachine").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>() != null)
                playerNonZoom = GameObject.FindGameObjectWithTag("PlayerFlag").transform.Find("3rdPersonCinemachine").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>();
            if (GameObject.FindGameObjectWithTag("PlayerFlag").transform.Find("3rdPersonCinemachineAim").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>() != null)
                playerZoom = GameObject.FindGameObjectWithTag("PlayerFlag").transform.Find("3rdPersonCinemachineAim").GetComponent<CinemachineVirtualCamera>().GetCinemachineComponent<CinemachinePOV>();
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
            if (playerNonZoom != null)
            {
                playerNonZoom.m_HorizontalAxis.m_MaxSpeed = yAxisNonZoom;
                playerNonZoom.m_VerticalAxis.m_MaxSpeed = xAxisNonZoom;
            }
            if (playerZoom != null)
            {
                playerZoom.m_HorizontalAxis.m_MaxSpeed = yAxisZoomed;
                playerZoom.m_VerticalAxis.m_MaxSpeed = xAxisZoomed;
            }
            nonZoomXSlider.onValueChanged.AddListener(NonZoomedVerticalInput);
            zoomXSlider.onValueChanged.AddListener(ZoomedVerticalInput);
            nonZoomYSlider.onValueChanged.AddListener(NonZoomedHorizontalInput);
            zoomYSlider.onValueChanged.AddListener(ZoomedHorizontalInput);
            nonZoomXSlider.value = xAxisNonZoom;
            zoomXSlider.value = xAxisZoomed;
            nonZoomYSlider.value = yAxisNonZoom;
            zoomYSlider.value = yAxisZoomed;
        }

    }
    public void SaveValues()
    {
        playerNonZoom.m_HorizontalAxis.m_MaxSpeed = yAxisNonZoom;
        playerNonZoom.m_VerticalAxis.m_MaxSpeed = xAxisNonZoom;
        // playerZoom.m_HorizontalAxis.m_MaxSpeed = yAxisZoomed;
        // playerZoom.m_VerticalAxis.m_MaxSpeed = xAxisZoomed;
        GlobalVariables.Instance.verticalSensitivityNonZoom = xAxisNonZoom;
        GlobalVariables.Instance.horizontalSensitivityNonZoom = yAxisNonZoom;
        //GlobalVariables.Instance.verticalSensitivity = xAxisZoomed;
        //GlobalVariables.Instance.horizontalSensitivity = yAxisZoomed;
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
