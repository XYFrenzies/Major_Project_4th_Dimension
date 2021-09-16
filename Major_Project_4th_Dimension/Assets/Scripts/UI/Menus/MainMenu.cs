using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_playGameOBJ;
    [SerializeField] private GameObject m_levelSelectOBJ;
    public void PlayGame()
    {
        m_playGameOBJ.SetActive(false);
        m_levelSelectOBJ.SetActive(true);
    }
    public void ReturnToMenu()
    {
        m_playGameOBJ.SetActive(true);
        m_levelSelectOBJ.SetActive(false);
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
