using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Conveyor_Move_X_Direction : MonoBehaviour
{
    public float speed;
    Rigidbody rBody;

    // Start is called before the first frame update
    void Start()
    {
        speed = 0.5f;
        rBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 pos = rBody.position;
        rBody.position += Vector3.left * speed * Time.deltaTime;
        rBody.MovePosition(pos);
    }


}
