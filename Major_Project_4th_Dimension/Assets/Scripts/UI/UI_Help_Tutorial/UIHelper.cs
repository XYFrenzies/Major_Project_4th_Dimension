using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIHelper : MonoBehaviour
{
    [SerializeField] private GameObject m_startingArea;
    private GameObject m_keyWASDImage;
    private GameObject m_joySticks;
    private GameObject m_lookAroundText;
    private GameObject m_pressToShoot;
    private GameObject m_zoomToLook;
    // Start is called before the first frame update
    void Start()
    {
        m_keyWASDImage = gameObject.transform.Find("Canvas").Find("WASD Image").gameObject;
        m_joySticks = gameObject.transform.Find("Canvas").Find("JoyStick").gameObject;
        m_lookAroundText = gameObject.transform.Find("Canvas").Find("Look Around").gameObject;
        m_pressToShoot = gameObject.transform.Find("Canvas").Find("Left_Click").gameObject;
        m_zoomToLook = gameObject.transform.Find("Canvas").Find("Hold_Right_Click").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
