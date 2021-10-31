using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateAnimation : MonoBehaviour
{
    [SerializeField] private GameEvent m_playAnimation;

    public void PlayAni() 
    {
        m_playAnimation.Raise();
    }
}
