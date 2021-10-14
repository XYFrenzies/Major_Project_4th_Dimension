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
    public event Action<int> onGrapplePointNotVisibleMulti;
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
    public void GrapplePointNotVisible(int id)
    {
        if (onGrapplePointNotVisibleMulti != null)
        {
            onGrapplePointNotVisibleMulti(id);
        }
    }
    public event Action<int> onPullObject;
    public void PullObject(int id)
    {
        if (onPullObject != null)
        {
            onPullObject(id);
        }
    }

    public event Action onStopPullObject;
    public void StopPullObject()
    {
        if (onStopPullObject != null)
        {
            onStopPullObject();
        }
    }
}
