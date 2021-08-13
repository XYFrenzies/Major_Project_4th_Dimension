using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleExample : MonoBehaviour
{
    private bool doorIsToOpen = false;
    [SerializeField] private GameObject door;
    private void Update()
    {
        if (doorIsToOpen)
        {
            door.transform.position = Vector3.Lerp(door.transform.position, new Vector3(door.transform.position.x, 30, door.transform.position.z), Time.deltaTime);
        }

    }
    public void OpenDoor() 
    {
        doorIsToOpen = true;
    }
}
