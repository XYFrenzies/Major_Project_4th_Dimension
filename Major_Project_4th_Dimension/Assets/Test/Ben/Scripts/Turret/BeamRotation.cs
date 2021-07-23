using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamRotation : Singleton<BeamRotation>
{
    [SerializeField] private float beamLength = 10.0f;
    [SerializeField] private Material m_neutral = null;
    [SerializeField] private Material m_attack = null;
    [SerializeField] private Material m_shooting = null;
    [SerializeField] private GameEvent m_takeDamage = null;
    [HideInInspector] public bool isInRange = false;
    private LineRenderer renderer;
    private GameObject m_player;
    private bool hasChangedColour = false;
    private bool isShooting = false;
    private bool hasTakenDamage = false;
    //This is for damaging the player.
    //[SerializeField] private GameObject m_player = null;
    //[SerializeField] private float damage = 1.0f;
    private void Awake()
    {
        renderer = GetComponent<LineRenderer>();
        m_player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    private void Update()
    {
        if (isInRange)
        {
            renderer.enabled = true;
            renderer.SetPosition(0, transform.position);
            renderer.SetPosition(1, transform.position + (transform.forward * beamLength));
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, beamLength) && hit.transform.gameObject == m_player)
            {
                if (isShooting && !hasTakenDamage)
                {
                    m_takeDamage.Raise();
                    hasTakenDamage = true;
                    return;
                }
                renderer.material = m_attack;
                hasChangedColour = true;
            }
            else if (hasChangedColour)
            {
                renderer.material = m_neutral;
                hasChangedColour = false;
                return;
            }
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
    public void IsShooting()
    {
        renderer.material = m_shooting;
        isShooting = true;
    }
    public void IsNotShooting()
    {
        renderer.material = m_neutral;
        isShooting = false;
        hasTakenDamage = false;
    }
}
