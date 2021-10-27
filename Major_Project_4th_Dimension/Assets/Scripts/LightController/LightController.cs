using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LightToDoor
{
    public string spotLights { get; set; }
    public Light spotLightInScene;
    public Light pointLightInScene;
    public int eventID;
}
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
    public void SetIDToColour(int id, Color colour)
    {
        if (FindID(id))
        {
            lightLogic[id].pointLightInScene.color = colour;
            lightLogic[id].spotLightInScene.color = colour;
        }
    }
    private bool FindID(int id)
    {
        for (int i = 0; i < lightLogic.Count; i++)
        {
            if (lightLogic[i].eventID == id)
                return true;
        }
        return false;
    }
}
