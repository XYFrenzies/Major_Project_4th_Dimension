using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenMotion : MonoBehaviour
{
    public Animator anim;

    public void MoveUpperDoor()
    {
        anim.Play("DoorOpen");
    }

    public void MoveLowerDoor()
    {
        anim.Play("LowerDoorOpen");
    }
}
