using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStatus : Singleton<PowerStatus>
{
    [SerializeField] private GameEvent m_powerOn;
    [SerializeField] private GameEvent m_powerOff;
    public bool powerIsOn = false;

    //Can change to collision depending on the collider to the power.
    public void TurnPowerOnOrOff()
    {
        powerIsOn = !powerIsOn;

        switch (powerIsOn)
        {
            case true:
                m_powerOn.Raise();
                //m_powerIsOn = false;
                Debug.Log("Power is on");
                break;
            case false:
                m_powerOff.Raise();
                //m_powerIsOn = true;
                Debug.Log("Power is off");
                break;
        }
    }
}
