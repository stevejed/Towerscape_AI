using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// UnitIntelligenceEditor
// • custom editor for a Unit Intelligence script for easier designer editing
//   · features an action set file property for read/write actions from a file
//   · features buttons for executing read/write of file in editor
//   · enumerates all Unit Intelligence's actions for easy modification
[CustomEditor(typeof(UnitIntelligence))][CanEditMultipleObjects]
public class UnitIntelligenceEditor : Editor {

    // Variables //

    // variables for: action modification and visualization //
    SerializedProperty actions;       //connection - automatic: actions of the given Unit Intelligence
    SerializedProperty actionSetFile; //connection - automatic: file of actions to read/write to




    // Updates //

    // on enabling of the Unit Intelligence instance: //
    private void OnEnable()
    {
        actions = serializedObject.FindProperty("actions");
        actionSetFile = serializedObject.FindProperty("actionSetFile");
    }

    // on opening of the Unit Intelligence instance GUI: //
    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(actionSetFile, true);

        UnitIntelligence myScript = (UnitIntelligence) target;
        if (GUILayout.Button("Read in Behavior Set"))
            myScript.ReadInBehaviors();
        if (GUILayout.Button("Write Behavior Set to File"))
            myScript.WriteOutBehaviors();

        EditorGUILayout.PropertyField(actions, true);

        serializedObject.ApplyModifiedProperties();
        Repaint();
    }
}
