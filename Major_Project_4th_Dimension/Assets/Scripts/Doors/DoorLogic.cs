using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorLogic : MonoBehaviour
{
    [SerializeField] private float openSpeed = 5f;
    [SerializeField] private float closeSpeed = 5f;
    private Vector3 target;
    [Space]
    [SerializeField] private float xAxisAmount = 0f;
    [SerializeField] private float yAxisAmount = 0f;
    [SerializeField] private float zAxisAmount = 0f;
    private Vector3 startPos;
    private bool open = false;
    private bool close = false;
    [SerializeField] private DoorData doorData;
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
        if (doorData.door4Open)
        {
            open = true;
            close = false;
            transform.position = Vector3.MoveTowards(transform.position, target, openSpeed * Time.deltaTime);
        }
    }

    public void CloseDoor()
    {
        close = true;
        open = false;
        transform.position = Vector3.MoveTowards(transform.position, startPos, closeSpeed * Time.deltaTime);

    }
}
