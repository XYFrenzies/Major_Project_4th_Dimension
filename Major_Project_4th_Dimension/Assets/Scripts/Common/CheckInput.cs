using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class CheckInput : Singleton<CheckInput>
{

    private bool m_mouseIsActive = true;
    public bool CheckGamePadActiveMenu()
    {
        if (Gamepad.current != null && Gamepad.current.leftStick.IsActuated() && (EventSystem.current.currentSelectedGameObject == null))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SetController()
    {
        if(Gamepad.current != null)
            m_mouseIsActive = false;
    }
    public void SetMouse()
    {
        if (Mouse.current != null)
            m_mouseIsActive = true;
    }
    public bool CheckGamePadActiveGame()
    {
        return !m_mouseIsActive;
    }
    public bool CheckMouseActive()
    {
        return m_mouseIsActive;
    }
}
