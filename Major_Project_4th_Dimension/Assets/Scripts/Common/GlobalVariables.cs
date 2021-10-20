using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
/// <summary>
/// These are global values that are saved between scenes.
/// It will also be saving volumes outside of the game so that it will be set for each new start.
/// </summary>
public class GlobalVariables : Singleton<GlobalVariables>
{
    [HideInInspector] public float masterVolume = 0.0f;
    [HideInInspector] public float soundVolume = 0.0f;
    [HideInInspector] public float musicVolume = 0.0f;
    [HideInInspector] public float verticalSensitivity = 0.2f;
    [HideInInspector] public float horizontalSensitivity = 0.2f;
    [HideInInspector] public int fpsIsOn = 1;
    [HideInInspector] public int gamepadIsOn = 1;
    [HideInInspector] public int mouseIsOn = 0;
    [HideInInspector] public int m_qualityDisplayInt = 0;
    [HideInInspector] public int m_resolutionInt = 0;
    private AudioMixer m_audioMixer;
    private Toggle m_interfaceOn;
    private string[] allValues = { "Master Volume", "Sound Volume", "Sound Effect Volume", "Vertical Sensitivity",
        "Horizontal Sensitivity", "FPS Display", "MouseIsOn" , "GamePadIsOn"};
    private void Awake()
    {
        switch (CheckIfPrefsExist(allValues))
        {
            case true:
                masterVolume = PlayerPrefs.GetFloat("Master Volume");
                soundVolume = PlayerPrefs.GetFloat("Sound Volume");
                musicVolume = PlayerPrefs.GetFloat("Sound Effect Volume");
                verticalSensitivity = PlayerPrefs.GetFloat("Vertical Sensitivity");
                horizontalSensitivity = PlayerPrefs.GetFloat("Horizontal Sensitivity");
                fpsIsOn = PlayerPrefs.GetInt("FPS Display");
                gamepadIsOn = PlayerPrefs.GetInt("GamePadIsOn");
                mouseIsOn = PlayerPrefs.GetInt("MouseIsOn");
                break;
            case false:
                PlayerPrefs.SetFloat("Master Volume", masterVolume);
                PlayerPrefs.SetFloat("Sound Volume", soundVolume);
                PlayerPrefs.SetFloat("Sound Effect Volume", musicVolume);
                PlayerPrefs.SetFloat("Vertical Sensitivity", verticalSensitivity);
                PlayerPrefs.SetFloat("Horizontal Sensitivity", horizontalSensitivity);
                PlayerPrefs.SetInt("FPS Display", fpsIsOn);
                PlayerPrefs.SetInt("GamePadIsOn", gamepadIsOn);
                PlayerPrefs.SetInt("MouseIsOn", mouseIsOn);
                break;
        }
        m_interfaceOn = InterfaceMenu.Instance.fpsCounter;
        m_audioMixer = VolumeMenu.Instance.m_audioMixer;
        DontDestroyOnLoad(gameObject);

    }
    private bool CheckIfPrefsExist(string[] variables)
    {
        for (int i = 0; i < variables.Length; i++)
        {
            if (!PlayerPrefs.HasKey(variables[i]))
                return false;
        }
        return true;
    }
    private void Start()
    {
        m_audioMixer.SetFloat("MasterVol", Mathf.Log10(masterVolume) * 30.0f);
        m_audioMixer.SetFloat("MusicVol", Mathf.Log10(soundVolume) * 30.0f);
        m_audioMixer.SetFloat("SFXVol", Mathf.Log10(musicVolume) * 30.0f);

        m_interfaceOn.isOn = GetFPSIsOn();
    }
    public void SaveVolumes(float a_masterVolume, float a_soundVolume, float a_soundEffectVolume)
    {
        masterVolume = a_masterVolume;
        soundVolume = a_soundVolume;
        musicVolume = a_soundEffectVolume;
        PlayerPrefs.SetFloat("Master Volume", masterVolume);
        PlayerPrefs.SetFloat("Sound Volume", soundVolume);
        PlayerPrefs.SetFloat("Sound Effect Volume", musicVolume);
    }
    public void ResetAllVolumes()
    {
        masterVolume = 0.0f;
        soundVolume = 0.0f;
        musicVolume = 0.0f;
    }

    public bool GetFPSIsOn()
    {
        if (fpsIsOn == 0)
            return true;
        else if (fpsIsOn == 1)
            return false;
        return false;
    }
    public void SaveFPSIsOn(bool boolValue)
    {
        if (boolValue)
            fpsIsOn = 0;
        else
            fpsIsOn = 1;
    }
    public void SaveSensitivity(float a_verticalSensitivity, float a_horizontalSensitivity)
    {
        verticalSensitivity = a_verticalSensitivity;
        horizontalSensitivity = a_horizontalSensitivity;
    }
    private void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat("Master Volume", masterVolume);
        PlayerPrefs.SetFloat("Sound Volume", soundVolume);
        PlayerPrefs.SetFloat("Sound Effect Volume", musicVolume);
        PlayerPrefs.SetFloat("Vertical Sensitivity", verticalSensitivity);
        PlayerPrefs.SetFloat("Horizontal Sensitivity", horizontalSensitivity);
        PlayerPrefs.SetInt("FPS Display", fpsIsOn);
        PlayerPrefs.SetInt("GamePadIsOn", gamepadIsOn);
        PlayerPrefs.SetInt("MouseIsOn", mouseIsOn);
    }
}