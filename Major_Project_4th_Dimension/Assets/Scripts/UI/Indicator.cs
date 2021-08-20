using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Indicator : Singleton<Indicator>
{
    [HideInInspector] public List<GameObject> objToAddImagesTo;
    private void Awake()
    {
        objToAddImagesTo = new List<GameObject>();
        List<GameObject[]> obj = new List<GameObject[]>() { GameObject.FindGameObjectsWithTag("CanHookShotTowards"),
        GameObject.FindGameObjectsWithTag("MoveableToMe"),  GameObject.FindGameObjectsWithTag("BigPullObject")};
        foreach (var item in obj)
        {
            for (int i = 0; i < item.Length; i++)
            {
                objToAddImagesTo.Add(item[i]);
            }
        }
    }
}
