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
            if ((OpenDoor != null && SceneManager.GetActiveScene().name == "Tutorial_Corridor") || (OpenDoor != null && (SceneManager.GetActiveScene().name == "Final_Level" || SceneManager.GetActiveScene().name == "Actual_Final_Level_Probably") && PowerStatus.Instance.powerIsOn))
                OpenDoor.Raise();
        }
        else
        {
            if (CloseDoor != null)
                CloseDoor.Raise();
        }
    }

//Not too sure if the power is turned off if the door closes or not.
    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("BigPullObject") || other.CompareTag("MoveableToMe") || other.CompareTag("Player")) //&& PowerStatus.Instance.powerIsOn 
    //    {
    //        if (CloseDoor != null)
    //            CloseDoor.Raise();
    //    }
    //}
}
