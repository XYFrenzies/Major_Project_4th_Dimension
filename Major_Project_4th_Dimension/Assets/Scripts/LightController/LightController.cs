using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum LightState
{
    Green,
    Yellow,
    Red
}
public class LightToDoor
{
    public string spotLights { get; set; }
    public Light spotLightInScene;
    public Light pointLightInScene;
    public LightState currentLightState = LightState.Red;
    public int eventID;
}
public class LightController : Singleton<LightController>
{
    [SerializeField] private List<LightToDoor> lightLogic;
    [SerializeField] private Color m_defaultColour = Color.red;
    private bool isInMainMenu = false;
    // Start is called before the first frame update
    private void Awake()
    {
        if (isInMainMenu)
        {
            for (int i = 0; i < lightLogic.Count; i++)
            {
                if (m_defaultColour == Color.red)
                    lightLogic[i].currentLightState = LightState.Red;
                else if (m_defaultColour == Color.green)
                    lightLogic[i].currentLightState = LightState.Green;
                else if (m_defaultColour == Color.yellow)
                    lightLogic[i].currentLightState = LightState.Yellow;
                lightLogic[i].spotLightInScene.color = m_defaultColour;
                lightLogic[i].pointLightInScene.color = m_defaultColour;
            }
        }
    }
    public LightToDoor GetLightByID(int id) 
    {
        if(FindID(id))
          return lightLogic[id];
        Debug.Log("No light with ID exists");
        return null;
    }
    public void SetIDToColour(int id, Color colour) 
    {
        if (FindID(id))
        {
            lightLogic[id].currentLightState = LightState.Red;
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
