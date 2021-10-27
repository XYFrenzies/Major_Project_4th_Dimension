using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : Singleton<LightController>
{
    [SerializeField] private List<LightToDoor> lightLogic;

    /// <summary>
    /// colo
    /// </summary>
    /// <param name="id"></param>
    /// <param name="colour"> is the number for which corresponds to the colour value
    ///  green = 0
    ///  yellow = 1
    ///  red = 2</param>
    public void SetColourID(int id, int colour)
    {
        for (int i = 0; i < lightLogic.Count; i++)
        {
            if (lightLogic[i].eventID == id)
            {
                if (colour > 2 || colour < 0)
                    return;
                switch (colour)
                {
                    case 0:
                        lightLogic[i].SetIDToGreen();
                        break;
                    case 1:
                        lightLogic[i].SetIDToYellow();
                        break;
                    case 2:
                        lightLogic[i].SetIDToRed();
                        break;
                }
            }
        }
    }
}
