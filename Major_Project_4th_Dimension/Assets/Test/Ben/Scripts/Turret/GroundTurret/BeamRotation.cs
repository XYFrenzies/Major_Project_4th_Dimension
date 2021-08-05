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
    private LineRenderer beamRenderer;
    private GameObject m_player;
    private bool hasChangedColour = false;
    private bool isShooting = false;
    private bool hasTakenDamage = false;
    //This is for damaging the player.
    //[SerializeField] private GameObject m_player = null;
    //[SerializeField] private float damage = 1.0f;
    private void Awake()
    {
        beamRenderer = GetComponent<LineRenderer>();
        m_player = GameObject.FindGameObjectWithTag("Player");
    }
    // Update is called once per frame
    private void Update()
    {
        if (isInRange && !TurretController.Instance.playerColliding)
        {
            beamRenderer.enabled = true;
            beamRenderer.SetPosition(0, transform.position);
            beamRenderer.SetPosition(1, transform.position + (transform.forward * beamLength));
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, beamLength) && hit.transform.gameObject == m_player)
            {
                if (isShooting && !hasTakenDamage)
                {
                    m_takeDamage.Raise();
                    hasTakenDamage = true;
                    return;
                }
                else if (!isShooting)
                {
                    beamRenderer.material = m_attack;
                    hasChangedColour = true;
                }
            }
            else if (hasChangedColour && !isShooting)
            {
                beamRenderer.material = m_neutral;
                hasChangedColour = false;
                return;
            }
        }
        else
            beamRenderer.enabled = false;
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
        beamRenderer.material = m_shooting;
        isShooting = true;
    }
    public void IsNotShooting()
    {
        beamRenderer.material = m_neutral;
        isShooting = false;
        hasTakenDamage = false;
    }
}
