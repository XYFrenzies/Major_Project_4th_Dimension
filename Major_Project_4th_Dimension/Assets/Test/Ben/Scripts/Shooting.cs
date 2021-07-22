using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private float timeBeforeShot = 2.0f;
    [SerializeField] private float timePeriodOfShot = 2.0f;
    [SerializeField] private bool isProjectile = false;
    [SerializeField] private bool isHitScan = true;
    [SerializeField] private GameEvent shooting;
    [SerializeField] private GameEvent stoppedShooting;
    private bool aboutToFire = false;
    // Update is called once per frame
    void Update()
    {
        if (!aboutToFire)
        {
            if (isHitScan)
            {
                ShootWithHitScan();
            }
            else if (isProjectile)
            {
                ShootWithProjectile();
            }
        }
    }
    IEnumerator ShootWithHitScan()
    {
        aboutToFire = true;
        yield return new WaitForSeconds(timeBeforeShot);
        
        //Do damage to player if they are within the line renderer
    }
    public void ShootWithProjectile()
    {
        aboutToFire = true;
    }
}
