using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// BehaviorReaderEditor (Vestigial - Operations passed to UnitIntelligence)
// • customizes the Inspector for the BehaviorReaderEditor
//   · provides Inspector variables for accessing an Action Set file and writing it to a Unit
//   · includes buttons for read/write of the Action Set file in Editor mode
[CustomEditor(typeof(BehaviorSetReader))]
[CanEditMultipleObjects]
public class BehaviorReaderEditor : Editor
{
    //// variables //

    //// variables for: Action Set read/write //
    //SerializedProperty actionSetFile;          // setting: file to read/write for assc. Unit Intelligence's Actions
    //SerializedProperty unitIntelligenceScript; // setting: Unit Intelligence script to write to




    //// updating //

    
    //// on enabling of the BehaviorSetReader: //
    //private void OnEnable()
    //{
    //    actionSetFile = serializedObject.FindProperty("actionSetFile");
    //    unitIntelligenceScript = serializedObject.FindProperty("unitIntelligenceScript");
    //}

    //// on opening the BehaviorSetReader's Inspector: //
    //public override void OnInspectorGUI()
    //{
    //    //initializes variable fields in Inspector
    //    EditorGUILayout.PropertyField(actionSetFile, true);
    //    EditorGUILayout.PropertyField(unitIntelligenceScript, true);

    //    serializedObject.ApplyModifiedProperties();

    //    // sets up the buttons
    //    BehaviorSetReader myScript = (BehaviorSetReader)target;
    //    if (GUILayout.Button("Read in Behavior Set"))
    //        myScript.ReadInBehaviors();
    //    if (GUILayout.Button("Write Behavior Set to File"))
    //        myScript.WriteOutBehaviors();
    //}
}
