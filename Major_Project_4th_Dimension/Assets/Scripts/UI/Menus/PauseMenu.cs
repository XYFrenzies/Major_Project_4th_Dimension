using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;
public class PauseMenu : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction pauseMenuAction;
    private InputAction moveAction;
    private InputAction mouseControl;
    [SerializeField] private GameObject m_pauseMenu = null;
    [SerializeField] private GameObject m_gameUI = null;
    [SerializeField] private GameObject m_optionsUI = null;
    [SerializeField] private Texture img;
    private bool isPaused = false;
    private Vector2 screenPos;
    private bool isGamePadActive = false;
    private bool isMouseActive = true;
    [SerializeField] private float gamepadSpeed = 0.2f;
    private void Awake()
    {
        pauseMenuAction = playerInput.actions["PauseMenu"];
        moveAction = playerInput.actions["PauseMoveController"];
        mouseControl = playerInput.actions["MousePosition"];
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
