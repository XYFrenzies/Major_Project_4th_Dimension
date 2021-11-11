using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour
{
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.None;
       
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("MainMenu");

    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
