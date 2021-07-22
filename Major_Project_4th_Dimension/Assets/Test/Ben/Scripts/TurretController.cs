using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    bool isShooting = false;
    private PlayerController player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isShooting)
            Quaternion.Lerp(new Quaternion(0, transform.rotation.y, 0, 1), player.transform.rotation, Time.deltaTime); ;
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
