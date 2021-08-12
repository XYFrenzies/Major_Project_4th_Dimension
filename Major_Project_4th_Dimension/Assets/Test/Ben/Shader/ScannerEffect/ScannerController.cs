using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class ScannerController : MonoBehaviour
{
    [SerializeField] private bool m_scanGreyScale = false; 
    [SerializeField] private Transform m_scanLocation = null;
    [SerializeField] private Material material;
    [SerializeField] private float m_speed = 40.0f;
    private float m_scanDistance;
    private bool m_scanning;
    private Camera m_camera;
    private List<string> alpha = new List<string>();//This is used to prevent code from being repeated.
    private string[] alphabet = { "A", "B", "C", "D"};
    private void Awake()
    {
        m_camera = Camera.main;
        alpha.AddRange(alphabet);
    }
    //Checking if the keybind for the scanner has been pressed.
    public void Scanner(InputAction.CallbackContext context) 
    {
        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        m_scanning = true;
        m_scanDistance = 3;
        material.SetFloat("_ScanDistance", m_scanDistance);
    }
    void Update()
    {
        if (m_scanning)
        {
            m_scanDistance += Time.deltaTime * m_speed;
            foreach (var indicator in Indicator.Instance.objWithIndicators)
            {
                if (Vector3.Distance(m_scanLocation.position, indicator.transform.position) <= m_scanDistance)
                {
                    indicator.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            if (m_scanGreyScale)
            {
               
            }
            material.SetFloat("_ScanDistance", m_scanDistance);
            material.SetVector("_WorldSpaceScannerPos", new Vector4(m_scanLocation.position.x, m_scanLocation.position.y, m_scanLocation.position.z, 0));
        }
        CameraFrustumCorners();
    }
    private void CameraFrustumCorners()
    {
        Vector3[] frustumCorners = new Vector3[4];
        Vector3[] frustumCornersWorldPos = new Vector3[4];
        m_camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), m_camera.farClipPlane, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);
        for (int i = 0; i < 4; i++)
        {
            var worldSpaceCorner = m_camera.transform.TransformVector(frustumCorners[i]);
            frustumCornersWorldPos[i] = worldSpaceCorner;
            material.SetVector("_vector" + alphabet[i], frustumCornersWorldPos[i]);
        }
    }
    private void OnDisable()
    {
        m_scanDistance = 0;
        material.SetFloat("_ScanDistance", m_scanDistance);
    }
}
