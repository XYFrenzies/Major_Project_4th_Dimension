using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
/// <summary>
/// These are global values that are saved between scenes.
/// It will also be saving volumes outside of the game so that it will be set for each new start.
/// </summary>
public class GlobalVariables : Singleton<GlobalVariables>
{
    [HideInInspector] public float masterVolume = 0.2f;
    [HideInInspector] public float soundVolume = 0.2f;
    [HideInInspector] public float musicVolume = 0.2f;
    [HideInInspector] public float verticalSensitivity = 0.2f;
    [HideInInspector] public float horizontalSensitivity = 0.2f;
    [HideInInspector] public float verticalSensitivityNonZoom = 0.1f;
    [HideInInspector] public float horizontalSensitivityNonZoom = 0.1f;
    [HideInInspector] public int fpsIsOn = 1;
    [HideInInspector] public int gamepadIsOn = 1;
    [HideInInspector] public int mouseIsOn = 0;
    [HideInInspector] public int m_qualityDisplayInt = 2;
    [HideInInspector] public int m_resolutionInt = 0;
    [HideInInspector] public int m_isFullscreen = 0;
    private AudioMixer m_audioMixer;
    private Toggle m_interfaceOn;
    private string[] allValues = { "Master Volume", "Sound Volume", "Sound Effect Volume", "Vertical SensitivityZoom",
        "Horizontal SensitivityZoom", "Vertical SensitivityNonZoom", "Horizontal SensitivityNonZoom", "FPS Display", "MouseIsOn" , "GamePadIsOn", "FullScreen", "Resolution", "Quality"};
    [HideInInspector]public string m_preSceneNames;
    private void Awake()
    {
        m_preSceneNames = SceneManager.GetActiveScene().name;
        switch (CheckIfPrefsExist(allValues))
        {
            case true:
                masterVolume = PlayerPrefs.GetFloat("Master Volume");
                soundVolume = PlayerPrefs.GetFloat("Sound Volume");
                musicVolume = PlayerPrefs.GetFloat("Sound Effect Volume");
                verticalSensitivity = PlayerPrefs.GetFloat("Vertical SensitivityZoom");
                horizontalSensitivity = PlayerPrefs.GetFloat("Horizontal SensitivityZoom");
                verticalSensitivityNonZoom = PlayerPrefs.GetFloat("Vertical SensitivityNonZoom");
                horizontalSensitivityNonZoom = PlayerPrefs.GetFloat("Horizontal SensitivityNonZoom");
                fpsIsOn = PlayerPrefs.GetInt("FPS Display");
                gamepadIsOn = PlayerPrefs.GetInt("GamePadIsOn");
                mouseIsOn = PlayerPrefs.GetInt("MouseIsOn");
                m_qualityDisplayInt = PlayerPrefs.GetInt("Quality");
                m_resolutionInt = PlayerPrefs.GetInt("Resolution");
                m_isFullscreen = PlayerPrefs.GetInt("FullScreen");
                break;
            case false:
                PlayerPrefs.SetFloat("Master Volume", masterVolume);
                PlayerPrefs.SetFloat("Sound Volume", soundVolume);
                PlayerPrefs.SetFloat("Sound Effect Volume", musicVolume);
                PlayerPrefs.SetFloat("Vertical SensitivityZoom", verticalSensitivity);
                PlayerPrefs.SetFloat("Horizontal SensitivityZoom", horizontalSensitivity);
                PlayerPrefs.SetFloat("Vertical SensitivityNonZoom", verticalSensitivityNonZoom);
                PlayerPrefs.SetFloat("Horizontal SensitivityNonZoom", horizontalSensitivityNonZoom);
                PlayerPrefs.SetInt("FPS Display", fpsIsOn);
                PlayerPrefs.SetInt("GamePadIsOn", gamepadIsOn);
                PlayerPrefs.SetInt("MouseIsOn", mouseIsOn);
                PlayerPrefs.SetInt("Quality", m_resolutionInt);
                PlayerPrefs.SetInt("Resolution",m_qualityDisplayInt);
                PlayerPrefs.SetInt("FullScreen", m_isFullscreen);
                break;
        }
        if(InterfaceMenu.Instance != null)
            m_interfaceOn = InterfaceMenu.Instance.fpsCounter;
        if(VolumeMenu.Instance != null)
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
        if (VolumeMenu.Instance != null)
        {
            m_audioMixer.SetFloat("MasterVol", Mathf.Log10(masterVolume) * 30.0f);
            m_audioMixer.SetFloat("MusicVol", Mathf.Log10(soundVolume) * 30.0f);
            m_audioMixer.SetFloat("SFXVol", Mathf.Log10(musicVolume) * 30.0f);
        }
        if (InterfaceMenu.Instance != null)
            m_interfaceOn.isOn = GetFPSIsOn();
    }
    public void SaveScene(string name) 
    {
        m_preSceneNames = name;
    }
    public string GetPreScene()
    {
        return m_preSceneNames;
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

    public bool GetMouseActive()
    {
        if (mouseIsOn == 0)
            return true;
        else if (mouseIsOn == 1)
            return false;
        return false;
    }
    public bool GetGamepadActive()
    {
        if (gamepadIsOn == 0)
            return true;
        else if (gamepadIsOn == 1)
            return false;
        return false;
    }

    public bool GetFPSIsOn()
    {
        if (fpsIsOn == 0)
            return true;
        else if (fpsIsOn == 1)
            return false;
        return false;
    }
    public bool GetScreenIsOn()
    {
        if (m_isFullscreen == 0)
            return true;
        else if (m_isFullscreen == 1)
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
    public void SaveFullScreenIsOn(bool boolValue)
    {
        if (boolValue)
            m_isFullscreen = 0;
        else
            m_isFullscreen = 1;
    }

    public void SaveMouseIsOn(bool boolValue)
    {
        if (boolValue)
            mouseIsOn = 0;
        else
            gamepadIsOn = 1;
    }
    public void SaveGamepadIsOn(bool boolValue)
    {
        if (boolValue)
            gamepadIsOn = 0;
        else
            m_isFullscreen = 1;
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
        PlayerPrefs.SetFloat("Vertical SensitivityZoom", verticalSensitivity);
        PlayerPrefs.SetFloat("Horizontal SensitivityZoom", horizontalSensitivity);
        PlayerPrefs.SetFloat("Vertical SensitivityNonZoom", verticalSensitivityNonZoom);
        PlayerPrefs.SetFloat("Horizontal SensitivityNonZoom", horizontalSensitivityNonZoom);
        PlayerPrefs.SetInt("FPS Display", fpsIsOn);
        PlayerPrefs.SetInt("GamePadIsOn", gamepadIsOn);
        PlayerPrefs.SetInt("MouseIsOn", mouseIsOn);
        PlayerPrefs.SetInt("Quality", m_resolutionInt);
        PlayerPrefs.SetInt("Resolution", m_qualityDisplayInt);
        PlayerPrefs.SetInt("FullScreen", m_isFullscreen);
    }
}