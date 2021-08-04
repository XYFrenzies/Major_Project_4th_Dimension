using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightRotation : MonoBehaviour
{
    [SerializeField] private GameEvent m_takeDamage = null;
    [HideInInspector] public bool isInRange = false;
    private Light m_spotLight;
    private GameObject m_player;
    private bool isShooting = false;
    private bool hasTakenDamage = false;
    // Start is called before the first frame update
    private void Awake()
    {
        m_spotLight = GetComponent<Light>();
        m_player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (isInRange)
        {
            RaycastHit hit;
            if (Physics.Raycast(m_spotLight.gameObject.transform.position, m_spotLight.gameObject.transform.forward, out hit)
                && hit.transform.gameObject == m_player && isShooting && !hasTakenDamage)
            {
                m_takeDamage.Raise();
            }
        }
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
    public void IsShooting()
    {
        //Different colour of the spotlight here
        isShooting = true;
    }
    public void IsNotShooting()
    {
        //Different colour of the spotlight here
        isShooting = false;
        hasTakenDamage = false;
    }
}
