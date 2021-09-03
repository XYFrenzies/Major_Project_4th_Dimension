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
        DrawStandingSineWave(currentGrapplePosition,2f,3f,2f);
        // line.SetPosition(0, pos[0].position);
        // line.SetPosition(1, Vector3.MoveTowards(pos[0].position, pos[1].position, speed * Time.time));

        //Vector3 calculatePoint = hookshotPosition;

        //currentGrapplePosition = Vector3.Lerp(currentGrapplePosition, calculatePoint, Time.deltaTime * hookshotFlySpeed);
        //for (int i = 0; i < maxHookShotDistance; i++)
        //{
        //    Vector3 dir = i * (currentGrapplePosition - transform.position) / maxHookShotDistance;
        //    float x = dir.magnitude;
        //    float y = Mathf.Sin(x * waveScale);

        //    float percent = (float)i / maxHookShotDistance;

        //    Vector3 way = dir + transform.position + (transform.rotation * new Vector3(y, 0, 0) * magnitudeOverDistance.Evaluate(percent));
        //    lineRenderer.SetPosition(i, way);
        //}
    }

    void DrawStandingSineWave(Vector3 startPoint, float amplitude, float wavelength, float waveSpeed)
    {

        float x = 0f;
        float y;
        float k = 2 * Mathf.PI / wavelength;
        float w = k * waveSpeed;
        lineRenderer.positionCount = 200;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            x += i * 0.001f;
            y = amplitude * (Mathf.Sin(k * x + w * Time.time) + Mathf.Sin(k * x - w * Time.time));
            lineRenderer.SetPosition(i, new Vector3(x, y, 0) + startPoint);
        }
    }

    void DrawTravellingSineWave(Vector3 startPoint, float amplitude, float wavelength, float waveSpeed)
    {

        float x = 0f;
        float y;
        float k = 2 * Mathf.PI / wavelength;
        float w = k * waveSpeed;
        lineRenderer.positionCount = 200;
        for (int i = 0; i < lineRenderer.positionCount; i++)
        {
            x += i * 0.001f;
            y = amplitude * Mathf.Sin(k * x + w * Time.time);
            lineRenderer.SetPosition(i, new Vector3(x, y, 0) + startPoint);
        }
    }
}
