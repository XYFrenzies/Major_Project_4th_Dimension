using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_pauseMenu = null;
    [SerializeField] private GameObject m_gameUI = null;

    private void Awake()
    {
        m_pauseMenu.SetActive(false);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void ResumeGame() 
    {
        Time.timeScale = 1;
        m_pauseMenu.SetActive(false);
        m_gameUI.SetActive(false);
    }
    public void OptionsMenu() 
    {
        //Need to fill this in when the options menu is ready to be used.
    }
    public void ReturnToMenu(string nameOfScene)
    {
        SceneManager.LoadScene(nameOfScene);
    }
    public void ExitGame() 
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

}
