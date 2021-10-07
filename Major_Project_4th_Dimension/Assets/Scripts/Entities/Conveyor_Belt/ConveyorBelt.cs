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
    [SerializeField] private GameObject endPointForward;
    [SerializeField] private GameObject endPointBackward;
    [SerializeField] private GameObject materialTexture;
    private void Update()
    {
        if (PowerStatus.Instance.powerIsOn)
        {
            gameObject.GetComponent<BoxCollider>().enabled = true;
            float OffsetX = Time.time * m_scrollX;
            float OffsetY = Time.time * m_scrollY;
            materialTexture.GetComponent<Renderer>().material.mainTextureOffset = new Vector2(OffsetX, OffsetY);
        }
        else if (!PowerStatus.Instance.powerIsOn)
            gameObject.GetComponent<BoxCollider>().enabled = false;
    }
    private void OnTriggerStay(Collider other)
    {
        switch (m_directionToGo)
        {
            case Direction.Forward:
                MoveDirection(other, endPointForward);
                break;
            case Direction.Backward:
                MoveDirection(other, endPointBackward);
                break;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && other.gameObject.GetComponent<Rigidbody>())
            other.gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
    }
    private void MoveDirection(Collider col, GameObject obj)
    {
        if (PowerStatus.Instance.powerIsOn)
            col.transform.position = Vector3.MoveTowards(col.transform.position, obj.transform.position, speed * Time.deltaTime);
    }
    public void ChangeDirection()
    {
        m_scrollX = -m_scrollX;
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
