using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PowerStatus : Singleton<PowerStatus>
{
    [SerializeField] private GameEvent m_powerOn;
    [SerializeField] private GameEvent m_powerOff;
    public bool powerIsOn = false;
    AudioSource source;
    //public SimpleAudioEvent powerOn;
    //public SimpleAudioEvent powerOff;

    private void Awake()
    {
        source = GetComponent<AudioSource>();
        if (powerIsOn)
            m_powerOn.Raise();
        else
            m_powerOff.Raise();
    }
    //Can change to collision depending on the collider to the power.
    public void TurnPowerOnOrOff()
    {
        powerIsOn = !powerIsOn;

        switch (powerIsOn)
        {
            case true:
                m_powerOn.Raise();
                if (source != null)
                    SoundPlayer.Instance.PlaySoundEffect("PowerOn", source);
                    //Debug.Log("Power on sound");
                Debug.Log("Power is on");
                break;
            case false:
                m_powerOff.Raise();
                if (source != null)
                    SoundPlayer.Instance.PlaySoundEffect("PowerOff", source);
                    //Debug.Log("Power off sound");

                Debug.Log("Power is off");
                break;
        }
    }
}
