using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuInput : MonoBehaviour
{
     //Sensitivety X
     //Sensitivety Y
    public void ControllerInput() 
    {
        CheckInput.Instance.SetController();
    }
    public void PCInput() 
    {
        CheckInput.Instance.SetMouse();
    }
}
