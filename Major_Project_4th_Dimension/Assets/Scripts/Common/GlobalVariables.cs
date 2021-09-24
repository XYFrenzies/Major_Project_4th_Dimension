using System.Collections;
using System.Collections.Generic;
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
    [HideInInspector] public float soundEffectVolume = 0.0f;
    private void Awake()
    {
        if (PlayerPrefs.HasKey("Master Volume") && PlayerPrefs.HasKey("Sound Volume") && PlayerPrefs.HasKey("Sound Effect Volume"))
        {
            masterVolume = PlayerPrefs.GetFloat("Master Volume");
            soundVolume = PlayerPrefs.GetFloat("Sound Volume");
            soundEffectVolume = PlayerPrefs.GetFloat("Sound Effect Volume");
        }
        else
        {
            Debug.Log("New volume defaults need to be entered.");
            PlayerPrefs.SetFloat("Master Volume", masterVolume);
            PlayerPrefs.SetFloat("Sound Volume", soundVolume);
            PlayerPrefs.SetFloat("Sound Effect Volume", soundEffectVolume);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void SaveVolumes(float a_masterVolume, float a_soundVolume, float a_soundEffectVolume) 
    {
        masterVolume = a_masterVolume;
        soundVolume = a_soundVolume;
        soundEffectVolume = a_soundEffectVolume;
        PlayerPrefs.SetFloat("Master Volume", masterVolume);
        PlayerPrefs.SetFloat("Sound Volume", soundVolume);
        PlayerPrefs.SetFloat("Sound Effect Volume", soundEffectVolume);
    }
    public void ResetAllVolumes() 
    {
        masterVolume = 0.0f;
        soundVolume = 0.0f;
        soundEffectVolume = 0.0f;
    }
    //public bool CheckIsFade()
    //{
    //    switch (isFading)
    //    {
    //        case true:
    //            return true;
    //        case false:
    //            return false;
    //    }
    //}

}
