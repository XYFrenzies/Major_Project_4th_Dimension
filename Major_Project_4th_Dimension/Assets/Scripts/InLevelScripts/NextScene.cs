using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    [SerializeField] private string m_nameOfSceneNext;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GlobalVariables.Instance.SaveScene(m_nameOfSceneNext);
            SceneManager.LoadScene("TransitionScene");
        }
    }
    public void EndGame() 
    {
        SceneManager.LoadScene(m_nameOfSceneNext);
    }
}
