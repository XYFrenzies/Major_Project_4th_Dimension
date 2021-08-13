using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : Singleton<GlobalVariables>
{
    //[HideInInspector] public string time = "";
    //[HideInInspector] public float score = 0;
    //[HideInInspector] public bool isTowerDead = false;
    [HideInInspector]public bool isFading = false;
    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void RestartGlobalValues()
    {

        //time = "";
        //score = 0;
        //isTowerDead = false;
    }
    public bool CheckIsFade()
    {
        switch (isFading)
        {
            case true:
                return true;
            case false:
                return false;
        }
    }
}
