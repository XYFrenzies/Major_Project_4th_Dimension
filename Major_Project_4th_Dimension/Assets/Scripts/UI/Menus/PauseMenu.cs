using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    [SerializeField]private PlayerInput playerInput;
    [SerializeField] private GameObject m_pauseMenu = null;
    [SerializeField] private GameObject m_gameUI = null;
    [SerializeField] private GameObject m_optionsUI = null;
    [SerializeField] private GameObject m_fsOptionsMenu = null;
    [SerializeField] private float gamepadSpeed = 0.2f;
    private InputAction pauseMenuAction;
    private bool isPaused = false;
    private void Awake()
    {
        pauseMenuAction = playerInput.actions["PauseMenu"];
        m_pauseMenu.SetActive(false);
    }
    private void OnEnable()
    {
        pauseMenuAction.performed += _ => Pause();
    }
    private void OnDisable()
    {
        pauseMenuAction.performed -= _ => Pause();
    }
    public void Pause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        m_pauseMenu.SetActive(true);
        m_gameUI.SetActive(false);
        isPaused = true;
    }
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        m_pauseMenu.SetActive(false);
        m_gameUI.SetActive(true);
        m_optionsUI.SetActive(false);
    }
    public void OptionsMenuBack()
    {
        m_optionsUI.SetActive(false);
        m_pauseMenu.SetActive(true);
        //Need to fill this in when the options menu is ready to be used.
    }
    public void OptionsMenu()
    {
        m_optionsUI.SetActive(true);
        m_pauseMenu.SetActive(false);
        UnityEngine.EventSystems.EventSystem.current.firstSelectedGameObject = m_fsOptionsMenu;
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
