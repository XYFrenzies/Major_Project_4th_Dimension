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
    public void LevelToSelect(string name) 
    {
        if(m_playerAnimtion != null)
            m_playerAnimtion.SetBool("playGame", true);
        gameObject.SetActive(false);
        StartCoroutine(LevelBeforeStart(name));
    }
    private IEnumerator LevelBeforeStart(string name) 
    {
        yield return new WaitForSeconds(m_timeBeforeStartScene);
        SceneManager.LoadScene(name);
    }
}
