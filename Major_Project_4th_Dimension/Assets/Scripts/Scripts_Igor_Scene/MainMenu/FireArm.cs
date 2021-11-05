using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArm : MonoBehaviour
{
    //public bool isArmOn = false;
    public LineRenderer lineRenderer;

    public void FireTheArm()
    {
        //isArmOn = true;
        lineRenderer.enabled = true;
    }
}
