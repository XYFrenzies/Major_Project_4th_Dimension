using UnityEngine;

public enum TurretMovement 
{
    RotatingAround,
    PositionToPosition,
    GameObjectToGameObject
}
public class TurretRotationalAI : Singleton<TurretRotationalAI>
{
    [SerializeField] private GameObject m_baseTurret = null;
    [SerializeField] private GameObject m_bodyTurret = null;
    [SerializeField] private GameObject m_faceTurret = null;
    [SerializeField] private TurretMovement m_turretMovement = TurretMovement.RotatingAround;
    [SerializeField] private Vector3 m_minBaseRotation = Vector3.zero;
    [SerializeField] private Vector3 m_maxBaseRotation = Vector3.zero;
    [SerializeField] private Vector3 m_minBodyRotation = Vector3.zero;
    [SerializeField] private Vector3 m_maxBodyRotation = Vector3.zero;
    private bool m_cantRotate = false;
    [HideInInspector] public bool stopRotating = false;
    private void Awake()
    {
        if (m_bodyTurret == null || m_baseTurret == null || m_faceTurret == null)
            m_cantRotate = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (!m_cantRotate)
        {

        }
    }
    private void RotatingAround() 
    {

    }
    private void PositionToPosition() 
    {

    }
}
