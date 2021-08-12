using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor_Move_Z_Direction : MonoBehaviour
{

    [SerializeField] private float speed = 0.5f;
    [SerializeField] private float ScrollX = 0.5f;
    [SerializeField] private float ScrollY = 0.5f;
    private Rigidbody rBody;
    void Awake()
    {
        rBody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = rBody.position;
        rBody.position += Vector3.back * speed * Time.deltaTime;
        rBody.MovePosition(pos);
    }
    void Update()
    {
        float OffsetX = Time.time * ScrollX;
        float OffsetY = Time.time * ScrollY;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(OffsetX, OffsetY);
    }
}
