using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
/// <summary>
/// This script works with both controller and keyboard support on the main menu
/// </summary>
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_playGameOBJ;//The Playing Game obj
    [SerializeField] private GameObject m_optionsOBJ;//Options Menu for the obj
    [SerializeField] private GameObject m_levelSelectOBJ;//The Level select obj
    [SerializeField] private GameObject m_firstButtonMain;//First selected object for the play menu
    [SerializeField] private GameObject m_firstButtonLevelS;//First selected object in level select
    [SerializeField] private GameObject m_parentLevelSelect;//The parent of the LevelSelect
    [SerializeField] private GameObject m_parentMainMenu;//The parent of the Main Menu
    private ColorBlock colourSelected;//Changing the ui selection colour
    private ColorBlock naturalState;//The natural state of the ui selection colour
    private bool m_gamePadActive = false;//Checking if the gamepad is active.
    private void Awake()
    {
        colourSelected.colorMultiplier = 1;
        colourSelected.selectedColor = new Color(0, 1, 0.117f, 0.39f);
        colourSelected.normalColor = new Color(1, 1, 1, 0);
        colourSelected.highlightedColor = new Color(0, 1, 0.117f, 0.39f);

        naturalState.colorMultiplier = 1;
        naturalState.highlightedColor = new Color(0, 1, 0.117f, 0.39f);
        naturalState.selectedColor = new Color(0, 1, 1, 0);
        naturalState.normalColor = new Color(1, 1, 1, 0);
    }
    private void Start()
    {
        CheckInput.Instance.GamePadActive();
        if (CheckInput.Instance.CheckGamePadActiveGame())
        {
            m_firstButtonLevelS.GetComponent<Button>().colors = colourSelected;
            EventSystem.current.SetSelectedGameObject(m_firstButtonMain);
        }
    }
    /// <summary>
    ///For every frame, checking if theres a change in input and whether the button is being selected or not. 
    ///Only one input can be selected at a time.
    /// </summary>
    private void Update()
    {

        if (CheckInput.Instance.CheckGamePadActiveGame() && EventSystem.current.currentSelectedGameObject == null)
        {
            if (m_parentMainMenu != null && m_parentMainMenu.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(m_firstButtonMain);
                m_firstButtonMain.GetComponent<Button>().colors = colourSelected;
            }
            else if (m_parentLevelSelect != null && m_parentLevelSelect.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(m_firstButtonLevelS);
                m_firstButtonLevelS.GetComponent<Button>().colors = colourSelected;
            }
            m_gamePadActive = true;
        }
        else if (CheckInput.Instance.CheckMouseActive())
        {
            m_gamePadActive = false;
            if (EventSystem.current.alreadySelecting)
            {
                m_firstButtonLevelS.GetComponent<Button>().colors = naturalState;
                m_firstButtonMain.GetComponent<Button>().colors = naturalState;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }
    /// <summary>
    /// Once the button has been pressed, a change in ui occurs and checks for the initial input.
    /// Sends the player to the level select.
    /// </summary>
    public void PlayGame()
    {
        m_playGameOBJ.SetActive(false);
        m_levelSelectOBJ.SetActive(true);
        if (m_gamePadActive)
        {
            m_firstButtonLevelS.GetComponent<Button>().colors = colourSelected;
            EventSystem.current.SetSelectedGameObject(m_firstButtonLevelS);
        }
        else if (!m_gamePadActive)
        {
            m_firstButtonLevelS.GetComponent<Button>().colors = naturalState;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
    /// <summary>
    /// Once the button has been pressed, a change in ui occurs and checks for the initial input.
    /// Sends the player back to the original menu
    /// </summary>
    public void ReturnToMenu()
    {
        m_playGameOBJ.SetActive(true);
        m_levelSelectOBJ.SetActive(false);
        if (m_gamePadActive)
        {
            m_firstButtonMain.GetComponent<Button>().colors = colourSelected;
            EventSystem.current.SetSelectedGameObject(m_firstButtonMain);
        }
        else if (!m_gamePadActive)
        {
            m_firstButtonMain.GetComponent<Button>().colors = naturalState;
            EventSystem.current.SetSelectedGameObject(null);
        }
    }
    //Exits the game (the if statements is determining if its in build or not).
    public void OptionsMenu()
    {
        m_optionsOBJ.SetActive(true);
        m_playGameOBJ.SetActive(false);
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
