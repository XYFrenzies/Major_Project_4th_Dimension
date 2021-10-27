using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecondRoomUIHook : MonoBehaviour
{
    private GameObject m_pressToShoot;
    private GameObject m_zoomToLook;
    private List<GameObject> allObjectsInUI;
    // Start is called before the first frame update
    void Start()
    {
        m_pressToShoot = gameObject.transform.Find("Canvas").Find("Left_Click").gameObject;
        m_zoomToLook = gameObject.transform.Find("Canvas").Find("Hold_Right_Click").gameObject;
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
