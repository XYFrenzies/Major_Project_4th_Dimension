using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class CheckInput : Singleton<CheckInput>
{
    private bool m_mouseIsActive = false;
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
    public bool CheckGamePadActiveGame()
    {
        if (Gamepad.current != null && (Gamepad.current.IsActuated() || !m_mouseIsActive) && !Mouse.current.leftButton.IsActuated() && !Mouse.current.rightButton.IsActuated())
        {
            m_mouseIsActive = false;
            return true;
        }
        return false;
    }
    public bool CheckMouseActive()
    {
        if (Mouse.current != null && (Mouse.current.IsActuated() || m_mouseIsActive) && !Gamepad.current.leftStick.IsActuated() && !Gamepad.current.rightStick.IsActuated())
        {
            m_mouseIsActive = true;
            return true;
        }
        return false;
    }
}
