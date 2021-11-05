using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            m_uiInteract.SetActive(true);
            Debug.Log("Player inside trigger area");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            outsideTriggerArea.Raise();
            m_uiInteract.SetActive(false);
            Debug.Log("Player outside trigger area");

        }
    }
}
