using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LandingFinished : MonoBehaviour
{
    public PlayerStateManager psMan;

    public void FinishedLanding()
    {
        psMan.hasLanded = true;
    }
}
