using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject m_diedUI;

    public void PlayerDied() 
    {
        if(m_diedUI != null)
            m_diedUI.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void RestartGame() 
    {
        if (m_diedUI != null)
            m_diedUI.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
