using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Any designer can use this to select the name of the level to proceed to next.
/// </summary>
/// //playGame
public class LevelSelect : MonoBehaviour
{
    [SerializeField] private Animator m_playerAnimtion;
    [SerializeField] private float m_timeBeforeStartScene;
    [SerializeField] private GameObject m_levelSelect;
    [SerializeField] private GameObject m_title;
    private string m_levelToSelect;
    public void LevelToSelect(string nameofScene)
    {
        m_levelToSelect = nameofScene;
        if (m_playerAnimtion != null)
            m_playerAnimtion.SetBool("playGame", true);
        StartCoroutine(LoadScene());
        m_levelSelect.SetActive(false);
        m_title.SetActive(false);
    }
    public void LevelToGoToNext(string name) 
    {
        GlobalVariables.Instance.SaveScene(name);
    }
    private IEnumerator LoadScene() 
    {
        yield return new WaitForSeconds(m_timeBeforeStartScene);
        SceneManager.LoadScene(m_levelToSelect);
    }
}
