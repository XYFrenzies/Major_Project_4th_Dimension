using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TurretState
{
    Inactive,
    Startup,
    StartDown,
    Searching,
    Attacking
}
public class TurretStateManager : MonoBehaviour
{
    #region Variables
    //TurretState
    [SerializeField] private TurretState m_turretState;

    //Startup
    [SerializeField] private GameEvent m_startUpTurret;

    //TurretAnimation
    [SerializeField] private Animation m_animationStartDown;
    [SerializeField] private Animation m_animationFiring;
    [SerializeField] private Animation m_animateTurretStartUp;

    //The player rotation
    [SerializeField] private List<GameObject> m_objectsRotatingTo;
    [SerializeField] private GameObject gizmos;
    [SerializeField] private float m_rotationSpeed = 2.0f;
    [SerializeField] private int m_startingPosition = 1;
    private int m_positionMove;

    //The period of time before death
    [SerializeField] private float m_gracePeriodTimer = 3.0f;
    [SerializeField] private GameEvent m_restartLevel;
    private float m_deltaTimeTimer = 0f;

    //Find Player system
    [SerializeField] private Light m_spotLight;

    //Spotlight
    [SerializeField] private Color m_baseColourSpotLight;
    [SerializeField] private Color m_shootColourSpotLight;
    [SerializeField] private float m_spotLightRCRadius = 1.5f;
    [SerializeField] private float m_turretRange = 10f;

    //AimingSystem for attacking Player
    [SerializeField] private GameObject m_leftSight;
    [SerializeField] private GameObject m_rightSight;
    [SerializeField] private GameObject m_raycastChecker;
    private bool m_playerInArea = false;
    #endregion
    // Start is called before the first frame update
    private void Start()
    {
        m_spotLight.color = m_baseColourSpotLight;
        m_positionMove = m_startingPosition - 1;
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
            case TurretState.StartDown:
                PlayStartDownAni();
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
        m_animateTurretStartUp.Play();
    }
    public void IsSettingInActiveTurret()
    {
        m_turretState = TurretState.StartDown;
        m_animationStartDown.Play();
    }
    private void PlayStartDownAni()
    {
        if (!m_animationStartDown.isPlaying)
            m_turretState = TurretState.Inactive;
    }
    //Plays animation of the turret starting up.
    private void PlayStartUpAnimation()
    {
        if (!m_animateTurretStartUp.isPlaying)
            m_turretState = TurretState.Searching;
    }

    private void MoveAround()
    {
        while (m_objectsRotatingTo != null && m_objectsRotatingTo.Count > 1)
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
            else
                return;
        }
    }
    private void FindPlayer()
    {
        gizmos.transform.position = Vector3.MoveTowards(gizmos.transform.position, GameObject.FindGameObjectWithTag("Player").transform.position, m_rotationSpeed * Time.deltaTime);
        //if(!m_animationFiring.isPlaying)
        //    m_animationFiring.Play();
        if (StopCheck())
            return;
        m_deltaTimeTimer += Time.deltaTime;
        if (m_deltaTimeTimer >= m_gracePeriodTimer)
        {
            RaycastAttackCheck();
            m_deltaTimeTimer = 0;
        }
    }
    private void RaycastSearchCheck()
    {
        RaycastHit hit;
        if (Physics.SphereCast(m_spotLight.gameObject.transform.position, m_spotLightRCRadius, m_spotLight.gameObject.transform.forward, out hit, m_turretRange)
            && hit.transform.gameObject == hit.transform.CompareTag("Player"))
        {
            m_turretState = TurretState.Attacking;
            m_spotLight.color = m_shootColourSpotLight;
            m_leftSight.SetActive(true);
            m_rightSight.SetActive(true);
        }
    }
    private void RaycastAttackCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_raycastChecker.transform.position, m_raycastChecker.transform.forward, out hit)
            && hit.transform.gameObject == hit.transform.CompareTag("Player"))
            m_restartLevel.Raise();

        m_turretState = TurretState.Searching;
        m_spotLight.color = m_baseColourSpotLight;

    }
    private bool StopCheck()
    {
        RaycastHit hit;
        if (Physics.Raycast(m_raycastChecker.transform.position, m_raycastChecker.transform.forward, out hit)
            && hit.transform.gameObject == hit.transform.CompareTag("StopSearch") && !m_playerInArea)
        {
            m_turretState = TurretState.Searching;
            m_spotLight.color = m_baseColourSpotLight;
            return true;
        }
        return false;
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_playerInArea = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            m_playerInArea = false;
        }
    }
}
