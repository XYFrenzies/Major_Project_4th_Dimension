using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InterfaceMenu : MonoBehaviour
{
    [SerializeField] private Toggle fpsCounter;
    private bool valueFPSCounter = false;
    private bool isMouseActive = true;
    // Start is called before the first frame update
    void Start()
    {
        valueFPSCounter = GlobalVariables.Instance.GetFPSIsOn();
        fpsCounter.isOn = valueFPSCounter;
    }
    public void SaveValues() 
    {
        GlobalVariables.Instance.SaveFPSIsOn(valueFPSCounter);
    }
}
