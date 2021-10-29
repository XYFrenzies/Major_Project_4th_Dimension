using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingCheck : MonoBehaviour
{
    public ArmEffects armE;
    public ArmStateManager armSM;

    public void ShootingAnimationCheck()
    {
        armE.isShooting = true;
        armSM.isShootingAnimationReady = true;
        //armSM.lineRenderer.enabled = true;
        //armSM.blackHoleCentre.SetActive(true);
        //armSM.realisticBlackHole.SetActive(true);
    }

    public void StopShooting()
    {
        armE.isShooting = false;
        armSM.isShootingAnimationReady = false;
        armSM.playerSM.animator.SetBool("IsShooting", false);
    }
}
