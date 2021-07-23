using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float timeBeforeShot = 2.0f;
    [SerializeField] private float timePeriodOfShot = 2.0f;
    [SerializeField] private bool isHitScan = true;
    [SerializeField] private GameEvent shooting;
    [SerializeField] private GameEvent stoppedShooting;
    private bool aboutToFire = false;
    private bool cRBeforeShot = false;
    private bool cRDuringShot = false;
    // Update is called once per frame
    void Update()
    {
        if (!aboutToFire && BeamRotation.Instance.isInRange)
        {
            StartCoroutine(ShootWithHitScan());
        }
        else if ((cRBeforeShot || cRDuringShot) && !BeamRotation.Instance.isInRange)
        {
            StopCoroutine(ShootWithHitScan());
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
}
