using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    #region Variables
    //First Room ui
    private GameObject m_keyWASDImage;
    private GameObject m_joySticks;

    //Second Room ui
    private GameObject m_pressToShootClick;
    private GameObject m_zoomToLookClick;
    private GameObject m_pressToShootRT;
    private GameObject m_zoomToLookLT;

    //Third Room UI
    private GameObject m_pressSpaceForScanner;
    private GameObject m_pressRBForScanner;
    private GameObject m_moveObjects;

    //Fourth Room UI
    private GameObject m_smallObjectsPC;
    private GameObject m_smallObjectsXbox;
    private GameObject m_releasePC;
    private GameObject m_releaseXbox;
    #endregion
    private void Awake()
    {
        //First room
        m_keyWASDImage = gameObject.transform.Find("Canvas").Find("PC UI").Find("WASD Move").gameObject;
        m_joySticks = gameObject.transform.Find("Canvas").Find("Console UI").Find("JoySticks").gameObject;
        m_keyWASDImage.SetActive(false);
        m_joySticks.SetActive(false);

        //Second room
        m_pressToShootClick = gameObject.transform.Find("Canvas").Find("PC UI").Find("Right Click").gameObject;
        m_zoomToLookClick = gameObject.transform.Find("Canvas").Find("PC UI").Find("Left Click").gameObject;
        m_pressToShootRT = gameObject.transform.Find("Canvas").Find("Console UI").Find("RT to shoot").gameObject;
        m_zoomToLookLT = gameObject.transform.Find("Canvas").Find("Console UI").Find("LT to zoom").gameObject;
        m_pressToShootClick.SetActive(false);
        m_zoomToLookClick.SetActive(false);
        m_pressToShootRT.SetActive(false);
        m_zoomToLookLT.SetActive(false);

        //Third room
        m_pressSpaceForScanner = gameObject.transform.Find("Canvas").Find("PC UI").Find("Scanner Effect").gameObject;
        m_pressRBForScanner = gameObject.transform.Find("Canvas").Find("Console UI").Find("Rb scanner effect").gameObject;
        m_moveObjects = gameObject.transform.Find("Canvas").Find("Universal").Find("MoveObject").gameObject;
        m_pressSpaceForScanner.SetActive(false);
        m_pressRBForScanner.SetActive(false);
        m_moveObjects.SetActive(false);

        //Fourth room
        m_smallObjectsPC = gameObject.transform.Find("Canvas").Find("PC UI").Find("SmallerObjects").gameObject;
        m_smallObjectsXbox = gameObject.transform.Find("Canvas").Find("Console UI").Find("SmallerObjects gamepad").gameObject;
        m_releasePC = gameObject.transform.Find("Canvas").Find("PC UI").Find("Release").gameObject;
        m_releaseXbox = gameObject.transform.Find("Canvas").Find("Console UI").Find("Release gamepad").gameObject;
        m_smallObjectsPC.SetActive(false);
        m_smallObjectsXbox.SetActive(false);
        m_releasePC.SetActive(false);
        m_releaseXbox.SetActive(false);
    }
    public void EnableRoomUI(bool enabled, int location)
    {
        bool mouseInput = CheckInput.Instance.CheckMouseActive();
        switch (location)
        {
            //First room UI
            case 0:
                if (mouseInput)
                    m_keyWASDImage.SetActive(enabled);
                else
                    m_joySticks.SetActive(enabled);
                break;
            //Second Room 1st section
            case 1:
                if (mouseInput)
                {
                    m_pressToShootClick.SetActive(enabled);
                    m_zoomToLookClick.SetActive(enabled);
                }
                else
                {
                    m_pressToShootRT.SetActive(enabled);
                    m_zoomToLookLT.SetActive(enabled);
                }
                break;
            //Third Room 1st section
            case 2:
                if (mouseInput)
                    m_pressSpaceForScanner.SetActive(enabled);
                else
                    m_pressRBForScanner.SetActive(enabled);
                break;
            //Third Room 2nd section
            case 3:
                m_moveObjects.SetActive(enabled);
                break;
            //Fourth Room 1st section
            case 4:
                if (mouseInput)
                    m_smallObjectsPC.SetActive(enabled);
                else
                    m_smallObjectsXbox.SetActive(enabled);
                break;
            //Fourth Room 2nd section
            case 5:
                if (mouseInput)
                    m_releasePC.SetActive(enabled);
                else
                    m_releaseXbox.SetActive(enabled);
                break;

            default:
                Debug.Log("Out of scope!!!");
                break;
        }

    }
    public void EnableThisObject(bool enabled)
    { 
        gameObject.SetActive(enabled);
    }
}
