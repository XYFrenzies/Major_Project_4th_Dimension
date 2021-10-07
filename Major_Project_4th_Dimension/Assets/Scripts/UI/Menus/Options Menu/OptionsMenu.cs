using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private List<GameObject> m_menus;
    [SerializeField] private List<GameObject> m_firstButtonInMenus;
    [SerializeField] private GameObject m_mainMenu;
    [SerializeField] private GameObject m_firstButtonMainMenu;
    private InputAction m_optionsMenuAction;
    private ColorBlock colourSelected;//Changing the ui selection colour
    private ColorBlock naturalState;//The natural state of the ui selection colour
    private InputAction pauseGamepad;//Checks if the b button has been pressed on the controller.
    private int m_menuChosen = 1;
    // Start is called before the first frame update
    private void Start()
    {
        m_optionsMenuAction = playerInput.actions["OptionsMovement"];
        pauseGamepad = playerInput.actions["PauseMoveController"];
        //m_pauseMenu.SetActive(false);
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
        m_menus[0].SetActive(true);
        m_mainMenu.SetActive(false);
        if (CheckInput.Instance.CheckGamePadActive())
        {
            EventSystem.current.SetSelectedGameObject(m_firstButtonInMenus[0]);
            m_firstButtonInMenus[0].GetComponent<Scrollbar>().colors = colourSelected;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            m_firstButtonInMenus[0].GetComponent<Scrollbar>().colors = naturalState;
        }
    }
    public void BackGamPad()
    {
        m_mainMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(m_firstButtonMainMenu);
        m_firstButtonMainMenu.GetComponent<Button>().colors = colourSelected;
        foreach (var item in m_menus)
        {
            item.SetActive(false);
        }
        Time.timeScale = 1;
    }
    public void BackToGame()
    {
        m_mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(m_firstButtonMainMenu);
        m_firstButtonMainMenu.GetComponent<Button>().colors = colourSelected;
        foreach (var item in m_menus)
        {
            item.SetActive(false);
        }
    }
    // Update is called once per frame
    private void Update()
    {
        pauseGamepad.started += ctx => BackGamPad();
        m_optionsMenuAction.performed += OptionsMove;
        UpdateInput();
    }
    private void OptionsMove(InputAction.CallbackContext context)
    {
        if (context.action.ReadValue<Vector2>().x > 0)
        {
            m_menuChosen -= 1;
            if (m_menuChosen <= 0)
                m_menuChosen = m_menus.Count;

        }
        else if (context.action.ReadValue<Vector2>().y < 0)
        {
            m_menuChosen += 1;
            if (m_menuChosen >= m_menus.Count)
                m_menuChosen = 1;
        }
        SetMenu(m_menuChosen);
    }

    public void SetMenu(int value) 
    {
        foreach (var item in m_menus)
        {
            if (item == m_menus[value])
                item.SetActive(true);
            else
                item.SetActive(false);
        }
    }

    public void SetMenu(GameObject menuSelected)
    {
        foreach (var item in m_menus)
        {
            if (item == menuSelected)
                item.SetActive(true);
            else
                item.SetActive(false);
        }
    }
    private void UpdateInput()
    {
        if (CheckInput.Instance.CheckGamePadActive() && (EventSystem.current.currentSelectedGameObject == null))
        {
            EventSystem.current.SetSelectedGameObject(m_firstButtonInMenus[0]);
            m_firstButtonInMenus[0].GetComponent<Scrollbar>().colors = colourSelected;
        }
        else if (Mouse.current.IsActuated())
        {
            if (EventSystem.current.alreadySelecting)
            {
                m_firstButtonInMenus[0].GetComponent<Scrollbar>().colors = naturalState;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }

}
