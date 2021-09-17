using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
public class MainMenu : MonoBehaviour
{
    [SerializeField] private GameObject m_playGameOBJ;
    [SerializeField] private GameObject m_levelSelectOBJ;
    [SerializeField] private GameObject m_firstButtonMain;
    [SerializeField] private GameObject m_firstButtonLevelS;
    [SerializeField] private GameObject m_parentLevelSelect;
    [SerializeField] private GameObject m_parentMainMenu;
    private ColorBlock colourSelected;
    private ColorBlock naturalState;
    private bool m_gamePadActive = false;
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
        m_firstButtonLevelS.GetComponent<Button>().colors = colourSelected;
        EventSystem.current.SetSelectedGameObject(m_firstButtonMain);
    }
    private void Update()
    {
        if (Gamepad.current.leftStick.IsActuated() && (EventSystem.current.currentSelectedGameObject == null || m_gamePadActive))
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
        else if (Mouse.current.IsActuated())
        {
            if(!Gamepad.current.leftStick.IsActuated())
                m_gamePadActive = false;
            if (EventSystem.current.alreadySelecting)
            {
                m_firstButtonLevelS.GetComponent<Button>().colors = naturalState;
                m_firstButtonMain.GetComponent<Button>().colors = naturalState;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }
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
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}
