using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] private GameEvent m_restartLevel;
    [SerializeField] private GameEvent m_deathAni;
    [SerializeField] private GameEvent m_noDeathAni;
    [SerializeField] private float m_timeBeforeDeath;
    private void Awake()
    {
        m_noDeathAni.Raise();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(StartRestartProcess());
            m_deathAni.Raise();
        }
    }
    private IEnumerator StartRestartProcess() 
    {
        yield return new WaitForSeconds(m_timeBeforeDeath);
        m_restartLevel.Raise();
    }
}
