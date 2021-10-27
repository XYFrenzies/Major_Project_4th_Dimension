using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToDoor
{
    public string spotLights { get; set; }
    public Light spotLightInScene;
    public Color colourLight;
}
public class LightController : MonoBehaviour
{
    [SerializeField] private List<LightToDoor> lightLogic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
