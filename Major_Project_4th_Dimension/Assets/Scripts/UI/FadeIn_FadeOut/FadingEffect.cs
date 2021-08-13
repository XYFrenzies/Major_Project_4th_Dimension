using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadingEffect : Singleton<FadingEffect>
{
    [SerializeField] private bool fadeEffectActive = false;
    [SerializeField] private Material matFading = null;
    [SerializeField] private float fadingInTime = 2.0f;
    [SerializeField] private float timePeriodBetweenFade = 1.0f;
    [SerializeField] private float fadingOutTime = 2.0f;
    // Update is called once per frame
    void Update()
    {
        if (fadeEffectActive && matFading != null)
        {

        }
    }
    public void RestartSceneFade() 
    {
        fadeEffectActive = true;
    }
    public void ChangeSceneFade() 
    {
        fadeEffectActive = true;
    }
}
