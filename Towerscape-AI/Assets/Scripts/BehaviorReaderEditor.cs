using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BehaviorSetReader))]
[CanEditMultipleObjects]
public class BehaviorReaderEditor : Editor {

    SerializedProperty set;
    SerializedProperty unitIntelligenceScripts;

    private void OnEnable()
    {
        set = serializedObject.FindProperty("set");
        unitIntelligenceScripts = serializedObject.FindProperty("unitIntelligenceScripts");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(set, true);
        EditorGUILayout.PropertyField(unitIntelligenceScripts, true);

        serializedObject.ApplyModifiedProperties();
    }
}
