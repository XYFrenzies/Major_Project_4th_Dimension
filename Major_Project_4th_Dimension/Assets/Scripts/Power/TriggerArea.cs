using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TriggerArea : MonoBehaviour
{

    public GameEvent insideTriggerArea;
    public GameEvent outsideTriggerArea;
    [SerializeField] private GameObject m_uiInteract;
    [SerializeField] private string m_controllerUI = "Press X to Interact";
    [SerializeField] private string m_pcUI = "Press E to Interact";
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") && gameObject.CompareTag("Power")) || (other.CompareTag("Player") && gameObject.CompareTag("Conveyor Belt") && PowerStatus.Instance.powerIsOn))
        {
            insideTriggerArea.Raise();
            if(!PlayerStateManager.player.conveyorPressed || gameObject.name != "CBSwitch")
                m_uiInteract.gameObject.SetActive(true);
            if (CheckInput.Instance.CheckGamePadActiveGame())
                m_uiInteract.GetComponent<Text>().text = m_controllerUI;
            else
                m_uiInteract.GetComponent<Text>().text = m_pcUI;
            Debug.Log("Player inside trigger area");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outsideTriggerArea.Raise();
            m_uiInteract.gameObject.SetActive(false);
            if (CheckInput.Instance.CheckGamePadActiveGame())
                m_uiInteract.GetComponent<Text>().text = m_controllerUI;
            else
                m_uiInteract.GetComponent<Text>().text = m_pcUI;
            Debug.Log("Player outside trigger area");

        }
    }
}
