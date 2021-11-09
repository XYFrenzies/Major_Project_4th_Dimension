using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class TransitionScenes : MonoBehaviour
{
    [SerializeField] private List<string> m_mainMenuFacts;
    [SerializeField] private List<string> m_firstLevelFacts;
    [SerializeField] private List<string> m_secondLevelFacts;
    [SerializeField] private List<string> m_thirdLevelFacts;
    [SerializeField] private List<string> m_oasisFacts;
    private string m_nameOfScene;
    private string randQuote;
    // Start is called before the first frame update
    private void Awake()
    {
        m_nameOfScene = GlobalVariables.Instance.GetPreScene();
        if (m_nameOfScene == SceneManager.GetActiveScene().name)
            m_nameOfScene = "Level_01";
        switch (m_nameOfScene) 
        {
            case "MainMenu":
                randQuote = SceneFacts(m_mainMenuFacts);
                break;
            case "Level_01":
                randQuote = SceneFacts(m_firstLevelFacts);
                break;
            case "Level_02":
                randQuote = SceneFacts(m_secondLevelFacts);
                break;
            case "Level_03":
                randQuote = SceneFacts(m_thirdLevelFacts);
                break;
            case "Oasis":
                randQuote = SceneFacts(m_oasisFacts);
                break;
        }
    }
    private string SceneFacts(List<string> randQuote) 
    {
        System.Random rnd = new System.Random();
        int value = rnd.Next(0, randQuote.Count);
        string quote = randQuote[value];
        return quote;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
