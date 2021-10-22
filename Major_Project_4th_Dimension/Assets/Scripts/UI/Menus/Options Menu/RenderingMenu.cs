using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using TMPro;
public class RenderingMenu : Singleton<RenderingMenu>
{
    public TMP_Dropdown m_resolutionDropDown;
    public TMP_Dropdown m_quality;
    private Resolution[] m_resolutionsMultiple;
    [SerializeField] private HDRenderPipelineAsset[] m_qualityChanger;
    private int m_qualityLevel;
    private int m_resolutionLevel;
    // Start is called before the first frame update
    private void Awake()
    {
        #region Resolution
        int indexOfResolution = 0;

        m_resolutionsMultiple = Screen.resolutions;
        m_resolutionDropDown.ClearOptions();
        List<string> options = new List<string>();
        for (int i = 0; i < m_resolutionsMultiple.Length; i++)
        {
            int value = i - 1;
            string option = m_resolutionsMultiple[i].width + " x " + m_resolutionsMultiple[i].height;
            if (value != -1)
            {
                if (((m_resolutionsMultiple[i].width != m_resolutionsMultiple[i - 1].width &&
                    m_resolutionsMultiple[i].height != m_resolutionsMultiple[i - 1].height) || m_resolutionsMultiple[i].width == m_resolutionsMultiple[i - 1].width &&
                    m_resolutionsMultiple[i].height != m_resolutionsMultiple[i - 1].height) || m_resolutionsMultiple[i].width != m_resolutionsMultiple[i - 1].width &&
                    m_resolutionsMultiple[i].height == m_resolutionsMultiple[i - 1].height)
                    options.Add(option);
            }
            if (i == 0)
                options.Add(option);
            if (m_resolutionsMultiple[i].width == Screen.currentResolution.width &&
                m_resolutionsMultiple[i].height == Screen.currentResolution.height)
            {
                indexOfResolution = i;
            }
        }
        Screen.SetResolution(m_resolutionsMultiple[indexOfResolution].width, m_resolutionsMultiple[indexOfResolution].height, true);
        m_resolutionLevel = indexOfResolution;
        m_resolutionDropDown.AddOptions(options);
        m_resolutionDropDown.value = indexOfResolution;
        m_resolutionDropDown.RefreshShownValue();
        #endregion
        #region Quality
        m_quality.ClearOptions();
        int amountOFQuality = m_qualityChanger.Length;
        List<string> qualityOptions = new List<string>();

        for (int i = 0; i < amountOFQuality; i++)
        {
            string option = m_qualityChanger[i].name;
            qualityOptions.Add(option);
        }
        QualitySettings.SetQualityLevel(2);
        m_qualityLevel = GlobalVariables.Instance.m_qualityDisplayInt;
        m_quality.AddOptions(qualityOptions);
        m_quality.value = m_qualityLevel;
        m_quality.RefreshShownValue();
        #endregion
    }
    public void SaveValues()
    {
        Resolution resolution = m_resolutionsMultiple[m_resolutionLevel];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        QualitySettings.SetQualityLevel(m_qualityLevel);
        GlobalVariables.Instance.m_qualityDisplayInt = m_qualityLevel;
        GlobalVariables.Instance.m_resolutionInt = m_resolutionLevel;
        m_resolutionDropDown.RefreshShownValue();
        m_quality.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
        m_resolutionLevel = resolutionIndex;
    }
    public void SetQuality(int qualityIndex)
    {
        m_qualityLevel = qualityIndex;
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
}
