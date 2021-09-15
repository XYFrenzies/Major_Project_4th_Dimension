using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretState 
{
    Inactive,
    Startup,
    Searching,
    Attacking
}
public class TurretSearcher : MonoBehaviour
{
    //TurretState
    [SerializeField] private TurretState m_turretState;

    //Startup
    [SerializeField] private Animation m_animateTurretStartUp;
    [SerializeField] private GameEvent m_startUpTurret;

    //The player rotation
    [SerializeField] private List<GameObject> m_objectsRotatingTo;
    [SerializeField] private GameObject gizmos;
    [SerializeField] private float m_rotationSpeed = 2.0f;
    private int m_positionMove;

    //The period of time before death
    [SerializeField] private float m_gracePeriodTimer = 3.0f;
    [SerializeField] private GameEvent m_restartLevel;
    private float m_deltaTimeTimer = 0f;

    //Find Player system
    [SerializeField] private Light m_spotLight;
    [SerializeField] private GameObject m_player;

    //Spotlight
    [SerializeField] private Color m_baseColourSpotLight;
    [SerializeField] private Color m_shootColourSpotLight;
    private float m_spotLightRCRadius = 1.5f;

    //AimingSystem for attacking Player
    [SerializeField] private GameObject m_leftSight;
    [SerializeField] private GameObject m_rightSight;

    // Start is called before the first frame update
    private void Start()
    {
        if (m_player == null)
        {
            m_player = GameObject.FindGameObjectWithTag("Player");
        }
        m_positionMove = m_objectsRotatingTo.Count;
        m_spotLight.color = m_baseColourSpotLight;
    }

    // Update is called once per frame
    private void Update()
    {
        switch (m_turretState)
        {
            case TurretState.Inactive:
                break;
            case TurretState.Startup:
                PlayStartUpAnimation();
            break;
            case TurretState.Attacking:
                FindPlayer();
                break;
            case TurretState.Searching:
                MoveAround();
                RaycastSearchCheck();
                break;
        }
    }
    //Enables for the turret to startup on GameEvent change;
    public void IsActiveTurret()
    {
        m_turretState = TurretState.Startup;
    }
    //Plays animation of the turret starting up.
    private void PlayStartUpAnimation() 
    {
        if (!m_animateTurretStartUp.isPlaying)
            m_turretState = TurretState.Searching;
    }

    private void MoveAround()
    {
        while (m_objectsRotatingTo != null && m_positionMove > 1)
        {
            gizmos.transform.position = Vector3.MoveTowards(gizmos.transform.position,
                m_objectsRotatingTo[m_positionMove].transform.position, m_rotationSpeed * Time.deltaTime);
            if (gizmos.transform.position == m_objectsRotatingTo[m_positionMove].transform.position)
            {
                if (m_positionMove >= m_objectsRotatingTo.Count - 1)
                    m_positionMove = 0;
                else if (m_positionMove < m_objectsRotatingTo.Count - 1)
                    m_positionMove += 1;
            }
        }
    }
    private void FindPlayer()
    {
        gizmos.transform.position = m_player.transform.position;

        if (m_deltaTimeTimer >= m_gracePeriodTimer)
        {
            RaycastAttackCheck(m_leftSight);
            RaycastAttackCheck(m_rightSight);
            m_gracePeriodTimer = 0;
        }
        
    }
    private void RaycastSearchCheck() 
    {
        RaycastHit hit;
        if (Physics.SphereCast(m_spotLight.gameObject.transform.position, m_spotLightRCRadius, m_spotLight.gameObject.transform.forward, out hit)
            && hit.transform.gameObject == m_player)
        {
            m_turretState = TurretState.Attacking;
            m_spotLight.color = m_shootColourSpotLight;
            m_leftSight.SetActive(true);
            m_rightSight.SetActive(true);
        }
    }
    private void RaycastAttackCheck(GameObject obj) 
    {
        RaycastHit hit;
        if (Physics.Raycast(obj.transform.position,obj.transform.forward, out hit)
            && hit.transform.gameObject == m_player)
            m_restartLevel.Raise();
        else
            m_turretState = TurretState.Searching;
    }

}
