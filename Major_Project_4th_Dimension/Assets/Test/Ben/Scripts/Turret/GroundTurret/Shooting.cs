using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : Singleton<Shooting>
{
    [SerializeField] private float timeBeforeShot = 2.0f;
    [SerializeField] private float timePeriodOfShot = 2.0f;
    [SerializeField] private GameEvent shooting;
    [SerializeField] private GameEvent stoppedShooting;
    private bool aboutToFire = false;
    private bool cRBeforeShot = false;
    private bool cRDuringShot = false;
    // Update is called once per frame
    void Update()
    {
        if (!aboutToFire && BeamRotation.Instance.isInRange && !TurretController.Instance.playerColliding)
        {
            StartCoroutine(ShootWithHitScan());
        }
        else if (((cRBeforeShot || cRDuringShot) && !BeamRotation.Instance.isInRange) || TurretController.Instance.playerColliding)
        {
            StopAllCoroutines();
           // StopCoroutine(ShootWithHitScan());
            stoppedShooting.Raise();
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
        shooting.Raise();
        cRDuringShot = true;
        //Do damage to player if they are within the line renderer with raycast.hit (need to discuss if its a health system or not)
        yield return new WaitForSeconds(timePeriodOfShot);
        stoppedShooting.Raise();
        aboutToFire = false;
    }
    public void Shoot() 
    {
        aboutToFire = true;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == TurretController.Instance.m_player)
        {
            TurretController.Instance.playerColliding = true;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject == TurretController.Instance.m_player)
        {
            TurretController.Instance.playerColliding = false;
        }
    }
}
