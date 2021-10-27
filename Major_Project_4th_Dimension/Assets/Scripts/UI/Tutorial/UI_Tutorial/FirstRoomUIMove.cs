using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstRoomUIMove : MonoBehaviour
{
    private GameObject m_keyWASDImage;
    private GameObject m_joySticks;
    private List<GameObject> allObjectsInUI;
    // Start is called before the first frame update
    void Start()
    {
        m_keyWASDImage = gameObject.transform.Find("Canvas").Find("WASD Image").gameObject;
        m_joySticks = gameObject.transform.Find("Canvas").Find("JoyStick").gameObject;
        allObjectsInUI.Add(m_keyWASDImage);
        allObjectsInUI.Add(m_joySticks);
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
