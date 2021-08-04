using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private GameObject m_player = null;
    private Vector3 m_lastKnownLocation = Vector3.zero;
    private Quaternion m_lookRotation;
    private bool isShooting = false;
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
        if (m_player && !isShooting && BeamRotation.Instance.isInRange)
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
        }
    }

    public void TurretIsShooting()
    {
        isShooting = true;
    }
    public void TurretStoppedShooting()
    {
        isShooting = false;
    }
}
