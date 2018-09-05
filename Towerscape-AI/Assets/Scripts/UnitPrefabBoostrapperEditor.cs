using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// UnitPrefabBoostrapperEditor
// • custom editor for a Unit Prefab Bootstrapper script for easier designer editing
//   · features an action set file property for read/write actions from a file
//   · features buttons for executing read/write of file in editor
[CustomEditor(typeof(UnitPrefabBootstrapper))][CanEditMultipleObjects]
public class UnitPrefabBoostrapperEditor : Editor {

    // Updates //

    // on opening of the Unit Intelligence instance GUI: //
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        UnitPrefabBootstrapper myScript = (UnitPrefabBootstrapper)target;
        if (GUILayout.Button("Read in Behavior Set"))
            myScript.ReadInBehaviors();
        if (GUILayout.Button("Write Behavior Set to File"))
            myScript.WriteOutBehaviors();

        serializedObject.ApplyModifiedProperties();
        Repaint();
    }
}
