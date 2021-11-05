using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject m_diedUI;
    [SerializeField] private PlayerInput playerInput;
    public void PlayerDied() 
    {
        playerInput.SwitchCurrentActionMap("Menu");
        if (m_diedUI != null)
            m_diedUI.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void RestartGame() 
    {
        playerInput.SwitchCurrentActionMap("Player");
        if (m_diedUI != null)
            m_diedUI.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BackToMainMenu(string name) 
    {
        SceneManager.LoadScene(name);
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
