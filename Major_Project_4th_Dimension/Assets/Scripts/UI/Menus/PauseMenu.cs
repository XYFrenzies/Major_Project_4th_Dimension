using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PauseMenu : MonoBehaviour
{
    [SerializeField]private PlayerInput playerInput;
    [SerializeField] private GameObject m_pauseMenu = null;
    [SerializeField] private GameObject m_gameUI = null;
    [SerializeField] private GameObject m_optionsUI = null;
    [SerializeField] private GameObject m_fsOptionsMenu = null;
    [SerializeField] private GameObject m_fsPauseMenu = null;
    private InputAction pauseMenuAction;
    private InputAction pauseGamepad;
    private bool isPaused = false;
    private ColorBlock colourSelected;
    private ColorBlock naturalState;
    private bool m_gamePadActive = false;
    private void Awake()
    {
        pauseMenuAction = playerInput.actions["PauseMenu"];
        pauseGamepad = playerInput.actions["PauseMoveController"];
        m_pauseMenu.SetActive(false);
        colourSelected.colorMultiplier = 1;
        colourSelected.selectedColor = new Color(0, 1, 0.117f, 1);
        colourSelected.normalColor = new Color(1, 1, 1, 1);
        colourSelected.highlightedColor = new Color(0, 1, 0.117f, 1);

        naturalState.colorMultiplier = 1;
        naturalState.highlightedColor = new Color(0, 1, 0.117f, 1);
        naturalState.selectedColor = new Color(0, 1, 1, 1);
        naturalState.normalColor = new Color(1, 1, 1, 1);
    }
    private void OnEnable()
    {
        pauseMenuAction.performed += Pause;
    }
    private void OnDisable()
    {
        pauseMenuAction.performed -= Pause;
    }
    public void Pause(InputAction.CallbackContext context)
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
    private void Update()
    {
        if (isPaused)
            pauseGamepad.started += ctx => Back();

        if (Gamepad.current != null && Gamepad.current.leftStick.IsActuated() && (EventSystem.current.currentSelectedGameObject == null || m_gamePadActive))
        {
            if (m_pauseMenu != null && m_pauseMenu.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(m_fsPauseMenu);
                m_fsPauseMenu.GetComponent<Button>().colors = colourSelected;
            }
            else if (m_optionsUI != null && m_optionsUI.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(m_fsOptionsMenu);
                m_fsOptionsMenu.GetComponent<Scrollbar>().colors = colourSelected;
            }
            m_gamePadActive = true;
        }
        else if (Mouse.current.IsActuated())
        {
            if (Gamepad.current != null && !Gamepad.current.leftStick.IsActuated())
                m_gamePadActive = false;
            if (EventSystem.current.alreadySelecting)
            {
                m_fsPauseMenu.GetComponent<Button>().colors = naturalState;
                m_fsOptionsMenu.GetComponent<Scrollbar>().colors = naturalState;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

    }
    private void Back() 
    {
        if (m_pauseMenu != null && m_pauseMenu.activeSelf)
        {
            ResumeGame();
        }
        else if (m_optionsUI != null && m_optionsUI.activeSelf)
        {
            OptionsMenuBack();
        }
    }
    private void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        m_pauseMenu.SetActive(true);
        m_gameUI.SetActive(false);
        isPaused = true;
        m_fsPauseMenu.GetComponent<Button>().colors = colourSelected;
        EventSystem.current.SetSelectedGameObject(m_fsPauseMenu);

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
        if (m_gamePadActive)
        {
            m_fsPauseMenu.GetComponent<Button>().colors = colourSelected;
            EventSystem.current.SetSelectedGameObject(m_fsPauseMenu);
        }
        else if (!m_gamePadActive)
        {
            m_fsPauseMenu.GetComponent<Button>().colors = naturalState;
            EventSystem.current.SetSelectedGameObject(null);
        }
        //Need to fill this in when the options menu is ready to be used.
    }
    public void OptionsMenu()
    {
        m_optionsUI.SetActive(true);
        m_pauseMenu.SetActive(false);
        if (m_gamePadActive)
        {
            m_fsOptionsMenu.GetComponent<Scrollbar>().colors = colourSelected;
            EventSystem.current.SetSelectedGameObject(m_fsOptionsMenu);
        }
        else if (!m_gamePadActive)
        {
            m_fsOptionsMenu.GetComponent<Scrollbar>().colors = naturalState;
            EventSystem.current.SetSelectedGameObject(null);
        }

        //Need to fill this in when the options menu is ready to be used.
    }
    public void ReturnToMenu(string nameOfScene)
    {
        Time.timeScale = 1;
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
    public void RestartLevel() 
    {
        //Scene scene = SceneManager.GetActiveScene();
        //SceneManager.LoadScene(scene.name);
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }

}
