using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using TMPro;
public class RenderingMenu : Singleton<RenderingMenu>
{
    public TMP_Dropdown m_resolutionDropDown;
    public TMP_Dropdown m_quality;
    private Resolution[] m_resolutionsMultiple;
    [SerializeField]private HDRenderPipelineAsset[] m_qualityChanger;
    private int m_qualityLevel;
    // Start is called before the first frame update
    private void Start()
    {
        int amountOFQuality = m_qualityChanger.Length;
        m_resolutionsMultiple = Screen.resolutions;
        m_resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < m_resolutionsMultiple.Length; i++)
        {
            string option = m_resolutionsMultiple[i].width + " x " + m_resolutionsMultiple[i].height;
            options.Add(option);
        }
        m_resolutionDropDown.AddOptions(options);
        m_quality.ClearOptions();
        List<string> qualityOptions = new List<string>();
        int indexOfResolution = 0;
        for (int i = 0; i < amountOFQuality; i++)
        {
            string option = m_qualityChanger[i].name;
            qualityOptions.Add(option);

            if (m_resolutionsMultiple[i].width == Screen.currentResolution.width &&
                m_resolutionsMultiple[i].height == Screen.currentResolution.height)
            {
                indexOfResolution = i;
            }
        }
        m_quality.AddOptions(qualityOptions);
        m_resolutionDropDown.value = indexOfResolution;
        m_resolutionDropDown.RefreshShownValue();
    }
    public void SaveValues()
    {
        GlobalVariables.Instance.m_qualityDisplayInt = m_qualityLevel;
    }
    public void SetResolution(int resolutionIndex) 
    {
        Resolution resolution = m_resolutionsMultiple[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetQuality(int qualityIndex) 
    {
        m_qualityLevel = qualityIndex;
        QualitySettings.SetQualityLevel(qualityIndex);
    }
    public void SetFullScreen(bool isFullScreen) 
    {
        Screen.fullScreen = isFullScreen;
    }
}
