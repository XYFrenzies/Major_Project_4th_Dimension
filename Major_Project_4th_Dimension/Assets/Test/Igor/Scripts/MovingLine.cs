using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingLine : MonoBehaviour
{
  //  public LineRenderer line;
    public Transform[] pos;

    public float waveScale = 1f;
    public float hookshotFlySpeed = 1f;
    public int maxHookShotDistance = 100;
    public AnimationCurve magnitudeOverDistance;
    LineRenderer lineRenderer;
    Vector3 currentGrapplePosition;
    
    private Vector3 hookshotPosition;
    
    // public float speed = 55f;
    // Start is called before the first frame update
    void Start()
    {
        // line = GetComponent<LineRenderer>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = maxHookShotDistance;
        hookshotPosition = pos[1].position;
        currentGrapplePosition = pos[0].position;
    }

    // Update is called once per frame
    void Update()
    {
        // line.SetPosition(0, pos[0].position);
        // line.SetPosition(1, Vector3.MoveTowards(pos[0].position, pos[1].position, speed * Time.time));

        Vector3 calculatePoint = hookshotPosition;

        currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, calculatePoint, Time.deltaTime * hookshotFlySpeed);
        for (int i = 0; i < maxHookShotDistance; i++)
        {
            Vector3 dir = i * (currentGrapplePosition - transform.position) / maxHookShotDistance;
            float x = dir.magnitude;
            float y = Mathf.Sin(x * waveScale);

            float percent = (float)i / maxHookShotDistance;

            Vector3 way = dir + transform.position + (transform.rotation * new Vector3(y, 0, 0) * magnitudeOverDistance.Evaluate(percent));
            lineRenderer.SetPosition(i, way);
        }
    }
}
