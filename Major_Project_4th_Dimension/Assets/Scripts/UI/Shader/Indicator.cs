using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
/// <summary>
/// The indicator is a globally used class that has all the obejcts that can be interacted with in the scene.
/// </summary>
public class Indicator : Singleton<Indicator>
{
    [HideInInspector] public List<GameObject> objCanMoveAround;//Objects that can be moved around
    [HideInInspector] public List<GameObject> objCanHookTo;//The objects that can be hooked to
    [HideInInspector] public List<GameObject> objPower;//The powers
    [HideInInspector] public List<GameObject> objSwitch;//The switches
    /// <summary>
    /// This is set on awake as it is the initial beginning of the scene and before the first frame.
    /// The more objects that have this, the more costly the effect will be.
    /// </summary>
    private void Awake()
    {
        objCanMoveAround = new List<GameObject>();
        objCanHookTo = new List<GameObject>();
        objPower = new List<GameObject>();
        objSwitch = new List<GameObject>();
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
        foreach (var item in GameObject.FindGameObjectsWithTag("Power"))
        {

            if (item.name == "CBSwitch")
            {
                GameObject obj = item.transform.Find("SM_Conveyor_Control_Box").gameObject;
                objPower.Add(obj);
            }
            else
                objPower.Add(item);
        }
        foreach (var item in GameObject.FindGameObjectsWithTag("Switch"))
        {
            objSwitch.Add(item);
        }
    }
}
