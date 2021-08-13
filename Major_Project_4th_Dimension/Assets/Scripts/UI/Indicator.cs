using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : Singleton<Indicator>
{
    public List<GameObject> objWithIndicators = null;
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var obj in objWithIndicators)
        {
            Canvas objWithCanvas = obj.GetComponentInChildren<Canvas>();
            if (objWithCanvas)
                objWithCanvas.gameObject.SetActive(false);
        }
    }
}
