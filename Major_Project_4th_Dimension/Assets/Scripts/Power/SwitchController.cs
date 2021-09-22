using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchController : MonoBehaviour
{
    [SerializeField] private List<GameObject> m_switchesInScene;
    private void Awake()
    {
        SwitchOff();
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
        }
    }
    public void SwitchOff()
    {
        if (m_switchesInScene != null)
        {
            foreach (var item in m_switchesInScene)
            {
                item.SetActive(false );
            }
        }
    }
}
