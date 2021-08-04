using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InLocation : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            WallTurretController.Instance.playerInArea = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            WallTurretController.Instance.playerInArea = false;
        }
    }
}
