using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrapplePoint : MonoBehaviour
{
    Transform cam;
    Material mat;
    PlayerController playerCont;
    public int id;
    public Material materialOn;
    public Material materialOff;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
        cam = Camera.main.transform;

    }

    private void Start()
    {
        GameEvents.current.onGrapplePointVisible += TurnPointOn;
        GameEvents.current.onGrapplePointNotVisible += TurnPointOff;
    }

    private void Update()
    {

    }

    public void TurnPointOn(int id)
    {
        if (id == this.id)
        {
            mat.color = materialOn.color;
        }
    }

    public void TurnPointOff()
    {
        
            mat.color = materialOff.color;
     
    }

}
