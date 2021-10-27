using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FourthRoomUIPullObjects : MonoBehaviour
{
    private GameObject m_pullObjects;
    private List<GameObject> allObjectsInUI;
    // Start is called before the first frame update
    void Start()
    {
        m_pullObjects = gameObject.transform.Find("Canvas").Find("Pull Objects").gameObject;
        allObjectsInUI.Add(m_pullObjects);
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
