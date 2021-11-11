using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightToDoor : MonoBehaviour
{
    [SerializeField]private Light spotLightInScene;
    [SerializeField]private Light pointLightInScene;
    private void Awake()
    {
        if (spotLightInScene == null)
        {
            spotLightInScene = transform.Find("Spot_Light").GetComponent<Light>();
        }
        if (pointLightInScene == null)
            pointLightInScene = transform.Find("Point_Light").GetComponent<Light>();
    }

    public void SetIDToRed()
    {
        pointLightInScene.color = Color.red;
        spotLightInScene.color = Color.red;
    }
    public void SetIDToGreen()
    {
        pointLightInScene.color = Color.green;
        spotLightInScene.color = Color.green;
    }
    public void SetIDToYellow()
    {
        pointLightInScene.color = Color.yellow;
        spotLightInScene.color = Color.yellow;
    }
}
