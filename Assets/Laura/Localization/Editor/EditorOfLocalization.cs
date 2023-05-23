using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Localization))]
public class EditorOfLocalization : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if(GUILayout.Button("GenerateID"))
        {
            ((Localization)serializedObject.targetObject).GenerateList();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
