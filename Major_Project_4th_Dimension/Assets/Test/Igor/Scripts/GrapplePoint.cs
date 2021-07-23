using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    Material mat;
    public Material materialOn;
    public Material materialOff;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }

    public void TurnPointOn()
    {
        mat.color = materialOn.color;
        
    }

    public void TurnPointOff()
    {
        mat.color = materialOff.color;
        

    }
}
