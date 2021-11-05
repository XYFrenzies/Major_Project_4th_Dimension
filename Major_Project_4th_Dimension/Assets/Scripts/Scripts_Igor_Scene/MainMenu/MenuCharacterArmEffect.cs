using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCharacterArmEffect : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform firePoint;
    public Transform target;
    //public FireArm arm;


    private void Update()
    {
        if (lineRenderer.enabled)
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, target.position);

        }
    }
}
