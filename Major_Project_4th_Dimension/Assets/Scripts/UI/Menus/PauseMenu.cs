using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
/// <summary>
/// This script works with both controller and keyboard support on the pause menu
/// </summary>
public class PauseMenu : MonoBehaviour
{
    [SerializeField]private PlayerInput playerInput;//Input from the player
    [SerializeField] private GameObject m_pauseMenu = null;//Pause menu obj
    [SerializeField] private GameObject m_gameUI = null;//Game ui obj
    [SerializeField] private GameObject m_optionsUI = null;//Options menu obj
    [SerializeField] private GameObject m_fsOptionsMenu = null;//Checking object first selected in options menu
    [SerializeField] private GameObject m_fsPauseMenu = null;//Checking object first selected in pause menu
    private InputAction pauseMenuAction;//Checks if the input has been called for the pause menu
    private InputAction pauseGamepad;//Checks if the b button has been pressed on the controller.
    private bool isPaused = false;//Checks if the game has been paused
    private ColorBlock colourSelected;//Changing the ui selection colour
    private ColorBlock naturalState;//The natural state of the ui selection colour
    private bool m_gamePadActive = false;//Checking if the gamepad is active.
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
    //Callback when a button has been pressed (the pause button).
    public void Pause(InputAction.CallbackContext context)
    {
        if (isPaused)
            ResumeGame();
        else
            PauseGame();
    }
    /// <summary>
    ///For every frame, checking if theres a change in input and whether the button is being selected or not. 
    ///Only one input can be selected at a time.
    /// </summary>
    private void Update()
    {
        if (Gamepad.current != null && Gamepad.current.leftStick.IsActuated() && (EventSystem.current.currentSelectedGameObject == null || m_gamePadActive))
        {
            if (isPaused)
                pauseGamepad.started += ctx => Back();

            if (m_pauseMenu != null && m_pauseMenu.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(m_fsPauseMenu);
                m_fsPauseMenu.GetComponent<Button>().colors = colourSelected;
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
                EventSystem.current.SetSelectedGameObject(null);
            }
        }

    }
    //Uses the back input to return back to menu from pause or options menu.
    private void Back()
    {
        if (m_pauseMenu != null && m_pauseMenu.activeSelf)
        {
            ResumeGame();
        }
    }
    //When the game is paused, this function will occur.
    private void PauseGame()
    {
        //Setting the cursor to visible, timescale = 0, cursor is not locked, changing menus and checking the initial input.
        Cursor.visible = true;
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        m_pauseMenu.SetActive(true);
        m_gameUI.SetActive(false);
        isPaused = true;
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
    }
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        m_pauseMenu.SetActive(false);
        m_gameUI.SetActive(true);
        m_optionsUI.SetActive(false);
    }
    public void OptionsMenu()
    {
        m_optionsUI.SetActive(true);
        m_pauseMenu.SetActive(false);
    }
    //Returns the player to the main menu
    public void ReturnToMenu(string nameOfScene)
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(nameOfScene);
    }
    //Exits the game (the if statements is determining if its in build or not).
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
    //Reloads the scene.
    public void RestartLevel() 
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
