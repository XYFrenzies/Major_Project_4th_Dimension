using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class MenuInput : MonoBehaviour
{
    private CinemachineVirtualCamera player;
    private float xAxis;
    private float yAxis;
    //Sensitivety X
    //Sensitivety Y
    private void Awake()
    {
        if(GameObject.FindGameObjectWithTag("PlayerFlag").transform.Find("3rdPersonCinemachine").GetComponent<CinemachineVirtualCamera>() != null)
            player = GameObject.FindGameObjectWithTag("PlayerFlag").transform.Find("3rdPersonCinemachine").GetComponent<CinemachineVirtualCamera>();
        xAxis = GlobalVariables.Instance.verticalSensitivity;
        yAxis = GlobalVariables.Instance.horizontalSensitivity;
        //player.;
    }
    public void ControllerInput() 
    {
        CheckInput.Instance.SetController();
    }
    public void PCInput() 
    {
        CheckInput.Instance.SetMouse();
    }
}
