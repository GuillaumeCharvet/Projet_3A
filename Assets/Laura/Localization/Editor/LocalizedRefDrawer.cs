using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(LocalizedStringRef),true)]
public class LocalizedRefDrawer : BasePropertyDrawer
{

    private SerializedProperty idProperty;
    internal override void OnGUIEffect(Rect position, SerializedProperty property)
    {
        idProperty = property.FindPropertyRelative("ID");
        var labelRect = MakeRectForDrawer(0.2f, 0);
        var contentRect = MakeRectForDrawer(0.8f, 0);
        
        EditorGUI.LabelField(labelRect,property.displayName);
        var tempArray = Localization.GetLocalization().GetIDList();
        var tempIndex = Mathf.Max(0,tempArray.ToList().IndexOf(idProperty.stringValue));
        tempIndex = EditorGUI.Popup(contentRect,tempIndex, tempArray);
        idProperty.stringValue = tempArray[tempIndex];

    }
}
