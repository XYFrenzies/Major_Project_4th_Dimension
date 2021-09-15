using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenedDoors : MonoBehaviour
{
    public DoorData doorData;
    public GameEvent OpenDoor1;
    public GameEvent OpenDoor2;
    public GameEvent OpenDoor3;
    public GameEvent OpenDoor4;
    public GameEvent OpenDoor5;

    // Start is called before the first frame update
    void Start()
    {
        if (doorData.door1Open == true)
            OpenDoor1.Raise();
        if (doorData.door2Open == true)
            OpenDoor2.Raise(); 
        if (doorData.door3Open == true)
            OpenDoor3.Raise(); 
        if (doorData.door4Open == true)
            OpenDoor4.Raise(); 
        if (doorData.door5Open == true)
            OpenDoor5.Raise();

    }

    public void Door1()
    {
        doorData.door1Open = true;
    }

    public void Door2()
    {
        doorData.door2Open = true;
    }

    public void Door3()
    {
        doorData.door3Open = true;
    }

    public void Door4()
    {
        doorData.door4Open = true;
    }

    public void Door5()
    {
        doorData.door5Open = true;
    }
}
