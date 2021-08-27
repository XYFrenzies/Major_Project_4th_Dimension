using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class Camera_Call : MonoBehaviour
{
    public Snapshot_Camera snapCam;
    private PlayerInput input;
    private InputAction snapshotButton;
    private float time = 0.0f;
    private float minTime = 1.0f;
    void Awake()
    {
        input = FindObjectOfType<PlayerInput>();
        snapshotButton = input.actions["Jump"];
    }
    // Update is called once per frame
    void Update()
    {
        float value = snapshotButton.ReadValue<float>();
        if (value > 0 && time > minTime)
        {
            Debug.Log("Hi");
            //snapCam.CallTakeSnapshot();
            time = 0;
        }
        time += Time.unscaledDeltaTime;
    }
}
