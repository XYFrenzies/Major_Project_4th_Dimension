using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class GameUI : MonoBehaviour
{
    [SerializeField] private GameObject m_diedUI;
    [SerializeField] private PlayerInput playerInput;
    public DoorData doorData;
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
        doorData.door1Open = false;
        doorData.door2Open = false;
        doorData.door3Open = false;
        doorData.door4Open = false;
        doorData.door5Open = false;
        UnityEditor.EditorApplication.isPlaying = false;
#else
        doorData.door1Open = false;
        doorData.door2Open = false;
        doorData.door3Open = false;
        doorData.door4Open = false;
        doorData.door5Open = false;
        Application.Quit();
#endif
    }
}
