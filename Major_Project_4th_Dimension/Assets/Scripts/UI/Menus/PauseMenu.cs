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
    private InputAction mouseControl;
    [SerializeField] private GameObject m_pauseMenu = null;
    [SerializeField] private GameObject m_gameUI = null;
    [SerializeField] private GameObject m_optionsUI = null;
    private bool isPaused = false;
    private Vector2 cursorPosition;
    private Vector3 screenPos;
    [SerializeField] private float gamepadSpeed = 2.0f;
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
    private void Update()
    {
        if (isPaused)
        {
            //MoveController();
            moveAction.performed += _ => MoveController();
        }
    }
    private void MoveController()
    {
        cursorPosition = mouseControl.ReadValue<Vector2>();
        if (cursorPosition == Vector2.zero)
        {
            screenPos = new Vector2(Screen.width / 2, Screen.height);
        }
        else
            screenPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width + cursorPosition.x, Screen.height + cursorPosition.y, 0));
        Vector2 mouseDelta = moveAction.ReadValue<Vector2>();
        screenPos.x += mouseDelta.x * gamepadSpeed;
        screenPos.y += mouseDelta.y * gamepadSpeed;
        Mouse.current.WarpCursorPosition(screenPos);

        //Vector2 delta = moveAction.ReadValue<Vector2>();
        //cursorPosition += delta;
        //cursorPosition.x = Mathf.Clamp(cursorPosition.x, 0, Screen.width);
        //cursorPosition.y = Mathf.Clamp(cursorPosition.y, 0, Screen.height);
        //InputState.Change(Mouse.current.position, cursorPosition);
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
