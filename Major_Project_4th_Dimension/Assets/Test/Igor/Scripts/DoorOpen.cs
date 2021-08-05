using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpen : MonoBehaviour
{

    public GameEvent OpenDoor;
    public GameEvent CloseDoor;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BigPullObject") || other.CompareTag("MoveableToMe"))
        {
            OpenDoor.Raise();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BigPullObject") || other.CompareTag("MoveableToMe"))
        {
            CloseDoor.Raise();
        }
    }
}
