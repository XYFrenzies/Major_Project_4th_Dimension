using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using TMPro;
using UnityEngine.UI;
public class RenderingMenu : Singleton<RenderingMenu>
{
    [SerializeField] private TMP_Dropdown m_resolutionDropDown;
    [SerializeField] private TMP_Dropdown m_quality;
    [SerializeField] private Toggle m_fullscreen;
    [SerializeField] private HDRenderPipelineAsset[] m_qualityChanger;
    private Resolution[] m_resolutionsMultiple;
    private int m_qualityLevel;
    private int m_resolutionLevel;
    private bool m_isFullScreen = true;
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
        m_quality.value = QualitySettings.GetQualityLevel();
        m_quality.RefreshShownValue();
        #endregion
        m_isFullScreen = GlobalVariables.Instance.GetScreenIsOn();
        m_fullscreen.isOn = m_isFullScreen;

    }
    private void Start()
    {
        
    }
    public void SaveValues()
    {
        Resolution resolution = m_resolutionsMultiple[m_resolutionLevel];
        Screen.fullScreen = m_isFullScreen;
        Screen.SetResolution(resolution.width * 2, resolution.height * 2, Screen.fullScreen);
        QualitySettings.SetQualityLevel(m_qualityLevel);
        QualitySettings.renderPipeline = m_qualityChanger[m_qualityLevel];
        GlobalVariables.Instance.m_qualityDisplayInt = m_qualityLevel;
        GlobalVariables.Instance.m_resolutionInt = m_resolutionLevel;
        GlobalVariables.Instance.SaveFullScreenIsOn(Screen.fullScreen);
        m_quality.value = QualitySettings.GetQualityLevel();
        m_quality.RefreshShownValue();
        m_resolutionDropDown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
        m_resolutionLevel = resolutionIndex;
    }
    public void SetQuality(int qualityIndex)
    {
        m_qualityLevel = qualityIndex;
    }
    public void SetFullScreen(bool isFullscreen) 
    {
        m_isFullScreen = isFullscreen;
    }
}
