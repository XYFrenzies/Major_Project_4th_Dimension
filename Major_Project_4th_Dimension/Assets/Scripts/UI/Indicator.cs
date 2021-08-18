using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Indicator : Singleton<Indicator>
{
    [SerializeField] private Image imgScan = null;
    [HideInInspector] public List<GameObject> objToAddImagesTo;
    private readonly Canvas can;
    // Start is called before the first frame update
    void Awake()
    {
        objToAddImagesTo = new List<GameObject>();
        List<GameObject[]> obj = new List<GameObject[]>() { GameObject.FindGameObjectsWithTag("CanHookShotTowards"),
        GameObject.FindGameObjectsWithTag("MoveableToMe"),  GameObject.FindGameObjectsWithTag("BigPullObject")};
        foreach (var item in obj)
        {
            Array.Copy(item, objToAddImagesTo.ToArray(), 100);
        }
        foreach (var item in objToAddImagesTo)
        {
            can.renderMode = RenderMode.WorldSpace;
            can.worldCamera = Camera.main;
            can.transform.parent = item.transform;
            imgScan.transform.parent = can.transform;
            can.gameObject.SetActive(false);
        }
    }
}
