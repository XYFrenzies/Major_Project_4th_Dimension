using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Async : MonoBehaviour
{
    private List<AsyncOperation> asyncList;
    // Start is called before the first frame update
    private void Start()
    {

        for (int i = 0; i < SceneManager.sceneCountInBuildSettings; i++)
        {
            AsyncOperation newAsync = SceneManager.LoadSceneAsync(i);
            asyncList.Add(newAsync);
            newAsync.allowSceneActivation = false;
            SceneManager.GetSceneByBuildIndex(i).GetRootGameObjects()[0].SetActive(false);
        }
        SceneManager.GetSceneByName("MainMenu").GetRootGameObjects()[0].SetActive(true);
    }
    public void OpenNewScene(int i)
    {
        for (int index = 0; index < asyncList.Count; index++)
        {
            asyncList[index].allowSceneActivation = true;
        }
    }
}
