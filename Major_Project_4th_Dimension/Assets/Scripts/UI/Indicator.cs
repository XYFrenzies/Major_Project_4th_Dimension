using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Indicator : Singleton<Indicator>
{
    [HideInInspector] public List<GameObject> objToAddImagesTo;
    private Canvas canvas;
    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>();
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
        foreach (var item in objToAddImagesTo)
        {
            Canvas can = Instantiate(canvas, item.transform);
            can.transform.position = item.transform.position;
            can.GetComponentInChildren<Image>().gameObject.transform.position = item.transform.position;
            can.gameObject.SetActive(false);
        }
        canvas.GetComponentInChildren<Image>().gameObject.SetActive(false);
        canvas.gameObject.SetActive(false);
    }
}
