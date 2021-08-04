using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpotLightRotation : Singleton<SpotLightRotation>
{
    [SerializeField] private GameEvent m_takeDamage = null;
    private Light m_spotLight;
    private GameObject m_player;
    private bool hasTakenDamage = false;
    private Color currentColourLight;
    private float spotLightRCRadius = 1.5f;
    // Start is called before the first frame update
    private void Awake()
    {
        m_spotLight = GetComponent<Light>();
        m_player = GameObject.FindGameObjectWithTag("Player");
        currentColourLight = m_spotLight.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (WallTurretController.Instance.playerInArea)
        {
            RaycastHit hit;
            if(Physics.SphereCast(m_spotLight.gameObject.transform.position, spotLightRCRadius, m_spotLight.gameObject.transform.forward, out hit)
                && hit.transform.gameObject == m_player && !hasTakenDamage && !WallTurShoot.Instance.isShooting)
            {
                WallTurShoot.Instance.aboutToFire = true;
                m_takeDamage.Raise();
            }
        }
    }
    public void IsShooting()
    {
        //Different colour of the spotlight here
        m_spotLight.color = Color.red;
    }
    public void IsNotShooting()
    {
        //Different colour of the spotlight here
        m_spotLight.color = currentColourLight;
        hasTakenDamage = false;
    }
}
