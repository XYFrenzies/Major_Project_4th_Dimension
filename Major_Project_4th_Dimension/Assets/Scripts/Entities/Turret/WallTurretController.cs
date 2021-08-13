using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTurretController : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private GameObject m_player = null;
    [SerializeField] private Light m_spotLight;
    [SerializeField] private GameEvent m_takeDamage = null;
    [SerializeField] private float timeBeforeShot = 2.0f;
    [SerializeField] private float timePeriodOfShot = 2.0f;
    private Vector3 m_lastKnownLocation = Vector3.zero;
    private Quaternion m_lookRotation;
    [HideInInspector]public bool playerInArea = false;
    private bool isShooting = false;
    private Color currentColourLight;
    private float spotLightRCRadius = 1.5f;
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
        currentColourLight = m_spotLight.color;
    }
    // Update is called once per frame
    void Update()
    {
        //If the player is within the radius of the turret (need to have a trigger check for the player)
        if (m_player && !isShooting && playerInArea)
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
            RaycastHit hit;
            if (Physics.SphereCast(m_spotLight.gameObject.transform.position, spotLightRCRadius, m_spotLight.gameObject.transform.forward, out hit)
                && hit.transform.gameObject == m_player && !hasTakenDamage && !isShooting)
            {
                aboutToFire = true;
                m_takeDamage.Raise();
            }
            if (aboutToFire)
            {
                StartCoroutine(ShootWithHitScan());
                isShooting = true;
            }
            else if ((cRBeforeShot || cRDuringShot) && !playerInArea)
            {
                StopAllCoroutines();
                IsNotShooting();
                aboutToFire = false;
                cRBeforeShot = false;
                cRDuringShot = false;
            }
        }
        //We can have an else statement as a randomly generated path for the light to follow until the player is in the area.
    }
    IEnumerator ShootWithHitScan()
    {
        aboutToFire = false;
        cRBeforeShot = true;
        yield return new WaitForSeconds(timeBeforeShot);
        cRDuringShot = true;
        IsFiring();
        //Do damage to player if they are within the line renderer with raycast.hit (need to discuss if its a health system or not)
        yield return new WaitForSeconds(timePeriodOfShot);
        IsNotShooting();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInArea = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInArea = false;
        }
    }

    public void IsFiring()
    {
        isShooting = true;
        //Different colour of the spotlight here
        m_spotLight.color = Color.red;
    }
    public void IsNotShooting()
    {
        isShooting = false;
        //Different colour of the spotlight here
        m_spotLight.color = currentColourLight;
        hasTakenDamage = false;
    }

}
