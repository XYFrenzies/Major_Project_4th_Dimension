using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFOV : MonoBehaviour
{
    private Camera playerCamera;
    private float targetFOV;
    private float FOV;
    public float fovSpeed = 4f;

    // Start is called before the first frame update
    void Awake()
    {
        playerCamera = GetComponent<Camera>();
        targetFOV = playerCamera.fieldOfView;
        FOV = targetFOV;
    }

    // Update is called once per frame
    void Update()
    {
        FOV = Mathf.Lerp(FOV, targetFOV, Time.deltaTime * fovSpeed);
        playerCamera.fieldOfView = FOV;
    }

    public void SetCameraFOV(float targetFov)
    {
        targetFOV = targetFov;
    }
}
