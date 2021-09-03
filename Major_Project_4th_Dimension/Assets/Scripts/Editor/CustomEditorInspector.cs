using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
[CustomEditor(typeof(TurretRotationalAI))]
[CanEditMultipleObjects]
public class CustomEditorInspector : Editor
{
    TurretRotationalAI turretAI;
    private int gameObjValue;
    private int positionValue;
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        turretAI = (TurretRotationalAI)target;
        if (turretAI != null)
        {
            GUILayout.Label("Rotation of Turret");
            GUILayout.Label("Type of Rotation");
            turretAI.m_turretMovement =
                (TurretMovement)EditorGUILayout.EnumPopup("Turret movement", turretAI.m_turretMovement);

            turretAI.m_baseTurret =
                (GameObject)EditorGUILayout.ObjectField("Base of the Turret", turretAI.m_baseTurret,
                typeof(GameObject), false);
            turretAI.m_bodyTurret =
                (GameObject)EditorGUILayout.ObjectField("Body of the Turret", turretAI.m_bodyTurret,
                typeof(GameObject), false);
            turretAI.m_faceTurret =
                (GameObject)EditorGUILayout.ObjectField("Face of the Turret", turretAI.m_faceTurret,
                typeof(GameObject), false);

            turretAI.m_turretSearchSpeed = EditorGUILayout.FloatField("Search Speed", turretAI.m_turretSearchSpeed);

            if (turretAI.m_turretMovement == TurretMovement.GameObjectToGameObject)
            {
                gameObjValue = EditorGUILayout.IntField("Amount of Objects", gameObjValue);
                for (int i = 0; i < gameObjValue; i++)
                {
                    while (gameObjValue != turretAI.m_objPosToGoTo.Count) 
                    {
                        if(gameObjValue < turretAI.m_objPosToGoTo.Count)
                            turretAI.m_objPosToGoTo.RemoveAt(turretAI.m_objPosToGoTo.Count - 1);
                        else if(gameObjValue > turretAI.m_objPosToGoTo.Count)
                            turretAI.m_objPosToGoTo.Add(new GameObject());
                    }
                    int value = i + 1;
                    turretAI.m_objPosToGoTo[i] = 
                        (GameObject)EditorGUILayout.ObjectField("Position " + value, turretAI.m_objPosToGoTo[i],
                        typeof(GameObject), false);
                }
            }
            else if (turretAI.m_turretMovement == TurretMovement.PositionToPosition)
            {
                positionValue = EditorGUILayout.IntField("Amount of Positions", positionValue);
                for (int i = 0; i < positionValue; i++)
                {
                    while (positionValue != turretAI.m_positionsToGoTo.Count)
                    {
                        if (positionValue < turretAI.m_positionsToGoTo.Count)
                            turretAI.m_positionsToGoTo.RemoveAt(turretAI.m_positionsToGoTo.Count - 1);
                        else if (positionValue > turretAI.m_positionsToGoTo.Count)
                            turretAI.m_positionsToGoTo.Add(new Vector3());
                    }
                    int value = i + 1;
                    turretAI.m_positionsToGoTo[i] =
                        EditorGUILayout.Vector3Field("Position " + value, turretAI.m_positionsToGoTo[i]);
                }
            }
        }
    }


}
