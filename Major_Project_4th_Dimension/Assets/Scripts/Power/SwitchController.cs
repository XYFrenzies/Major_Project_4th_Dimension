using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_switchesInScene;
    [SerializeField] private List<GameEvent> closeDoors;
    [SerializeField] private List<Light> lightsInScene;
    private void Awake()
    {
        SwitchOff();
        foreach (var item in lightsInScene)
        {
            item.color = Color.red;
        }
    }
    // Update is called once per frame
    public void SwitchOn()
    {
        if (m_switchesInScene != null)
        {
            foreach (var item in m_switchesInScene)
            {
                item.SetActive(true);
            }
            foreach (var item in lightsInScene)
            {
                item.color = Color.green;
            }
        }
    }
    public void SwitchOff()
    {
        if (m_switchesInScene != null)
        {
            foreach (var item in m_switchesInScene)
            {
                item.SetActive(false);
            }
            foreach (var item in closeDoors)
            {
                item.Raise();
            }
            foreach (var item in lightsInScene)
            {
                item.color = Color.red;
            }

        }
    }
}
