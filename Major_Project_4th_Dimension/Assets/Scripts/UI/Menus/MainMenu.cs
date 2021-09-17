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
    [SerializeField] private PlayerInput m_controllerInput;
    private InputAction menuMenuAction;
    private ColorBlock colourSelected;
    private void Awake()
    {
        //menuMenuAction = m_controllerInput.actions[];
        colourSelected.colorMultiplier = 1;
        colourSelected.selectedColor = new Color(0, 1, 0.117f, 0.39f);
        colourSelected.normalColor = new Color(1, 1, 1, 0);
        colourSelected.highlightedColor = new Color(0, 1, 0.117f, 0.39f);
    }
    private void Start()
    {
        m_firstButtonLevelS.GetComponent<Button>().colors = colourSelected;
        EventSystem.current.SetSelectedGameObject(m_firstButtonMain);
    }
    public void PlayGame()
    {
        m_playGameOBJ.SetActive(false);
        m_levelSelectOBJ.SetActive(true);
        m_firstButtonLevelS.GetComponent<Button>().colors = colourSelected;
        EventSystem.current.SetSelectedGameObject(m_firstButtonLevelS);
    }
    public void ReturnToMenu()
    {
        m_playGameOBJ.SetActive(true);
        m_levelSelectOBJ.SetActive(false);
        m_firstButtonMain.GetComponent<Button>().colors = colourSelected;
        EventSystem.current.SetSelectedGameObject(m_firstButtonMain);
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
