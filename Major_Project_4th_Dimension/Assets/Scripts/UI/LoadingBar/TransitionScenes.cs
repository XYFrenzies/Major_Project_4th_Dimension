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
    [SerializeField] private Image backgroundImg;
    
    //These are all the texts that go into the menus.
    [SerializeField] private List<string> m_mainMenuFacts;
    [SerializeField] private List<string> m_firstLevelFacts;
    [SerializeField] private List<string> m_secondLevelFacts;
    [SerializeField] private List<string> m_thirdLevelFacts;
    [SerializeField] private List<string> m_oasisFacts;

    [SerializeField] private List<Sprite> m_mainMenuImages;
    [SerializeField] private List<Sprite> m_firstLevelImages;
    [SerializeField] private List<Sprite> m_secondLevelImages;
    [SerializeField] private List<Sprite> m_thirdLevelImages;
    [SerializeField] private List<Sprite> m_oasisImages;
    private string m_nameOfScene;
    private string m_randQuote;
    private bool m_alreadyLoaded = false;
    // Start is called before the first frame update
    private void Awake()
    {
        if (backgroundImg == null)
            backgroundImg = transform.Find("Image").GetComponent<Image>();
        if (GlobalVariables.Instance != null)
            m_nameOfScene = GlobalVariables.Instance.GetPreScene();
        if (m_nameOfScene == SceneManager.GetActiveScene().name || m_nameOfScene == null || m_nameOfScene == "")
            m_nameOfScene = "Level_01";
        switch (m_nameOfScene) 
        {
            case "MainMenu":
                m_randQuote = SceneFacts(m_mainMenuFacts);
                backgroundImg.sprite = SceneFacts(m_mainMenuImages);
                break;
            case "Level_01":
                m_randQuote = SceneFacts(m_firstLevelFacts);
                backgroundImg.sprite = SceneFacts(m_firstLevelImages);
                break;
            case "Level_02":
                m_randQuote = SceneFacts(m_secondLevelFacts);
                backgroundImg.sprite = SceneFacts(m_secondLevelImages);
                break;
            case "Level_03":
                m_randQuote = SceneFacts(m_thirdLevelFacts);
                backgroundImg.sprite = SceneFacts(m_thirdLevelImages);
                break;
            case "Oasis":
                m_randQuote = SceneFacts(m_oasisFacts);
                backgroundImg.sprite = SceneFacts(m_oasisImages);
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
    private Sprite SceneFacts(List<Sprite> randQuote)
    {
        System.Random rnd = new System.Random();
        int value = rnd.Next(0, randQuote.Count);
        Sprite quote = randQuote[value];
        return quote;
    }
    // Update is called once per frame
    private void Update()
    {
        if(!m_alreadyLoaded)
        StartCoroutine(LoadNewScene());
    }
    private IEnumerator LoadNewScene() 
    {
        yield return new WaitForEndOfFrame();
        AsyncOperation async = SceneManager.LoadSceneAsync(m_nameOfScene);
        async.allowSceneActivation = false;
        while (!async.isDone)
        {
            m_alreadyLoaded = true;
            float progress = Mathf.Clamp01(async.progress / 0.9f);
            m_slider.value = progress;
            m_percentageText.text = progress * 100f + "%";
            if (async.progress == 0.9f)
            {
                m_slider.value = 1f;
                async.allowSceneActivation = true;
            }
            yield return null;
        }
    }
}
