using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
public class PauseMenu : MonoBehaviour
{
    public PlayerInput playerInput;
    private InputAction pauseMenuAction;
    private InputAction moveAction;
    [SerializeField] private GameObject m_pauseMenu = null;
    [SerializeField] private GameObject m_gameUI = null;
    [SerializeField] private GameObject m_optionsUI = null;
    private bool isPaused = false;
    private bool isGamePad = false;

    private Vector2 cursorPosition;
    private void Awake()
    {
        pauseMenuAction = playerInput.actions["PauseMenu"];
        moveAction = playerInput.actions["PauseMoveController"];
        m_pauseMenu.SetActive(false);
        cursorPosition = Mouse.current.position.ReadValue();
    }
    private void OnEnable()
    {
        pauseMenuAction.performed += _ => Pause();
    }
    private void OnDisable()
    {
        pauseMenuAction.performed -= _ => Pause();
    }
    private void Update()
    {
        if (isPaused)
        {
            MoveController();
        }
    }
    private void MoveController() 
    {
        Vector2 delta = moveAction.ReadValue<Vector2>();
        if (delta != Vector2.zero)
        {
            Vector2 newPos = cursorPosition + delta;
            Mouse.current.WarpCursorPosition(newPos * Time.unscaledDeltaTime);
        }
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
