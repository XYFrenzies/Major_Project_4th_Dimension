using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PowerStatus : Singleton<PowerStatus>
{
    [SerializeField] private GameEvent m_powerOn;
    [SerializeField] private GameEvent m_powerOff;
    [SerializeField] private List<Light> lightsInScene;
    public bool powerIsOn = false;
    AudioSource source;
    public Animator[] animators;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        if (powerIsOn)
        {
            m_powerOn.Raise();
            foreach (var item in lightsInScene)
            {
                item.color = Color.green;
            }
            foreach (var item in animators)
            {
                item.SetBool("IsPowerOn", true);
            }
        }
        else
        {
            m_powerOff.Raise();
            foreach (var item in lightsInScene)
            {
                item.color = Color.red;
            }
            foreach (var item in animators)
            {
                item.SetBool("IsPowerOn", false);
            }
        }


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
                foreach (var item in lightsInScene)
                {
                    item.color = Color.green;
                }
                foreach (var item in animators)
                {
                    item.SetBool("IsPowerOn", true);
                }
                Debug.Log("Power is on");
                break;
            case false:
                m_powerOff.Raise();
                if (source != null)
                    SoundPlayer.Instance.PlaySoundEffect("PowerOff", source);
                //Debug.Log("Power off sound");
                foreach (var item in lightsInScene)
                {
                    item.color = Color.red;
                }
                foreach (var item in animators)
                {
                    item.SetBool("IsPowerOn", false);
                }
                Debug.Log("Power is off");

                break;
        }
    }
}
