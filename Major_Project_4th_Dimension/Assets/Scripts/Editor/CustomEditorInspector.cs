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

            if (turretAI.m_turretMovement == TurretMovement.GameObjectToGameObject)
            {
                for (int i = 0; i < turretAI.m_posToGoTo.Count; i++)
                {
                    int value = i + 1;
                    turretAI.m_posToGoTo[i] = 
                        (GameObject)EditorGUILayout.ObjectField("Position " + value, turretAI.m_posToGoTo[i],
                        typeof(GameObject), false);
                }
            }
            else if (turretAI.m_turretMovement == TurretMovement.PositionToPosition || turretAI.m_turretMovement == TurretMovement.RotatingAround)
            {
                turretAI.m_minBaseRotation = EditorGUILayout.Vector3Field("Minimum Base Rotation Position", turretAI.m_minBaseRotation);
                turretAI.m_maxBaseRotation = EditorGUILayout.Vector3Field("Maximum Base Rotation Position", turretAI.m_maxBaseRotation);
                turretAI.m_minBodyRotation = EditorGUILayout.Vector3Field("Minimum Body Rotation Position", turretAI.m_minBodyRotation);
                turretAI.m_maxBodyRotation = EditorGUILayout.Vector3Field("Maximum Body Rotation Position", turretAI.m_maxBodyRotation);
            }
        }
    }


}
