using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

    public GameEvent OpenDoor;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BigPullObject") || other.CompareTag("MoveableToMe"))
        {
            OpenDoor.Raise();
        }
    }
}
