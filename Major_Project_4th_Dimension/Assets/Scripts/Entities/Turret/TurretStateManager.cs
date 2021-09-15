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
    [SerializeField] private GameObject m_player;

    //Spotlight
    [SerializeField] private Color m_baseColourSpotLight;
    [SerializeField] private Color m_shootColourSpotLight;
    private float m_spotLightRCRadius = 1.5f;

    //AimingSystem for attacking Player
    [SerializeField] private GameObject m_leftSight;
    [SerializeField] private GameObject m_rightSight;
    [SerializeField] private GameObject m_raycastChecker;
    #endregion
    // Start is called before the first frame update
    private void Start()
    {
        if (m_player == null)
        {
            m_player = GameObject.FindGameObjectWithTag("Player");
        }
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
        gizmos.transform.position = m_player.transform.position;
        if(!m_animationFiring.isPlaying)
            m_animationFiring.Play();
        if (m_deltaTimeTimer >= m_gracePeriodTimer)
        {
            RaycastAttackCheck(m_raycastChecker);
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
