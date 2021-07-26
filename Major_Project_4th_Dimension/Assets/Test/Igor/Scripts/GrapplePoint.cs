using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplePoint : MonoBehaviour
{
    Transform cam;
    Material mat;
    SphereCollider col;
    public float grapplePointRadius = 50f;
    public GameObject indicator;
    public Material materialOn;
    public Material materialOff;

    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
        cam = Camera.main.transform;
        col = GetComponent<SphereCollider>();
    }

    private void Start()
    {
        col.radius = grapplePointRadius;

    }

    private void Update()
    {
        transform.LookAt(transform.position + cam.forward);

    }

    public void TurnPointOn()
    {
        mat.color = materialOn.color;
        indicator.SetActive(true);
    }

    public void TurnPointOff()
    {
        mat.color = materialOff.color;
        indicator.SetActive(false);

    }

    private void OnTriggerEnter(Collider other)
    {
        TurnPointOn();
    }

    private void OnTriggerExit(Collider other)
    {
        TurnPointOff();
    }
}
