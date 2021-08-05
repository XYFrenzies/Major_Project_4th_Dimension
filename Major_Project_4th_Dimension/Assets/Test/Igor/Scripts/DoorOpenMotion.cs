using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorOpenMotion : MonoBehaviour
{
    //public Animator anim;
    public float openSpeed = 5f;
    public float closeSpeed = 5f;
    private Vector3 target;
    [Space]
    public float xAxisAmount = 0f;
    public float yAxisAmount = 0f;
    public float zAxisAmount = 0f;
    Vector3 startPos;
    bool open = false;
    bool close = false;

    private void Awake()
    {
        target = new Vector3(transform.position.x + xAxisAmount, transform.position.y + yAxisAmount, transform.position.z + zAxisAmount);
        startPos = transform.position;
    }

    private void Update()
    {
        if (open)
            OpenDoor();
        if (close)
            CloseDoor();
    }

    public void OpenDoor()
    {
        open = true;
        close = false;
        transform.position = Vector3.MoveTowards(transform.position, target, openSpeed * Time.deltaTime);
        //transform.Translate(0, 0, Time.deltaTime);
        //anim.Play("DoorOpen");
        Debug.Log("open");
    }

    public void CloseDoor()
    {
        close = true;
        open = false;
        transform.position = Vector3.MoveTowards(transform.position, startPos, closeSpeed * Time.deltaTime);

        //anim.Play("LowerDoorOpen");
        Debug.Log("close");

    }
}
