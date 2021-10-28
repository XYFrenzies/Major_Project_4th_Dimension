using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Async : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
                SceneManager.LoadSceneAsync(i);
                SceneManager.GetSceneByBuildIndex(i).GetRootGameObjects()[0].SetActive(false);
        }
        SceneManager.GetSceneByName("MainMenu").GetRootGameObjects()[0].SetActive(true);
    }
    public void OpenNewScene(int i) 
    {
        SceneManager.GetActiveScene().GetRootGameObjects()[0].SetActive(false);
        SceneManager.GetSceneByBuildIndex(i).GetRootGameObjects()[0].SetActive(true);
    }
}
