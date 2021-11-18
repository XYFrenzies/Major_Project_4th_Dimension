using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TriggerArea : MonoBehaviour
{

    public GameEvent insideTriggerArea;
    public GameEvent outsideTriggerArea;
    [SerializeField] private GameObject m_uiInteract;
    private void OnTriggerEnter(Collider other)
    {
        if ((other.CompareTag("Player") && gameObject.CompareTag("Power")) || (other.CompareTag("Player") && gameObject.CompareTag("Conveyor Belt") && PowerStatus.Instance.powerIsOn))
        {
            insideTriggerArea.Raise();
            m_uiInteract.gameObject.SetActive(true);
            if (CheckInput.Instance.CheckGamePadActiveGame())
                m_uiInteract.GetComponent<Text>().text = "";
            else
                m_uiInteract.GetComponent<Text>().text = "";
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
                m_uiInteract.GetComponent<Text>().text = "";
            else
                m_uiInteract.GetComponent<Text>().text = "";
            Debug.Log("Player outside trigger area");

        }
    }
}
