using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
public class OptionsMenu : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private List<GameObject> m_menus;
    [SerializeField] private List<GameObject> m_firstButtonInMenus;
    [SerializeField] private List<GameObject> m_gamePadOptions;
    [SerializeField] private List<GameObject> m_optionsSceneSelect;
    [SerializeField] private GameObject m_mainMenu;
    [SerializeField] private GameObject m_firstButtonMainMenu;
    private InputAction m_optionsMenuActionRight;
    private InputAction m_optionsMenuActionLeft;
    private ColorBlock colourSelected;//Changing the ui selection colour
    private ColorBlock naturalState;//The natural state of the ui selection colour
    private InputAction pauseGamepad;//Checks if the b button has been pressed on the controller.
    private int m_menuChosen = 0;
    private bool m_alreadySeenGamePad;
    private bool m_alreadySeenMouse;
    // Start is called before the first frame update
    private void Awake()
    {
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


        playerInput.SwitchCurrentActionMap("Menu");
        m_optionsMenuActionRight = playerInput.actions["OptionsMovementRight"];
        m_optionsMenuActionLeft = playerInput.actions["OptionsMovementLeft"];
        pauseGamepad = playerInput.actions["PauseMoveController"];
        pauseGamepad.started += BackGamPad;
        m_optionsMenuActionRight.started += OptionsMoveRight;
        m_optionsMenuActionLeft.started += OptionsMoveLeft;
        m_menus[0].SetActive(true);
        if (CheckInput.Instance.CheckGamePadActiveMenu())
        {
            EventSystem.current.SetSelectedGameObject(m_firstButtonInMenus[0]);
            m_firstButtonInMenus[0].GetComponent<Slider>().colors = colourSelected;
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(null);
            m_firstButtonInMenus[0].GetComponent<Slider>().colors = naturalState;
        }

    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    private void OnDisable()
    {

        playerInput.SwitchCurrentActionMap("Player");
        if(pauseGamepad != null)
            pauseGamepad.started -= BackGamPad;
        if(m_optionsMenuActionRight != null)
        m_optionsMenuActionRight.started -= OptionsMoveRight;
        if(m_optionsMenuActionLeft != null)
        m_optionsMenuActionLeft.started -= OptionsMoveLeft;
        if (PauseMenu.Instance != null)
            PauseMenu.Instance.isPaused = false;

        gameObject.SetActive(false);
    }
    public void BackGamPad(InputAction.CallbackContext context)
    {
        playerInput.SwitchCurrentActionMap("Player");
        m_mainMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(m_firstButtonMainMenu);
        m_firstButtonMainMenu.GetComponent<Button>().colors = colourSelected;
        foreach (var item in m_menus)
        {
            item.SetActive(false);
        }
        Time.timeScale = 1;
        gameObject.SetActive(false);
    }
    public void BackToGame()
    {
        playerInput.SwitchCurrentActionMap("Player");
        m_mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(m_firstButtonMainMenu);
        m_firstButtonMainMenu.GetComponent<Button>().colors = colourSelected;
        foreach (var item in m_menus)
        {
            item.SetActive(false);
        }
        gameObject.SetActive(false);
    }
    // Update is called once per frame
    private void Update()
    {
        UpdateInput();
    }
    private void OptionsMoveLeft(InputAction.CallbackContext context)
    {

        m_menuChosen -= 1;
        if (m_menuChosen < 0)
            m_menuChosen = m_menus.Count - 1;

        SetMenu(m_menuChosen);
        SetFirstButton();
    }
    private void SetFirstButton()
    {
        if (m_menus[m_menuChosen])
        {
            EventSystem.current.SetSelectedGameObject(m_firstButtonInMenus[m_menuChosen]);
            if (m_firstButtonInMenus[m_menuChosen].GetComponent<Slider>() != null)
                m_firstButtonInMenus[m_menuChosen].GetComponent<Slider>().colors = colourSelected;
            else if (m_firstButtonInMenus[m_menuChosen].GetComponent<Button>() != null)
                m_firstButtonInMenus[m_menuChosen].GetComponent<Button>().colors = colourSelected;
            else if (m_firstButtonInMenus[m_menuChosen].GetComponent<Toggle>())
                m_firstButtonInMenus[m_menuChosen].GetComponent<Toggle>().colors = colourSelected;
        }
    }
    private void OptionsMoveRight(InputAction.CallbackContext context)
    {
        m_menuChosen += 1;
        if (m_menuChosen > m_menus.Count - 1)
            m_menuChosen = 0;
        SetMenu(m_menuChosen);
        SetFirstButton();
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
    public void BackToMainMenu(GameObject mainMenu)
    {
        foreach (var item in m_menus)
        {
            item.SetActive(false);
        }
        mainMenu.SetActive(true);
        gameObject.SetActive(false);
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
        if (CheckInput.Instance.CheckGamePadActiveMenu() && (EventSystem.current.currentSelectedGameObject == null))
        {
            if (!m_alreadySeenGamePad)
            {
                foreach (var item in m_gamePadOptions)
                {
                    item.SetActive(true);
                }
                m_alreadySeenGamePad = true;
                SetFirstButton();
            }

        }
        else if (Mouse.current.IsActuated())
        {
            if (!m_alreadySeenMouse)
            {
                foreach (var item in m_gamePadOptions)
                {
                    item.SetActive(false);
                }
                m_alreadySeenMouse = true;
            }
            if (EventSystem.current.alreadySelecting)
            {
                m_firstButtonInMenus[0].GetComponent<Slider>().colors = naturalState;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }

}
