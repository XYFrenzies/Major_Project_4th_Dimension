using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// This is an extra class incase we want to personalise the wall turrets shooting ability 
/// in comparison to the regular shooting.
/// </summary>
public class WallTurShoot : Singleton<WallTurShoot>
{
    [SerializeField] private float timeBeforeShot = 2.0f;
    [SerializeField] private float timePeriodOfShot = 2.0f;
    [SerializeField] private GameEvent shooting;
    [SerializeField] private GameEvent stoppedShooting;
    [HideInInspector]public bool aboutToFire = false;
    private bool cRBeforeShot = false;
    private bool cRDuringShot = false;
    [HideInInspector]public bool isShooting = false;
    // Update is called once per frame
    void Update()
    {
        if (aboutToFire && WallTurretController.Instance.playerInArea)
        {
            StartCoroutine(ShootWithHitScan());
            isShooting = true;
        }
        else if ((cRBeforeShot || cRDuringShot) && !WallTurretController.Instance.playerInArea)
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
        aboutToFire = false;
        cRBeforeShot = true;
        yield return new WaitForSeconds(timeBeforeShot);
        shooting.Raise();
        cRDuringShot = true;
        //Do damage to player if they are within the line renderer with raycast.hit (need to discuss if its a health system or not)
        yield return new WaitForSeconds(timePeriodOfShot);
        stoppedShooting.Raise();
        isShooting = false;
    }
    public void Shoot()
    {
        aboutToFire = true;
    }
}
