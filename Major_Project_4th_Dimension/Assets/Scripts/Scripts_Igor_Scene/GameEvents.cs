using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event Action<int> onGrapplePointVisible;
    public void GrapplePointVisible(int id)
    {
        if(onGrapplePointVisible != null)
        {
            onGrapplePointVisible(id);
        }
    }

    public event Action onGrapplePointNotVisible;
    public void GrapplePointNotVisible()
    {
        if (onGrapplePointNotVisible != null)
        {
            onGrapplePointNotVisible();
        }
    }

}
