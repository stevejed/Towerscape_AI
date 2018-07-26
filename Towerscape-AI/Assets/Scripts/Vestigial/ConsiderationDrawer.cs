//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEditor;

//[CustomPropertyDrawer(typeof(Consideration))]
//public class ConsiderationDrawer : PropertyDrawer
//{
//    // Draw the property inside the given rect
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        // Using BeginProperty / EndProperty on the parent property means that
//        // prefab override logic works on the entire property.
//        Rect newRect = new Rect(position.x + 30, position.y + 100, position.width, position.height);
//        //EditorGUI.BeginProperty(position, label, property);
//        EditorGUI.BeginProperty(newRect, label, property);

//        // Draw label
//        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

//        // Don't make child fields be indented
//        var indent = EditorGUI.indentLevel;
//        EditorGUI.indentLevel = 0;
//        // Calculate rects
//        //var curveTypeRect = new Rect(position.x, position.y, 30, position.height);
//        var inputRect = new Rect(position.x, position.y, position.width, position.height);
//        var curveRect = new Rect(position.x, position.y + 50, position.width, position.height);
//        var amountRect = new Rect(position.x, position.y, 30, position.height);
//        var unitRect = new Rect(position.x + 35, position.y, 50, position.height);
//        var nameRect = new Rect(position.x + 90, position.y, position.width - 90, position.height);
//        var mRect = new Rect(position.x, position.y + 35, position.width - 90, position.height);

//        // Draw fields - passs GUIContent.none to each so they are drawn without labels
//        //EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("name"), GUIContent.none);
//        EditorGUILayout.BeginVertical();
//        EditorGUI.PropertyField(curveRect, property.FindPropertyRelative("curve"), GUIContent.none);
//        EditorGUI.PropertyField(mRect, property.FindPropertyRelative("m"), GUIContent.none);
//        EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("k"), GUIContent.none);
//        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("c"), GUIContent.none);
//        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("b"), GUIContent.none);
//        EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("input"), GUIContent.none);
//        EditorGUILayout.EndVertical();

//        // Set indent back to what it was
//        EditorGUI.indentLevel = indent;
//        EditorGUI.EndProperty();
//    }
//}
