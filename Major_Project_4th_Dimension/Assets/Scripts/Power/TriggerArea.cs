using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerArea : MonoBehaviour
{
    public GameEvent insideTriggerArea;
    public GameEvent outsideTriggerArea;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            insideTriggerArea.Raise();
            Debug.Log("Player inside trigger area");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            outsideTriggerArea.Raise();
            Debug.Log("Player outside trigger area");

        }
    }
}
