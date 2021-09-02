using UnityEngine;
using System.Collections.Generic;
public enum TurretMovement 
{
    RotatingAround,
    PositionToPosition,
    GameObjectToGameObject,
    Stopped
}
public class TurretRotationalAI : Singleton<TurretRotationalAI>
{
    public GameObject m_baseTurret = null;
    public GameObject m_bodyTurret = null;
    public GameObject m_faceTurret = null;
    public TurretMovement m_turretMovement = TurretMovement.RotatingAround;
    public Vector3 m_minBaseRotation = Vector3.zero;
    public Vector3 m_maxBaseRotation = Vector3.zero;
    public Vector3 m_minBodyRotation = Vector3.zero;
    public Vector3 m_maxBodyRotation = Vector3.zero;
    public List<GameObject> m_posToGoTo;
    private bool m_cantRotate = false;

    private Quaternion m_baseLookRotation;
    private Quaternion m_bodyLookRotation;
    private Quaternion m_faceLookRotation;
    private void Awake()
    {
        if (m_bodyTurret == null || m_baseTurret == null || m_faceTurret == null)
            m_cantRotate = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!m_cantRotate || m_turretMovement == TurretMovement.Stopped)
        {
            if (m_turretMovement == TurretMovement.PositionToPosition)
                PositionToPosition();
            else if (m_turretMovement == TurretMovement.RotatingAround)
                RotatingAround();
            else if (m_turretMovement == TurretMovement.GameObjectToGameObject)
                ObjectToObject();
        }
    }
    private void RotatingAround() 
    {
        //lookRotation = Quaternion.LookRotation();
    }
    private void PositionToPosition() 
    {

    }
    private void ObjectToObject() 
    {

    }
}
