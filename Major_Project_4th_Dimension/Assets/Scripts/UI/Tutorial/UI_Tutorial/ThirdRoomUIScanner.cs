using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdRoomUIScanner : MonoBehaviour
{
    private GameObject m_pressSpaceForScanner;
    private List<GameObject> allObjectsInUI;
    // Start is called before the first frame update
    void Start()
    {
        m_pressSpaceForScanner = gameObject.transform.Find("Canvas").Find("Scanner").gameObject;
        allObjectsInUI.Add(m_pressSpaceForScanner);
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
