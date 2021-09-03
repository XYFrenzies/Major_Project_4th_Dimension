using System.Collections.Generic;
using UnityEngine;
public enum TurretMovement
{
    PositionToPosition,
    GameObjectToGameObject,
    Stopped
}
public class TurretRotationalAI : Singleton<TurretRotationalAI>
{
    public GameObject m_baseTurret = null;
    public GameObject m_bodyTurret = null;
    public GameObject m_faceTurret = null;
    public TurretMovement m_turretMovement = TurretMovement.PositionToPosition;
    public List<Vector3> m_positionsToGoTo;
    public List<GameObject> m_objPosToGoTo;
    public float m_turretSearchSpeed = 2.0f;
    private bool m_cantRotate = false;
    private int positionThatsNext = 0;
    private Quaternion m_lookRotation;
    private List<Vector3> m_objectPositions;
    private void Awake()
    {
        if (m_bodyTurret == null || m_baseTurret == null || m_faceTurret == null)
            m_cantRotate = true;
        foreach (var item in m_objPosToGoTo)
        {
            m_objectPositions.Add(item.transform.position);
        }
    }
    // Update is called once per frame
    private void Update()
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
        if (positions != null && positions.Count > 1)
        {
            m_lookRotation = Quaternion.LookRotation(positions[positionThatsNext]);
            if (m_baseTurret.transform.rotation != m_lookRotation)
                m_baseTurret.transform.rotation = Quaternion.RotateTowards(m_baseTurret.transform.rotation, new Quaternion(0, m_lookRotation.y, 0, 1), Time.deltaTime * m_turretSearchSpeed);
            else if (positionThatsNext >= m_objPosToGoTo.Count)
                positionThatsNext = 0;
            else if (positionThatsNext < m_objPosToGoTo.Count)
                positionThatsNext += 1;
        }
    }
}
