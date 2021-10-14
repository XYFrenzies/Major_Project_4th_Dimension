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
    private bool isOn = false;
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
        ChangeInterfaceType();
        isOn = true;
    }
    private void Update()
    {
        FindDirection(m_interfaceHookPC, GameObject.FindGameObjectWithTag("Player"));
        FindDirection(m_interfaceHookXbox, GameObject.FindGameObjectWithTag("Player"));
        if (isOn)
        {
            ChangeInterfaceType();
        }
    }
    public void TurnPointOff()
    {
        mat.color = materialOff.color;
        m_interfaceHookPC.SetActive(false);
        m_interfaceHookXbox.SetActive(false);
        isOn = false;
    }
    private void ChangeInterfaceType()
    {
        if (CheckInput.Instance.CheckMouseActive())
        {
            m_interfaceHookPC.SetActive(true);
            m_interfaceHookXbox.SetActive(false);
        }
        else if (CheckInput.Instance.CheckGamePadActiveGame())
        {
            m_interfaceHookXbox.SetActive(true);
            m_interfaceHookPC.SetActive(false);
        }
    }
    private void FindDirection(GameObject obj, GameObject player) 
    {
        Vector3 dir = obj.transform.position - player.transform.position;
        Quaternion lookDir = Quaternion.LookRotation(dir);
        obj.transform.rotation = lookDir;
    }
}
