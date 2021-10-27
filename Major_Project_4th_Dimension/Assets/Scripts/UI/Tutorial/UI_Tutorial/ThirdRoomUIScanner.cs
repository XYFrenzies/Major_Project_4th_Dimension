using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdRoomUIScanner : MonoBehaviour
{
    private GameObject m_keyWASDImage;
    private GameObject m_joySticks;
    private GameObject m_lookAroundText;
    private GameObject m_pressToShoot;
    private GameObject m_zoomToLook;
    private List<GameObject> allObjectsInUI;
    // Start is called before the first frame update
    void Start()
    {
        m_keyWASDImage = gameObject.transform.Find("Canvas").Find("WASD Image").gameObject;
        m_joySticks = gameObject.transform.Find("Canvas").Find("JoyStick").gameObject;
        m_lookAroundText = gameObject.transform.Find("Canvas").Find("Look Around").gameObject;
        m_pressToShoot = gameObject.transform.Find("Canvas").Find("Left_Click").gameObject;
        m_zoomToLook = gameObject.transform.Find("Canvas").Find("Hold_Right_Click").gameObject;
        allObjectsInUI.Add(m_keyWASDImage);
        allObjectsInUI.Add(m_joySticks);
        allObjectsInUI.Add(m_lookAroundText);
        allObjectsInUI.Add(m_pressToShoot);
        allObjectsInUI.Add(m_zoomToLook);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            UIManager.Instance.EnableObjects(true, allObjectsInUI);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.EnableObjects(false, allObjectsInUI);
        }
    }
}
