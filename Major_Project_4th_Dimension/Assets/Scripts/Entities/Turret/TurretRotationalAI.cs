using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum TurretMovement
{
    PositionToPosition,
    GameObjectToGameObject,
    Stopped
}
[System.Serializable]
public class TurretRotationalAI : Singleton<TurretRotationalAI>
{
    public GameObject m_baseTurret = null;
    public GameObject m_bodyTurret = null;
    public GameObject m_faceTurret = null;
    public TurretMovement m_turretMovement;
    public List<Vector3> m_positionsToGoTo;
    public List<GameObject> m_objPosToGoTo;
    public float m_turretSearchSpeed = 2.0f;
    public int gameObjValue;
    public int positionValue;
    private bool m_cantRotate = false;
    private int positionThatsNext = 0;
    private List<Vector3> m_objectPositions;

    private void Awake()
    {
        if (m_bodyTurret == null || m_baseTurret == null || m_faceTurret == null)
            m_cantRotate = true;
        m_objectPositions = new List<Vector3>();
        foreach (var item in m_objPosToGoTo)
        {
            m_objectPositions.Add(item.transform.position);
        }
    }
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!m_cantRotate || m_turretMovement == TurretMovement.Stopped)
        {
            switch (m_turretMovement)
            {
                case TurretMovement.PositionToPosition:
                    ObjectToObject(m_positionsToGoTo);
                    break;
                case TurretMovement.GameObjectToGameObject:
                    ObjectToObject(m_objectPositions);
                    break;
            }

        }
    }
    private void ObjectToObject(List<Vector3> positions)
    {
        while (positions != null && positions.Count > 1)
        {
            Vector3 dir = positions[positionThatsNext] - transform.position;
            Quaternion m_lookRotation = Quaternion.LookRotation(dir, m_baseTurret.transform.right);
            Vector3 rotation = Quaternion.RotateTowards(m_baseTurret.transform.rotation,
                m_lookRotation, Time.deltaTime * m_turretSearchSpeed).eulerAngles;
            m_baseTurret.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            //if(rotation.y != m_baseTurret.transform.rotation.y)
                return;
            //else if (positionThatsNext >= m_objPosToGoTo.Count - 1)
            //    positionThatsNext = 0;
            //else if (positionThatsNext < m_objPosToGoTo.Count - 1)
            //    positionThatsNext += 1;
        }
    }
}
