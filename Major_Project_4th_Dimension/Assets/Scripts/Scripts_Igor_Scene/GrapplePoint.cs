using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GrapplePoint : MonoBehaviour
{
    Material mat;
    public int id;
    public Material materialOn;
    public Material materialOff;
    [SerializeField] private GameObject m_interfaceHookPC;
    [SerializeField] private GameObject m_interfaceHookXbox;
    private void Awake()
    {
        mat = GetComponent<Renderer>().material;
    }

    private void Start()
    {
        GameEvents.current.onGrapplePointVisible += TurnPointOn;
        GameEvents.current.onGrapplePointNotVisible += TurnPointOff;
    }
    public void TurnPointOn(int id)
    {
        if (id == this.id)
            mat.color = materialOn.color;
        m_interfaceHookPC.SetActive(true);
    }
    private void Update()
    {
        m_interfaceHookPC.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
        m_interfaceHookXbox.transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform);
    }
    public void TurnPointOff()
    {
            mat.color = materialOff.color;
        m_interfaceHookPC.SetActive(false);
    }
}
