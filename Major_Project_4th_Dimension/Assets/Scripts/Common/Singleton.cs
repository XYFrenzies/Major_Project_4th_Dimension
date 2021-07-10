using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This is a pattern that enables other classes that derive from this class to be considered a singlton.
/// </summary>
public class Singleton<T> : MonoBehaviour where T : Singleton<T>
{
    //The instance of the class.
    private static T m_Instance;

    public static T Instance
    {
        get
        {
            if (m_Instance == null)
            {
                T[] foundObjects = FindObjectsOfType<T>();

                if (foundObjects == null || foundObjects.Length == 0)
                {
                    return null;
                }
                else
                {
                    m_Instance = foundObjects[0];
                    return m_Instance;
                }
            }
            return m_Instance;
        }
    }
}
