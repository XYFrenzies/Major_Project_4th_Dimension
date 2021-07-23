using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamRotation : Singleton<BeamRotation>
{
    [SerializeField] private float beamLength = 10.0f;

    [HideInInspector] public bool isInRange = false;
    private LineRenderer renderer;
    //This is for damaging the player.
    //[SerializeField] private GameObject m_player = null;
    //[SerializeField] private float damage = 1.0f;
    private void Awake()
    {
        renderer = GetComponent<LineRenderer>();
    }
    // Update is called once per frame
    private void Update()
    {

        if (isInRange)
        {
            renderer.enabled = true;
            renderer.SetPosition(0, transform.position);
            renderer.SetPosition(1, transform.position + (transform.forward * beamLength));
        }
        else
            renderer.enabled = false;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            isInRange = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isInRange = false;
        }
    }
}
