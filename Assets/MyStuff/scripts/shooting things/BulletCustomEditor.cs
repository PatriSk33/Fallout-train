using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(BulletScript))]
public class BulletCustomEditor : Editor
{
    private SerializedProperty speed;
    private SerializedProperty flyTime;

    private SerializedProperty isSniperBullet;
    private SerializedProperty longDistance;
    private SerializedProperty closeDistance;
    private SerializedProperty longDistanceDamage;
    private SerializedProperty mediumDistanceDamage;
    private SerializedProperty closeDistanceDamage;

    private void OnEnable()
    {
        speed = serializedObject.FindProperty("speed");
        flyTime = serializedObject.FindProperty("flyTime");

        isSniperBullet = serializedObject.FindProperty("isSniperBullet");
        longDistance = serializedObject.FindProperty("longDistance");
        closeDistance = serializedObject.FindProperty("closeDistance");
        longDistanceDamage = serializedObject.FindProperty("longDistanceDamage");
        mediumDistanceDamage = serializedObject.FindProperty("mediumDistanceDamage");
        closeDistanceDamage = serializedObject.FindProperty("closeDistanceDamage");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        EditorGUILayout.LabelField("Basic Stats", EditorStyles.boldLabel);
        EditorGUILayout.PropertyField(speed);
        EditorGUILayout.PropertyField(flyTime);
        
        EditorGUILayout.PropertyField(isSniperBullet);

        if (isSniperBullet.boolValue)
        {
            EditorGUILayout.LabelField("Sniper Bullet Stats", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(longDistance, new GUIContent("Long Distance"));
            EditorGUILayout.PropertyField(closeDistance, new GUIContent("Close Distance"));
            EditorGUILayout.PropertyField(longDistanceDamage, new GUIContent("Long Distance Damage"));
            EditorGUILayout.PropertyField(mediumDistanceDamage, new GUIContent("Medium Distance Damage"));
            EditorGUILayout.PropertyField(closeDistanceDamage, new GUIContent("Close Distance Damage"));
        }

        serializedObject.ApplyModifiedProperties();
    }
}
