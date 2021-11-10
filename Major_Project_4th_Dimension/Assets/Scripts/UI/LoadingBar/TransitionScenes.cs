using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class TransitionScenes : MonoBehaviour
{
    [SerializeField] private Slider m_slider;
    [SerializeField] private Text m_percentageText;
    [SerializeField] private Text m_randQuoteTxt;
    [SerializeField] private List<string> m_mainMenuFacts;
    [SerializeField] private List<string> m_firstLevelFacts;
    [SerializeField] private List<string> m_secondLevelFacts;
    [SerializeField] private List<string> m_thirdLevelFacts;
    [SerializeField] private List<string> m_oasisFacts;
    private string m_nameOfScene;
    private string m_randQuote;
    // Start is called before the first frame update
    private void Awake()
    {
        m_nameOfScene = GlobalVariables.Instance.GetPreScene();
        if (m_nameOfScene == SceneManager.GetActiveScene().name || m_nameOfScene == null)
            m_nameOfScene = "Level_01";
        switch (m_nameOfScene) 
        {
            case "MainMenu":
                m_randQuote = SceneFacts(m_mainMenuFacts);
                break;
            case "Level_01":
                m_randQuote = SceneFacts(m_firstLevelFacts);
                break;
            case "Level_02":
                m_randQuote = SceneFacts(m_secondLevelFacts);
                break;
            case "Level_03":
                m_randQuote = SceneFacts(m_thirdLevelFacts);
                break;
            case "Oasis":
                m_randQuote = SceneFacts(m_oasisFacts);
                break;
        }
        m_randQuoteTxt.text = m_randQuote;
    }
    private string SceneFacts(List<string> randQuote) 
    {
        System.Random rnd = new System.Random();
        int value = rnd.Next(0, randQuote.Count);
        string quote = randQuote[value];
        return quote;
    }
    // Update is called once per frame
    private void Update()
    {
        StartCoroutine(LoadNewScene());
    }
    private IEnumerator LoadNewScene() 
    {
        yield return new WaitForEndOfFrame();
        AsyncOperation async = SceneManager.LoadSceneAsync(m_nameOfScene);
        while (async.progress < 1)
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            m_slider.value = progress;
            m_percentageText.text = progress * 100f + "%";
            yield return new WaitForEndOfFrame();
        }
    }
}
