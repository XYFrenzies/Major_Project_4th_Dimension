using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretDirection
{
    Forward,
    Backward,
}
public enum Movement
{
    OtherDirection,
    ContinueInLine
}
public class MovingTurret : MonoBehaviour
{
    [SerializeField] private bool canMove = true;
    [Tooltip("All the gameobjects on the trail line.")]
    [SerializeField] private List<Transform> a_trails = null;
    [SerializeField] private float speed = 1;
    [SerializeField] private TurretDirection CurrentDirection = TurretDirection.Forward;
    [Tooltip("When it reaches the last rail, will it go the other direction or continue back to the original point (if its close to the end point).")]
    [SerializeField] private Movement wayToMove = Movement.OtherDirection;
    [Tooltip("The place the target goes to first. It goes by index so it will start from 0 onwards.")]
    [SerializeField] private int currentTarget = 0;
    // Update is called once per frame
    private void Update()
    {
        if (a_trails != null && canMove)
        {                
            if (IsAtCurrentTarget())
            {
                GetNextIndex();
                if (currentTarget >= a_trails.Count || currentTarget <= -1)
                {
                    if (wayToMove == Movement.ContinueInLine)
                        IsEndTargetGoal();
                    else if (wayToMove == Movement.OtherDirection)
                    {
                        ChangeDirections();
                        GetNextIndex();
                    }
                }
            }
            MoveTowardsTarget();
        }
    }
    private float DistanceSqud(Vector3 from, Vector3 to)
    {
        Vector3 fromTo = to - from;
        float dissqrd = Vector3.Dot(fromTo, fromTo);
        return dissqrd;
    }
    private bool IsAtCurrentTarget()
    {
        return DistanceSqud(transform.position, a_trails[currentTarget].position) <= float.Epsilon;
    }
    private void GetNextIndex()
    {
        switch (CurrentDirection)
        {
            case TurretDirection.Forward:
                {
                    currentTarget++;
                    break;
                }
            case TurretDirection.Backward:
                {
                    currentTarget--;
                    break;
                }
        }
    }
    private void ChangeDirections()
    {
        switch (CurrentDirection)
        {
            case TurretDirection.Backward:
                CurrentDirection = TurretDirection.Forward;
                break;
            case TurretDirection.Forward:
                CurrentDirection = TurretDirection.Backward;
                break;
        }
    }
    private void IsEndTargetGoal()
    {
        switch (CurrentDirection)
        {
            case TurretDirection.Backward:
                currentTarget = a_trails.Count - 1;
                break;
            case TurretDirection.Forward:
                currentTarget = 0;
                break;
        }
    }
    private void MoveTowardsTarget()
    {
        transform.position = Vector3.MoveTowards(transform.position,
         a_trails[currentTarget].position, speed * Time.deltaTime);
    }
}