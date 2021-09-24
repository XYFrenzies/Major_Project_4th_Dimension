using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
public class CheckInput : Singleton<CheckInput>
{
    public bool CheckGamePadActive()
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
}
