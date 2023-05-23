using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(LocalizedCollection))]
public class LocalizedSentencePropertyDrawer : BasePropertyDrawer
{

    private SerializedProperty _serializedPropertyList;
    internal override void AtStartOfGUI(SerializedProperty property)
    {
        _serializedPropertyList = property.FindPropertyRelative("contents");
        numberOfLine = Enum.GetValues(typeof(Language)).Length;
        
    }

    internal override void OnGUIEffect(Rect position, SerializedProperty property)
    {
        List<Rect> languageRects = new List<Rect>(), contentRect=new List<Rect>();
        
        for (int i = 0; i < numberOfLine; i++)
        {
            languageRects.Add(MakeRectForDrawer(i, 0.1f, 1, 0));
            contentRect.Add( MakeRectForDrawer(i, 0.9f, 1, 0));
        }
        if (_serializedPropertyList.arraySize != Enum.GetValues(typeof(Language)).Length)
        {
            _serializedPropertyList.ClearArray();
            int indexDebug = 0;
            foreach (var language in Enum.GetValues(typeof(Language)))
            {
                _serializedPropertyList.InsertArrayElementAtIndex(indexDebug);
                _serializedPropertyList.GetArrayElementAtIndex(indexDebug).FindPropertyRelative("language")
                    .enumValueFlag = indexDebug;
            }
        }
        
        
        int index = 0;
        foreach (var language in Enum.GetValues(typeof(Language)))
        {
            var _contentProperty =
                _serializedPropertyList.GetArrayElementAtIndex(index).FindPropertyRelative("content");
            EditorGUI.LabelField(languageRects[index],language.ToString());
            EditorGUI.PropertyField(contentRect[index], _contentProperty, new GUIContent(""));
            
            index++;
        }
            
        property.serializedObject.ApplyModifiedProperties();
    }
}
