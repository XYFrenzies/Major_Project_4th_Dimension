using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthRoomUIMoveObjects : MonoBehaviour
{
    private GameObject m_grabObjects;
    private List<GameObject> allObjectsInUI;
    // Start is called before the first frame update
    void Start()
    {
        m_grabObjects = gameObject.transform.Find("Canvas").Find("WASD Image").gameObject;
        allObjectsInUI.Add(m_grabObjects);
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
