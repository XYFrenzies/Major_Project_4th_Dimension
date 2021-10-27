using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : Singleton<LightController>
{
    [SerializeField] private List<LightToDoor> lightLogic;
    [SerializeField] private Color m_defaultColour = Color.red;
    // Start is called before the first frame update
    private void Awake()
    {
        for (int i = 0; i < lightLogic.Count; i++)
        {
            lightLogic[i].spotLightInScene.color = m_defaultColour;
            lightLogic[i].pointLightInScene.color = m_defaultColour;
        }
    }
}
