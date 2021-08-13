using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// In the future, if another script is to be used for the switch conveyor belt gameobject
/// move this code into it.
/// All this is doing is just triggering the event as soon as something has collided with the object.
/// </summary>
public class SwitchConveyorBelt : MonoBehaviour
{
    [SerializeField] private GameEvent changeDirection;
    private void OnTriggerEnter()
    {
        changeDirection.Raise();
    }
}
