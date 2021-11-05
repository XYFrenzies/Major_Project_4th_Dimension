using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoorOasis : MonoBehaviour
{
    [SerializeField] private GameEvent m_openDoor;
    private void OnTriggerEnter(Collider other)
    {
        m_openDoor.Raise();
    }
}
