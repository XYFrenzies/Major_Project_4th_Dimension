using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxSparkScript : MonoBehaviour
{
    public GameObject objectToEnable;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Environment")
        {
            objectToEnable.SetActive(true);
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    objectToEnable.SetActive(false);
    //}
}
