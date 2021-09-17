using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject m_firstButton;
    private ColorBlock colourSelected;
    private ColorBlock naturalState;
    private bool m_gamePadActive = false;
    // Start is called before the first frame update
    void Awake()
    {
        colourSelected.colorMultiplier = 1;
        colourSelected.selectedColor = new Color(0, 1, 0.117f, 1);
        colourSelected.normalColor = new Color(1, 1, 1, 1);
        colourSelected.highlightedColor = new Color(0, 1, 0.117f, 1);

        naturalState.colorMultiplier = 1;
        naturalState.highlightedColor = new Color(0, 1, 0.117f, 1);
        naturalState.selectedColor = new Color(0, 1, 1, 1);
        naturalState.normalColor = new Color(1, 1, 1, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Gamepad.current != null && Gamepad.current.leftStick.IsActuated() && (EventSystem.current.currentSelectedGameObject == null || m_gamePadActive))
        {
            if (m_firstButton != null && m_firstButton.activeSelf)
            {
                EventSystem.current.SetSelectedGameObject(m_firstButton);
                m_firstButton.GetComponent<Button>().colors = colourSelected;
            }
            m_gamePadActive = true;
        }
        else if (Mouse.current.IsActuated())
        {
            if (Gamepad.current != null && !Gamepad.current.leftStick.IsActuated())
                m_gamePadActive = false;
            if (EventSystem.current.alreadySelecting)
            {
                m_firstButton.GetComponent<Button>().colors = naturalState;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }
}
