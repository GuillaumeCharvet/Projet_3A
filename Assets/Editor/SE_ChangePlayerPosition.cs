using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(S_ChangePlayerPosition))]
public class SE_ChangePlayerPosition : Editor
{
    private S_ChangePlayerPosition cpp;

    void OnEnable()
    {
        cpp = (S_ChangePlayerPosition)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.Space(20f);

        GUILayout.Label("Sélection de la position", EditorStyles.boldLabel);

        GUILayout.Space(20f);

        GUILayout.BeginHorizontal();

        GUILayout.Space(120f);

        GUILayout.BeginVertical();

        foreach (var item in cpp.spawnPoints)
        {
            if (GUILayout.Button(item.name, GUILayout.Width(150), GUILayout.Height(40)))
            {
                cpp.transform.position = item.position;
                cpp.transform.rotation = Quaternion.Euler(item.rotation);

                /*
                var view = SceneView.currentDrawingSceneView;
                if (view != null)
                {
                    Selection.activeGameObject = cpp.gameObject;
                    SceneView.FrameLastActiveSceneView();
                    //view.AlignViewToObject(cpp.transform);
                }
                */
            }
        }

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();

        base.DrawDefaultInspector();
    }
}
