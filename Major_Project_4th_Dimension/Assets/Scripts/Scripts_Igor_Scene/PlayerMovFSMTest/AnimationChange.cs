using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationChange : MonoBehaviour
{
    public Animator anim;
    public string currentState;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
            return;

        anim.Play(newState);

        currentState = newState;
    }
}
