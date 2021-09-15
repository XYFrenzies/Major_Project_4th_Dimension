using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DoorData", menuName = "DoorData/NewDoorData", order = 1)]
public class DoorData : ScriptableObject
{
    public bool door1Open = false;
    public bool door2Open = false;
    public bool door3Open = false;
    public bool door4Open = false;
    public bool door5Open = false;
}
