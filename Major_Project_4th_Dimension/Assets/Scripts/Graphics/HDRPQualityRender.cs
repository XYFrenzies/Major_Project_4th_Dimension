using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
/// <summary>
/// HDRP in options menu
/// </summary>
public class HDRPQualityRender : MonoBehaviour
{
    [SerializeField] private HDRenderPipelineAsset m_qualityChanger;
    // Start is called before the first frame update

    public void RayTracing() 
    {
        //m_qualityChanger.currentPlatformRenderPipelineSettings.supportedRayTracingMode;
    }
   // public void 
    public void SaveQuality() 
    {
        
    }
}
