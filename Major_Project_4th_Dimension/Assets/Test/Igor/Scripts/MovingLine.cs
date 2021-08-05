using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLine : MonoBehaviour
{
    public LineRenderer line;
    public Transform[] pos;
    public float speed = 55f;
    // Start is called before the first frame update
    void Start()
    {
        line = GetComponent<LineRenderer>();
        
    }

    // Update is called once per frame
    void Update()
    {
        line.SetPosition(0, pos[0].position);
        line.SetPosition(1, Vector3.MoveTowards(pos[0].position, pos[1].position, speed * Time.deltaTime));
    }
}
