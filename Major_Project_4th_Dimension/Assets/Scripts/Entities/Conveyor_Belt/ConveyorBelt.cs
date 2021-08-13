using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Uses two gameobjects connected to the conveyor belt 
/// </summary>
public enum Direction
{
    Forward,
    Backward
}
public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private Direction m_directionToGo;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float m_scrollX = 0.5f;
    [SerializeField] private float m_scrollY = 0.5f;
    private GameObject endPointForward;
    private GameObject endPointBackward;
    private void Awake()
    {
        endPointBackward = transform.Find("Backward").gameObject;
        endPointForward = transform.Find("Forward").gameObject;
    }
    void Update()
    {
        float OffsetX = Time.time * m_scrollX;
        float OffsetY = Time.time * m_scrollY;
        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(OffsetX, OffsetY);
    }
    private void OnCollisionStay(Collision collision)
    {
        switch (m_directionToGo) 
        {
            case Direction.Forward:
                MoveDirection(collision, endPointForward);
                break;
            case Direction.Backward:
                MoveDirection(collision, endPointBackward);
                break;
        }
    }
    private void MoveDirection(Collision col, GameObject obj)
    {
        col.transform.position = Vector3.MoveTowards(col.transform.position, obj.transform.position, speed * Time.deltaTime);
    }
    public void ChangeDirection()
    {
        switch (m_directionToGo)
        {
            case Direction.Forward:
                m_directionToGo = Direction.Backward;
                break;
            case Direction.Backward:
                m_directionToGo = Direction.Forward;
                break;
            default:
                Debug.Log("An error has occured. The direction can not be changed!");
                break;
        }
    }
}
