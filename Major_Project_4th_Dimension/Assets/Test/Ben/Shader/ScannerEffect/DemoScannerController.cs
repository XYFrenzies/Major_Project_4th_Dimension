using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class DemoScannerController : MonoBehaviour
{
    [SerializeField] private Transform m_scanLocation = null;
    [SerializeField]private Material material;
    [SerializeField]private float m_speed = 40.0f;
    private float _scanDistance;
    bool _scanning;
    //Checking if the keybind for the scanner has been pressed.
    public void Scanner(InputAction.CallbackContext context) 
    {
        if (context.phase != InputActionPhase.Performed)
        {
            return;
        }
        _scanning = true;
        _scanDistance = 3;
        material.SetFloat("_ScanDistance", _scanDistance);
    }
    void Update()
    {
        if (_scanning)
        {
            _scanDistance += Time.deltaTime * m_speed;
            foreach (var indicator in Indicator.Instance.objWithIndicators)
            {
                if (Vector3.Distance(m_scanLocation.position, indicator.transform.position) <= _scanDistance)
                {
                    indicator.transform.GetChild(0).gameObject.SetActive(true);
                }
            }
            material.SetFloat("_ScanDistance", _scanDistance);
            material.SetVector("_WorldSpaceScannerPos", new Vector4(m_scanLocation.position.x, m_scanLocation.position.y, m_scanLocation.position.z, 0));
        }
    }
    private void OnDisable()
    {
        _scanDistance = 0;
        material.SetFloat("_ScanDistance", _scanDistance);
    }
}
