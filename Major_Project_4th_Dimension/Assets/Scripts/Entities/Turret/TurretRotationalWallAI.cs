using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum TurretMovementWall
{
    PositionToPosition,
    GameObjectToGameObject,
    Stopped
}
[System.Serializable]
public class TurretRotationalWallAI : Singleton<TurretRotationalWallAI>
{
    public GameObject m_player = null;
    public GameObject m_faceTurret = null;
    public GameEvent m_takeDamage = null;
    public TurretMovementWall m_turretMovement;
    public List<Vector3> m_positionsToGoTo;
    public List<GameObject> m_objPosToGoTo;
    public float m_turretSearchSpeed = 2.0f;
    public int gameObjValue;
    public int positionValue;
    public Light m_spotLight;
    private bool m_cantRotate = false;
    private int positionThatsNext = 0;
    private List<Vector3> m_objectPositions;
    private float spotLightRCRadius = 1.5f;
    private void Awake()
    {
        if (m_faceTurret == null)
            m_cantRotate = true;
        m_objectPositions = new List<Vector3>();
        if (m_player == null)
        {
            m_player = GameObject.FindGameObjectWithTag("Player");
        }
        foreach (var item in m_objPosToGoTo)
        {
            m_objectPositions.Add(item.transform.position);
        }
    }
    // Update is called once per frame
    private void Update()
    {
        if (!m_cantRotate || m_turretMovement == TurretMovementWall.Stopped)
        {
            switch (m_turretMovement)
            {
                case TurretMovementWall.PositionToPosition:
                    ObjectToObject(m_positionsToGoTo);
                    break;
                case TurretMovementWall.GameObjectToGameObject:
                    ObjectToObject(m_objectPositions);
                    break;
            }

        }
        RaycastHit hit;
        if (Physics.SphereCast(m_spotLight.gameObject.transform.position, spotLightRCRadius, m_spotLight.gameObject.transform.forward, out hit)
            && hit.transform.gameObject == m_player)
        {
            m_takeDamage.Raise();
        }
    }
    private void ObjectToObject(List<Vector3> positions)
    {
        while (positions != null && positions.Count > 1)
        {
            Vector3 dir = (positions[positionThatsNext] - transform.position).normalized;
                Quaternion m_headLookDirection = Quaternion.LookRotation(dir, m_faceTurret.transform.right);
            Vector3 m_rotationHead = Quaternion.RotateTowards(m_faceTurret.transform.rotation,
    m_headLookDirection, Time.deltaTime * m_turretSearchSpeed).eulerAngles;
            m_faceTurret.transform.rotation = Quaternion.Euler( m_rotationHead.x, 0f, 180f);

            double lRRound = Math.Round(m_headLookDirection.eulerAngles.x);
            double turRRound = Math.Round(m_faceTurret.transform.localEulerAngles.x);
            if ((turRRound - 3f) == lRRound || lRRound == turRRound || (turRRound + 3f) == lRRound)
            {
                if (positionThatsNext >= positions.Count - 1)
                {
                    positionThatsNext = 0;
                }
                else if (positionThatsNext < positions.Count - 1)
                {
                    positionThatsNext += 1;
                }
            }
            return;
        }
    }
}
