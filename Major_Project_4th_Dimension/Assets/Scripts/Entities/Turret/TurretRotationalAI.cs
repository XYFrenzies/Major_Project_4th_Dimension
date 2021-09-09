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
    public GameObject m_player = null;
    public GameObject m_baseTurret = null;
    public GameObject m_bodyTurret = null;
    public GameObject m_faceTurret = null;
    public GameEvent m_takeDamage = null;
    public TurretMovement m_turretMovement;
    public List<Vector3> m_positionsToGoTo;
    public List<GameObject> m_objPosToGoTo;
    public float m_turretSearchSpeed = 2.0f;
    public int gameObjValue;
    public int positionValue;
    private bool m_cantRotate = false;
    private int positionThatsNext = 0;
    private List<Vector3> m_objectPositions;
    private Quaternion m_headLookDirection;
    private Quaternion m_bodyLookDirection;
    private bool seenOnce = false;
    private Light m_spotLight;
    private float spotLightRCRadius = 1.5f;
    private void Awake()
    {
        if (m_bodyTurret == null || m_baseTurret == null || m_faceTurret == null)
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
            Vector3 dir = positions[positionThatsNext] - m_faceTurret.transform.position;
            if (!seenOnce)
            {
                m_headLookDirection = Quaternion.LookRotation(new Vector3(dir.x, dir.y, dir.z + 25f), m_faceTurret.transform.up);
                m_bodyLookDirection = Quaternion.LookRotation(dir);
                seenOnce = true;
            }
            Vector3 m_rotationHead = Quaternion.RotateTowards(m_faceTurret.transform.rotation,
    m_headLookDirection, Time.deltaTime * m_turretSearchSpeed).eulerAngles;
            Vector3 rotation = Quaternion.RotateTowards(m_baseTurret.transform.rotation,
    m_bodyLookDirection, Time.deltaTime * m_turretSearchSpeed).eulerAngles;

            m_baseTurret.transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);
            m_faceTurret.transform.eulerAngles = new Vector3(m_rotationHead.x, rotation.y, 0f);

            double lRRound = Math.Round(m_bodyLookDirection.eulerAngles.y);
            double turRRound = Math.Round(m_baseTurret.transform.localEulerAngles.y);
            if ((turRRound - 3f) == lRRound  || lRRound == turRRound || (turRRound + 3f) == lRRound)
            {
                if (positionThatsNext >= m_objPosToGoTo.Count - 1)
                {
                    positionThatsNext = 0;
                    seenOnce = false;
                }
                else if (positionThatsNext < m_objPosToGoTo.Count - 1)
                {
                    positionThatsNext += 1;
                    seenOnce = false;
                }
            }
            return;
        }
    }
}
