using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
public class VolumeMenu : Singleton<VolumeMenu>
{
    [SerializeField] protected AudioMixer m_audioMixer;
    [SerializeField] protected Slider m_masterScroll;
    [SerializeField] protected Slider m_musicScroll;
    [SerializeField] protected Slider m_soundScroll;
    [SerializeField] protected float m_multipler = 30.0f;
    private float m_masterVolume = 0.0f;
    private float m_musicVolume = 0.0f;
    private float m_soundVolume = 0.0f;
    // Start is called before the first frame update
    private void Start()
    {
        m_soundVolume = GlobalVariables.Instance.soundVolume;
        m_musicVolume = GlobalVariables.Instance.musicVolume;
        m_masterVolume = GlobalVariables.Instance.masterVolume;
        m_masterScroll.value = m_masterVolume;
        m_musicScroll.value = m_musicVolume;
        m_soundScroll.value = m_soundVolume;
        m_audioMixer.SetFloat("MasterVol", Mathf.Log10(m_masterVolume) * m_multipler);
    }
    public void SetMasterVolume(float masterVolLvl)
    {
        m_audioMixer.SetFloat("MasterVol", Mathf.Log10(masterVolLvl) * m_multipler);
    }
    public void SetMusicVolume(float musicVolLvl) { }
    public void SetSoundVolume(float soundVolLvl) { }
    public void SaveValues()
    {
        GlobalVariables.Instance.soundVolume = m_soundScroll.value;
        GlobalVariables.Instance.musicVolume = m_musicScroll.value;
        GlobalVariables.Instance.masterVolume = m_masterScroll.value;
    }
}
