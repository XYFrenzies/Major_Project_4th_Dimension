using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Any designer can use this to select the name of the level to proceed to next.
/// </summary>
public class LevelSelect : MonoBehaviour
{
    public void LevelToSelect(string name) 
    {
        SceneManager.LoadScene(name);
    }
}
