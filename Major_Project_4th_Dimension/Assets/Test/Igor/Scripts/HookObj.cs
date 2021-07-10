using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class HookObj : MonoBehaviour
{
    public string[] tagsToCheck;
    //Force applied to nova bomb upon spawn
    public float speed;
    public float returnSpeed;
    public float range;
    public float stopRange;

    [HideInInspector]
    public Transform hookShotPoint;
    [HideInInspector]
    public Transform objectCollidedWith;

    //private LineRenderer line;
    private bool hasCollided;


    private void Awake()
    {
        //line = GetComponentInChildren<LineRenderer>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (hookShotPoint)
        {
            //line.SetPosition(0, hookShotPoint.position);
            //line.SetPosition(1, transform.position);
            //Check if we have impacted
            if (hasCollided)
            {
                transform.LookAt(hookShotPoint);
                float dist = Vector3.Distance(transform.position, hookShotPoint.position);
                if (dist < stopRange)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                float dist = Vector3.Distance(transform.position, hookShotPoint.position);
                if (dist > range)
                {
                    Collision(null);
                }
            }

            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            if (objectCollidedWith) 
            { 
                objectCollidedWith.transform.position = transform.position; 
            }
        }
        else { Destroy(gameObject); }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Here add as many checks as you want for your nova bomb's collision
        if (!hasCollided && tagsToCheck.Contains(other.gameObject.tag))
        {
            Collision(other.transform);
        }
    }

    void Collision(Transform col)
    {
        speed = returnSpeed;
        //Stop movement
        hasCollided = true;
        if (col)
        {
            transform.position = col.position;
            objectCollidedWith = col;
        }
    }

}
