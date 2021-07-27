using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrapplePoint : MonoBehaviour
{
    Transform cam;
    Material mat;
    PlayerController playerCont;
    //SphereCollider col;
    //public float grapplePointRadius = 50f;
    public int id;
    public GameObject indicator;
    public Material materialOn;
    public Material materialOff;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
        cam = Camera.main.transform;

        //if (playerCont == null)
        //    playerCont = GameObject.FindObjectOfType<PlayerController>();
        //if (playerCont.canSeeGrapplePoint == null)
        //    playerCont.canSeeGrapplePoint = new UnityEvent();
        //if (playerCont.notSeeGrapplePoint == null)
        //    playerCont.notSeeGrapplePoint = new UnityEvent();

        //playerCont.canSeeGrapplePoint.AddListener(TurnPointOn);
        //playerCont.notSeeGrapplePoint.AddListener(TurnPointOff);
        //col = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        //col.radius = grapplePointRadius;
        GameEvents.current.onGrapplePointVisible += TurnPointOn;
        GameEvents.current.onGrapplePointNotVisible += TurnPointOff;
    }

    private void Update()
    {
        transform.LookAt(transform.position + cam.forward);

    }

    public void TurnPointOn(int id)
    {
        if (id == this.id)
        {
            mat.color = materialOn.color;
            indicator.SetActive(true);
        }
    }

    public void TurnPointOff()
    {
        
            mat.color = materialOff.color;
            indicator.SetActive(false);
     
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //        TurnPointOn();
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //        TurnPointOff();
    //}
}
