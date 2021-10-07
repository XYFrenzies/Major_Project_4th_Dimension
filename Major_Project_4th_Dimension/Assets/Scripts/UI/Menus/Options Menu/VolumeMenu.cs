using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeMenu : Singleton<VolumeMenu>
{
    [SerializeField]private Scrollbar m_masterScroll;
    [SerializeField]private Scrollbar m_musicScroll;
    [SerializeField]private Scrollbar m_soundScroll;
    [SerializeField] private GameObject m_volumeText;
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
        m_volumeText.SetActive(true);
    }
    public void SaveValues() 
    {
       GlobalVariables.Instance.soundVolume = m_soundScroll.value;
        GlobalVariables.Instance.musicVolume = m_musicScroll.value;
        GlobalVariables.Instance.masterVolume = m_masterScroll.value;
    }
}
