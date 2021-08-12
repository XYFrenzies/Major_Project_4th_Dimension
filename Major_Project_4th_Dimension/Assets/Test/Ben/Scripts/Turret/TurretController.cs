using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private float timeBeforeShot = 2.0f;
    [SerializeField] private float timePeriodOfShot = 2.0f;
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private GameObject m_player = null;
    [SerializeField] private float beamLength = 10.0f;
    [SerializeField] private Material m_neutral = null;
    [SerializeField] private Material m_attack = null;
    [SerializeField] private Material m_shooting = null;
    [SerializeField] private GameEvent m_takeDamage = null;
    [SerializeField] private LineRenderer beamRenderer;
    private Vector3 m_lastKnownLocation = Vector3.zero;
    private Quaternion m_lookRotation;
    private bool isShooting = false;
    [HideInInspector] public bool playerColliding = false;
    private bool isInRange = false;
    private bool hasChangedColour = false;
    private bool hasTakenDamage = false;
    private bool aboutToFire = false;
    private bool cRBeforeShot = false;
    private bool cRDuringShot = false;
    private void Awake()
    {
        //Will only work on the first instance in the game
        //Need to attach the player to the script.
        if (m_player == null)
        {
            m_player = GameObject.FindGameObjectWithTag("Player");
        }
    }
    // Update is called once per frame
    void Update()
    {
        //If the player is within the radius of the turret (need to have a trigger check for the player)
        if (m_player && isInRange && !playerColliding)
        {
            if (!isShooting)
            {
                //Checks if the player is in a different area.
                if (m_lastKnownLocation != m_player.transform.position)
                {
                    m_lastKnownLocation = m_player.transform.position;
                    m_lookRotation = Quaternion.LookRotation(m_lastKnownLocation - transform.position);
                }
                //Rotates to the new specified location
                if (transform.rotation != m_lookRotation)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, m_lookRotation, speed * Time.deltaTime);
                }

                if (hasChangedColour)
                {
                    beamRenderer.material = m_neutral;
                    hasChangedColour = false;
                }
            }
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
        }
        else
            beamRenderer.enabled = false;
        if (!aboutToFire && isInRange && !playerColliding)
        {
            StartCoroutine(ShootWithHitScan());
        }
        else if (((cRBeforeShot || cRDuringShot) && !isInRange) || playerColliding)
        {
            StopAllCoroutines();
            // StopCoroutine(ShootWithHitScan());
            IsNotShooting();
            aboutToFire = false;
            cRBeforeShot = false;
            cRDuringShot = false;
        }
    }
    IEnumerator ShootWithHitScan()
    {
        aboutToFire = true;
        cRBeforeShot = true;
        yield return new WaitForSeconds(timeBeforeShot);
        IsShooting();
        cRDuringShot = true;
        //Do damage to player if they are within the line renderer with raycast.hit (need to discuss if its a health system or not)
        yield return new WaitForSeconds(timePeriodOfShot);
        IsNotShooting();
        aboutToFire = false;
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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            playerColliding = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Player")
        {
            playerColliding = false;
        }
    }
}
