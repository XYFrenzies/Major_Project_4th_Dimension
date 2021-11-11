using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireArm : MonoBehaviour
{
    public LineRenderer lineRenderer;

    public void FireTheArm()
    {
        lineRenderer.enabled = true;
    }
}
