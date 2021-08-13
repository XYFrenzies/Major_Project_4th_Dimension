using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadingEffect : Singleton<FadingEffect>
{
    [SerializeField] private bool fadeEffectActive = false;
    [SerializeField] private float fadingInTime = 2.0f;
    [SerializeField] private float timePeriodBetweenFade = 1.0f;
    [SerializeField] private float fadingOutTime = 2.0f;
    private Image matFading = null;
    private bool fadedIn = false;
    private bool fadedOut = false;
    private void Awake()
    {
        matFading = GetComponent<Image>();
    }
    // Update is called once per frame
    private void Update()
    {
        if (fadeEffectActive && !matFading.enabled)
        {
            matFading.enabled = true;
            fadedIn = true;
        }
        else if (GlobalVariables.Instance.isFading)
        {
            fadedOut = true;
        }
        if(fadedIn)
            FadeIn();
        if(fadedOut)
            FadeOut();
    }
    private void FadeIn() 
    {
        
    } 
    private void FadeOut() 
    {

    }
    public void RestartSceneFade() 
    {
        GlobalVariables.Instance.isFading = true;
    }
    public void ChangeSceneFade() 
    {
        fadeEffectActive = true;
    }

}
