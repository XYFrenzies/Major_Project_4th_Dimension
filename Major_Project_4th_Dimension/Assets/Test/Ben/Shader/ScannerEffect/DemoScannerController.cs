using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class DemoScannerController : MonoBehaviour
{
    [SerializeField] private Transform m_scanLocation = null;
    public Material material;
    private float _scanDistance;
    public float Speed;
    bool _scanning;

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
            _scanDistance += Time.deltaTime * Speed;
            material.SetFloat("_ScanDistance", _scanDistance);
            material.SetVector("_WorldSpaceScannerPos", new Vector4(m_scanLocation.position.x, m_scanLocation.position.y, m_scanLocation.position.z, 0));
        }
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        //    RaycastHit hit;

        //    if (Physics.Raycast(ray, out hit))
        //    {
        //        _scanning = true;
        //        _scanDistance = 0;
        //        material.SetFloat("_ScanDistance", _scanDistance);
        //        material.SetVector("_WorldSpaceScannerPos", hit.point);
        //    }
        //}
    }
    private void OnDisable()
    {
        _scanDistance = 0;
        material.SetFloat("_ScanDistance", _scanDistance);
    }

}
