using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Name of Creater: Benjamin McDonald
/// Date of Creation: 26/3/2021
/// Last Modified: 9/4/2021
/// </summary>
public class CameraClipping : MonoBehaviour
{
    [SerializeField] public Material alphaMat = null;
    [SerializeField] private Camera m_playerCamera = null;
    [SerializeField] [Range(3.0f, 7.0f)] public float rayCastRange = 5.5f;
    [HideInInspector] public List<MeshRenderer> listobj;
    [HideInInspector] public List<Material> objectMaterials;
    private GameObject lastHitObject;
    private void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_playerCamera.gameObject.transform.position,
            m_playerCamera.gameObject.transform.forward, out hit, rayCastRange) &&//Needs to be adjustable not have 6 as its parameter.
            !hit.collider.gameObject.CompareTag("Player") && lastHitObject != hit.collider.gameObject)
        {
            //Saves memory space as there will be less variables to check through a list.
            MeshRenderer objectMesh = hit.transform.gameObject.GetComponent<MeshRenderer>();
            if (objectMesh != null && alphaMat != null)
            {
                //if (objectMesh.gameObject.CompareTag("Barrier"))//Because this is being called in update, it is always being called.
                //{
                AddToList(objectMesh);
                lastHitObject = hit.collider.gameObject;
                //}
            }
        }
        else
            SetBack();


    }
    private void SetBack()//Setting the meshRenderer back to active
    {
        for (int i = 0; i < listobj.Count; i++)
        {
            var value = listobj[i];
            value.material = objectMaterials[i];
        }
        listobj.Clear();
        objectMaterials.Clear();
    }
    private void AddToList(MeshRenderer obj)//Setting the mesh renderer back to inactive.
    {
        if (IsInList(obj))
        {
            objectMaterials.Add(obj.material);
            obj.material = alphaMat;
            listobj.Add(obj);
        }
    }
    private bool IsInList(MeshRenderer obj)
    {
        for (int i = 0; i < listobj.Count; i++)
        {
            if (obj == listobj[i])
                return false;
        }
        return true;
    }
}
