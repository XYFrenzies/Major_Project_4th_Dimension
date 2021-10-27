using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : Singleton<UIManager>
{
    public void EnableObjects(bool active, List<GameObject> obj) 
    {
        foreach (var item in obj)
        {
            item.SetActive(active);
        }
    }

}
