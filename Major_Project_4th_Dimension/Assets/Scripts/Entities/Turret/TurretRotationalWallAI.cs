using System;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public enum TurretMovementWall
{
    PositionToPosition,
    Stopped
}
[System.Serializable]
public class TurretRotationalWallAI : Singleton<TurretRotationalWallAI>
{
    public GameObject m_player = null;
    public GameObject m_faceTurret = null;
    public GameEvent m_takeDamage = null;
    public TurretMovementWall m_turretMovement;
    public List<Vector3> m_rotationOfTurret;
    public float m_turretSearchSpeed = 2.0f;
    public int gameObjValue;
    public int positionValue;
    public Light m_spotLight;
    private bool m_cantRotate = false;
    private int positionThatsNext = 0;
    private float spotLightRCRadius = 1.5f;
    private Quaternion quat;
    private void Awake()
    {
        if (m_faceTurret == null)
            m_cantRotate = true;
        if (m_player == null)
        {
            m_player = GameObject.FindGameObjectWithTag("Player");
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
                    ObjectToObject();
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
    private void ObjectToObject()
    {
        while (m_rotationOfTurret != null && m_rotationOfTurret.Count > 1)
        {
            quat = Quaternion.Euler(m_rotationOfTurret[positionThatsNext].x, 0f, 0f);
            Vector3 rotation = Quaternion.RotateTowards(m_faceTurret.transform.localRotation,
    quat, Time.deltaTime * m_turretSearchSpeed).eulerAngles;
            m_faceTurret.transform.localRotation = Quaternion.Euler(rotation.x, 0f, 0f);
            double lRRound = Math.Round(m_rotationOfTurret[positionThatsNext].x);
            double turRRound = Math.Round(m_faceTurret.transform.localEulerAngles.x);
            if (lRRound == turRRound)
            {
                if (positionThatsNext >= m_rotationOfTurret.Count - 1)
                    positionThatsNext = 0;
                else if (positionThatsNext < m_rotationOfTurret.Count - 1)
                    positionThatsNext += 1;
            }
            return;
        }
    }
}
