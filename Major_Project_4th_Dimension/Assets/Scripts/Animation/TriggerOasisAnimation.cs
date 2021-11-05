using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerOasisAnimation : MonoBehaviour
{
    [SerializeField] private List<GameObject> cameras;
    [SerializeField] private GameObject m_playerMoveState;
    [SerializeField] private GameObject m_oasisPlayer;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        m_oasisPlayer.SetActive(false);
        foreach (var item in cameras)
        {
            item.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        for (int i = 0; i < cameras.Count; i++)
        {
            cameras[i].SetActive(true);
        }
        m_playerMoveState.SetActive(false);
        m_oasisPlayer.SetActive(true);
        anim = m_oasisPlayer.GetComponent<Animator>();
        anim.SetBool("endGame", true);
    }
    private void Update()
    {
        //anim
    }
}
