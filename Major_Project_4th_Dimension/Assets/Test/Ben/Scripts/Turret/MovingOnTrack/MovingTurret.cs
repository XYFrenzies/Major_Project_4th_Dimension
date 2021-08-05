using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretDirection
{
    Forward,
    Backward,
    Stand_Still
}
public class MovingTurret : MonoBehaviour
{
    [Tooltip("First one will be the first point in the sequence of the turret.")]
    [SerializeField] private List<GameObject> pointsToGoTo;
    [Tooltip("Will be greater than 0!")]
    [SerializeField] private int sequenceNumberToStart;
    [Tooltip("Direction to move to first.")]
    private TurretDirection movingTowards;
    private bool m_moveTurret = false;
    // Start is called before the first frame update
    private void Awake()
    {
        if (pointsToGoTo != null && pointsToGoTo.Count >= 1)
        {
            m_moveTurret = true;
        }
    }

    // Update is called once per frame
    private void Update()
    {
        if (m_moveTurret)
        {

        }
    }

    private void MovingSequence() 
    {

    }
}