using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorOpen : MonoBehaviour
{

    public GameEvent OpenDoor;
    public GameEvent CloseDoor;
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BigPullObject") || other.CompareTag("MoveableToMe") || other.CompareTag("Player"))
        {
            if ((OpenDoor != null && SceneManager.GetActiveScene().name == "Level_01") || (OpenDoor != null && (SceneManager.GetActiveScene().name == "Level_02" || SceneManager.GetActiveScene().name == "Level_03") && PowerStatus.Instance.powerIsOn))
                OpenDoor.Raise();
        }

    }

    //Not too sure if the power is turned off if the door closes or not.
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BigPullObject") || other.CompareTag("MoveableToMe") || other.CompareTag("Player")) //&& PowerStatus.Instance.powerIsOn 
        {
            if (CloseDoor != null)
                CloseDoor.Raise();
        }
    }
}
