using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Call : MonoBehaviour
{
    public Snapshot_Camera snapCam;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            snapCam.CallTakeSnapshot();
        }
    }
}
