using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System;
//[CustomEditor(typeof(TurretRotationalGroundAI))]
//[CanEditMultipleObjects]
//public class CustomEditorInspectorGround : Editor
//{
//    TurretRotationalGroundAI turretAI;

//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();
//        turretAI = (TurretRotationalGroundAI)target;
//        if (turretAI != null)
//        {
//            GUILayout.Label("Rotation of Turret");
//            GUILayout.Label("Type of Rotation");
//            turretAI.m_turretMovement =
//                (TurretMovementGround)EditorGUILayout.EnumPopup("Turret movement", turretAI.m_turretMovement);

//            turretAI.m_player = (GameObject)EditorGUILayout.ObjectField("Player", turretAI.m_player,
//                typeof(GameObject), true);
//            turretAI.m_takeDamage = (GameEvent)EditorGUILayout.ObjectField("Player", turretAI.m_takeDamage,
//    typeof(GameEvent), true);
//            turretAI.m_baseTurret =
//                (GameObject)EditorGUILayout.ObjectField("Base of the Turret", turretAI.m_baseTurret,
//                typeof(GameObject), true);

//            turretAI.m_faceTurret =
//    (GameObject)EditorGUILayout.ObjectField("Face of the Turret", turretAI.m_faceTurret,
//    typeof(GameObject), true);

//            turretAI.m_spotLight =
//    (Light)EditorGUILayout.ObjectField("SpotLight", turretAI.m_spotLight,
//    typeof(Light), true);
//            turretAI.m_turretSearchSpeed = EditorGUILayout.FloatField("Search Speed", turretAI.m_turretSearchSpeed);

//            if (turretAI.m_turretMovement == TurretMovementGround.GameObjectToGameObject)
//            {
//                turretAI.gameObjValue = EditorGUILayout.IntField("Amount of Objects", turretAI.gameObjValue);
//                for (int i = 0; i < turretAI.gameObjValue; i++)
//                {
//                    while (turretAI.gameObjValue != turretAI.m_objPosToGoTo.Count) 
//                    {
//                        if(turretAI.gameObjValue < turretAI.m_objPosToGoTo.Count)
//                            turretAI.m_objPosToGoTo.RemoveAt(turretAI.m_objPosToGoTo.Count - 1);
//                        else if(turretAI.gameObjValue > turretAI.m_objPosToGoTo.Count)
//                            turretAI.m_objPosToGoTo.Add(null);
//                    }
//                    int value = i + 1;
//                    turretAI.m_objPosToGoTo[i] = 
//                        (GameObject)EditorGUILayout.ObjectField("Position " + value, turretAI.m_objPosToGoTo[i],
//                        typeof(GameObject), true);
//                }
//            }
//            else if (turretAI.m_turretMovement == TurretMovementGround.PositionToPosition)
//            {
//                turretAI.positionValue = EditorGUILayout.IntField("Amount of Positions", turretAI.positionValue);
//                for (int i = 0; i < turretAI.positionValue; i++)
//                {
//                    while (turretAI.positionValue != turretAI.m_positionsToGoTo.Count)
//                    {
//                        if (turretAI.positionValue < turretAI.m_positionsToGoTo.Count)
//                            turretAI.m_positionsToGoTo.RemoveAt(turretAI.m_positionsToGoTo.Count - 1);
//                        else if (turretAI.positionValue > turretAI.m_positionsToGoTo.Count)
//                            turretAI.m_positionsToGoTo.Add(new Vector3());
//                    }
//                    int value = i + 1;
//                    turretAI.m_positionsToGoTo[i] =
//                        EditorGUILayout.Vector3Field("Position " + value, turretAI.m_positionsToGoTo[i]);
//                }
//            }
//        }
//        if (GUILayout.Button("Save"))
//        {
//            EditorUtility.SetDirty(turretAI);
//        }
//    }
//}
//[CustomEditor(typeof(TurretRotationalWallAI))]
//[CanEditMultipleObjects]
//public class CustomEditorInspectorWall : Editor
//{
//    TurretRotationalWallAI turretAI;

//    public override void OnInspectorGUI()
//    {
//        serializedObject.Update();
//        turretAI = (TurretRotationalWallAI)target;
//        if (turretAI != null)
//        {
//            GUILayout.Label("Rotation of Turret");
//            GUILayout.Label("Type of Rotation");
//            turretAI.m_turretMovement =
//                (TurretMovementWall)EditorGUILayout.EnumPopup("Turret movement", turretAI.m_turretMovement);

//            turretAI.m_player = (GameObject)EditorGUILayout.ObjectField("Player", turretAI.m_player,
//                typeof(GameObject), true);
//            turretAI.m_takeDamage = (GameEvent)EditorGUILayout.ObjectField("Player", turretAI.m_takeDamage,
//    typeof(GameEvent), true);

//            turretAI.m_faceTurret =
//                (GameObject)EditorGUILayout.ObjectField("Face of the Turret", turretAI.m_faceTurret,
//                typeof(GameObject), true);
//            turretAI.m_spotLight =
//(Light)EditorGUILayout.ObjectField("SpotLight", turretAI.m_spotLight,
//typeof(Light), true);
//            turretAI.m_turretSearchSpeed = EditorGUILayout.FloatField("Search Speed", turretAI.m_turretSearchSpeed);

//            if (turretAI.m_turretMovement == TurretMovementWall.PositionToPosition)
//            {
//                turretAI.positionValue = EditorGUILayout.IntField("Amount of Positions", turretAI.positionValue);
//                for (int i = 0; i < turretAI.positionValue; i++)
//                {
//                    while (turretAI.positionValue != turretAI.m_rotationOfTurret.Count)
//                    {
//                        if (turretAI.positionValue < turretAI.m_rotationOfTurret.Count)
//                            turretAI.m_rotationOfTurret.RemoveAt(turretAI.m_rotationOfTurret.Count - 1);
//                        else if (turretAI.positionValue > turretAI.m_rotationOfTurret.Count)
//                            turretAI.m_rotationOfTurret.Add(new Vector3());
//                    }
//                    int value = i + 1;
//                    turretAI.m_rotationOfTurret[i] =
//                        EditorGUILayout.Vector3Field("Position " + value, turretAI.m_rotationOfTurret[i]);
//                }
//            }
//        }
//        if (GUILayout.Button("Save"))
//        {
//            EditorUtility.SetDirty(turretAI);
//        }
//    }
//}

