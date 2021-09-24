using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// A system for the fading in and out effect that can be easily used across other scripts.
/// If this script is needing to have impact across different scenes, uyse the global variables option that is
/// set to dont destroy.
/// For the time being, the effect wont be used until later into production.
/// </summary>
public class FadingEffect : Singleton<FadingEffect>
{
    //[SerializeField] private bool fadeEffectActive = false;
    //[SerializeField] private float fadingInTime = 2.0f;
    //[SerializeField] private float timePeriodBeforeFadeOut = 1.0f;
    //[SerializeField] private float fadingOutTime = 2.0f;
    //private float m_alphaIn = 0;
    //private float m_alphaOut = 1.0f;
    //private Image matFading = null;
    //private bool fadedIn = false;
    //private bool fadedOut = false;
    //private bool fadeInOnly = false;
    //private bool fadeOutOnly = false;
    //private void Awake()
    //{
    //    matFading = GetComponent<Image>();
    //}
    //// Update is called once per frame
    //private void Update()
    //{
    //    if ((fadeEffectActive && !matFading.enabled && !GlobalVariables.Instance.isFading) || fadeInOnly)
    //    {
    //        matFading.enabled = true;
    //        fadedIn = true;
    //        fadedOut = false;
    //    }
    //    else if (GlobalVariables.Instance.isFading)
    //    {
    //        fadedIn = false;
    //        fadedOut = true;
    //    }
    //    if(fadedIn)
    //        FadeIn();
    //    else if(fadedOut || fadeOutOnly)
    //        FadeOut();
    //}
    //private void FadeIn() 
    //{
    //    matFading.color = new Color(matFading.color.r, matFading.color.g, matFading.color.b,
    //        m_alphaIn += Time.unscaledDeltaTime * fadingInTime);
    //    if (matFading.color.a >= 1)
    //    {
    //        fadedIn = false;
    //        if(!fadeInOnly)
    //            StartCoroutine(TimeBeforeTransition());
    //        fadeInOnly = false;
    //        //Time.timeScale = 0;
    //    }
    //}
    //private IEnumerator TimeBeforeTransition() 
    //{
    //    yield return new WaitForSeconds(timePeriodBeforeFadeOut);
    //    fadedOut = true;
    //}
    //private void FadeOut() 
    //{
    //    matFading.color = new Color(matFading.color.r, matFading.color.g, matFading.color.b,
    //m_alphaOut -= Time.unscaledDeltaTime * fadingOutTime);
    //    if (matFading.color.a <= 0)
    //    {
    //        fadedIn = false;
    //        Time.timeScale = 1;
    //        matFading.enabled = false;
    //        fadeOutOnly = false;
    //    }
    //}
    //public void RestartSceneFade() 
    //{
    //    GlobalVariables.Instance.isFading = true;
    //}
    //public void FadeEffect() 
    //{
    //    fadeEffectActive = true;
    //}
    //public void FadeInOnly() 
    //{
    //    fadeInOnly = true;
    //    fadeOutOnly = false;
    //}
    //public void FadeOutOnly() 
    //{
    //    fadeOutOnly = true;
    //    fadeInOnly = false;
    //}

}
