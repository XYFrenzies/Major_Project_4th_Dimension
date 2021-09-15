using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerStatus : MonoBehaviour
{
    [SerializeField] private GameEvent m_powerOn;
    [SerializeField] private GameEvent m_powerOff;
    [SerializeField] private bool m_powerIsOn = false;

    //Can change to collision depending on the collider to the power.
    private void OnTriggerEnter(Collider other)
    {
        switch (m_powerIsOn)
        {
            case true:
                m_powerOff.Raise();
                m_powerIsOn = false;
                break;
            case false:
                m_powerOn.Raise();
                m_powerIsOn = true;
                break;
        }
    }
}
