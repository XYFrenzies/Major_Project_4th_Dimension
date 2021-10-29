using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomUI : MonoBehaviour
{
    [SerializeField] private int m_location;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            UIManager.Instance.EnableRoomUI(true, m_location);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            UIManager.Instance.EnableRoomUI(false, m_location);
        }
    }
}
