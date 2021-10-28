using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Setting the trigger for opening the door to occur.
/// </summary>
public class EnteringArea : MonoBehaviour
{
    private bool enteredOnce = false;
    [SerializeField] private GameEvent openDoor;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MoveableToMe" && !enteredOnce)
        {
            openDoor.Raise();
            enteredOnce = true;
        }

    }
}
