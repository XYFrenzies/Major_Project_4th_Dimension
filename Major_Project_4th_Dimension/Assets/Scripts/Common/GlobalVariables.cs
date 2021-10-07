using UnityEngine;
/// <summary>
/// These are global values that are saved between scenes.
/// It will also be saving volumes outside of the game so that it will be set for each new start.
/// </summary>
public class GlobalVariables : Singleton<GlobalVariables>
{
    //[HideInInspector] public bool isFading = false;
    [HideInInspector] public float masterVolume = 0.0f;
    [HideInInspector] public float soundVolume = 0.0f;
    [HideInInspector] public float musicVolume = 0.0f;
    [HideInInspector] public float verticalSensitivity = 0.2f;
    [HideInInspector] public float horizontalSensitivity = 0.2f;
    [HideInInspector] public int fpsIsOn = 1;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Master Volume") && PlayerPrefs.HasKey("Sound Volume") && PlayerPrefs.HasKey("Sound Effect Volume"))
        {
            masterVolume = PlayerPrefs.GetFloat("Master Volume");
            soundVolume = PlayerPrefs.GetFloat("Sound Volume");
            musicVolume = PlayerPrefs.GetFloat("Sound Effect Volume");
        }
        else
        {
            Debug.Log("New volume defaults need to be entered.");
            PlayerPrefs.SetFloat("Master Volume", masterVolume);
            PlayerPrefs.SetFloat("Sound Volume", soundVolume);
            PlayerPrefs.SetFloat("Sound Effect Volume", musicVolume);
        }
        if (PlayerPrefs.HasKey("Vertical Sensitivity") && PlayerPrefs.HasKey("Horizontal Sensitivity"))
        {
            verticalSensitivity = PlayerPrefs.GetFloat("Vertical Sensitivity");
            horizontalSensitivity = PlayerPrefs.GetFloat("Horizontal Sensitivity");
        }
        else 
        {
            Debug.Log("New sensitivity defaults need to be entered.");
            PlayerPrefs.SetFloat("Vertical Sensitivity", verticalSensitivity);
            PlayerPrefs.SetFloat("Horizontal Sensitivity", horizontalSensitivity);
        }
        if (PlayerPrefs.HasKey("FPS Display"))
            fpsIsOn = PlayerPrefs.GetInt("FPS Display");
        else
            PlayerPrefs.SetInt("FPS Display", fpsIsOn);
        //DontDestroyOnLoad(gameObject);
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
    }
}