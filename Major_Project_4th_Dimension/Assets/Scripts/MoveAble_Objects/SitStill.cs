using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SitStill : MonoBehaviour
{
    [SerializeField] private float m_timeBeforeFreeze = 2.0f;
    private void OnTriggerEnter(Collider other)
    {
        if((other.CompareTag("BigPullObject") || other.CompareTag("MoveableToMe")) && other.GetComponent<Rigidbody>() && other.GetComponent<Rigidbody>().constraints != RigidbodyConstraints.FreezeRotation)
            StartCoroutine(Timer(other));
    }
    private IEnumerator Timer(Collider col) 
    {
        yield return new WaitForSeconds(m_timeBeforeFreeze);
        col.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }
}
