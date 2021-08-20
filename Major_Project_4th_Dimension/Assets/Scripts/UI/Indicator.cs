using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Indicator : Singleton<Indicator>
{
    [HideInInspector] public List<GameObject> objCanMoveAround;
    [HideInInspector] public List<GameObject> objCanHookTo;
    private void Awake()
    {
        objCanMoveAround = new List<GameObject>();
        objCanHookTo = new List<GameObject>();
        List<GameObject[]> objMove = new List<GameObject[]>() {GameObject.FindGameObjectsWithTag("MoveableToMe"),  
            GameObject.FindGameObjectsWithTag("BigPullObject")};

        foreach (var item in objMove)
        {
            for (int i = 0; i < item.Length; i++)
            {
                objCanMoveAround.Add(item[i]);
            }
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("CanHookShotTowards"))
        {
            objCanHookTo.Add(item);
        }
    }
}
