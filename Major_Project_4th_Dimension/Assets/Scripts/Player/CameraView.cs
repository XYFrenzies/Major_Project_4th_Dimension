using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraView : MonoBehaviour
{
    [SerializeField] private GameObject m_player;
    [SerializeField] private float maxDistanceRaycast = 4.5f;
    private Transform m_obstruction;
    private List<MeshRenderer> listobj;
    private GameObject lastHitObject;
    private void Awake()
    {
        listobj = new List<MeshRenderer>();
    }
    // Update is called once per frame
    private void LateUpdate()
    {
        ViewObstructed();
    }

    private void ViewObstructed()
    {
        RaycastHit hit;
        if (Physics.CapsuleCast(transform.position, m_player.transform.position - transform.position, 10.0f, transform.forward, out hit, maxDistanceRaycast))
        {
            if (lastHitObject == hit.collider.gameObject || !hit.transform.gameObject.GetComponent<MeshRenderer>())
                return;

            if (hit.collider.gameObject.tag != "Player")
            {
                m_obstruction = hit.transform;
                m_obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                listobj.Add(m_obstruction.gameObject.GetComponent<MeshRenderer>());
                lastHitObject = m_obstruction.gameObject;
                //if (Vector3.Distance(Obstruction.position, transform.position) >= 3f && Vector3.Distance(transform.position, Target.position) >= 1.5f)
                //    transform.Translate(Vector3.forward * zoomSpeed * Time.deltaTime);
            }
            else
            {
                m_obstruction.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                //if (Vector3.Distance(transform.position, Target.position) < 4.5f)
                //    transform.Translate(Vector3.back * zoomSpeed * Time.deltaTime);
            }
        }
        else if (listobj.Count > 0)
        {
            for (int i = 0; i < listobj.Count; i++)
            {
                listobj[i].shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
                if (lastHitObject = listobj[i].gameObject)
                    lastHitObject = null;
            }
        }
    }
}
