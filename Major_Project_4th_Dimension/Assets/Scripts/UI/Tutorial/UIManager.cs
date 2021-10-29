using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    //First Room ui
    private GameObject m_keyWASDImage;
    private GameObject m_joySticks;

    //Second Room ui
    private GameObject m_pressToShoot;
    private GameObject m_zoomToLook;

    //Third Room UI
    private GameObject m_pressSpaceForScanner;
    private GameObject m_moveObjects;

    //Fourth Room UI

    private void Awake()
    {
        m_keyWASDImage = gameObject.transform.Find("Canvas").Find("WASD Image").gameObject;
        m_joySticks = gameObject.transform.Find("Canvas").Find("JoyStick").gameObject;
        m_pressToShoot = gameObject.transform.Find("Canvas").Find("Left_Click").gameObject;
        m_zoomToLook = gameObject.transform.Find("Canvas").Find("Hold_Right_Click").gameObject;
        m_pressSpaceForScanner = gameObject.transform.Find("Canvas").Find("Scanner").gameObject;
    }
    public void EnableRoomUI(bool enabled, int location)
    {
        switch (location)
        {
            //First room UI
            case 0:
                //item.SetActive(active);
                break;
            //Second Room 1st section
            case 1:
                break;
            //Second Room 2nd section
            case 2:
                break;
            //Second Room 3rd section
            case 3:
                break;
            //Third Room 1st section
            case 4:
                break;
            //Third Room 2nd section
            case 5:
                break;
            //Fourth Room 1st section
            case 6:
                break;
            //Fourth Room 2nd section
            case 7:
                break;

            default:
                break;
        }

    }
}
