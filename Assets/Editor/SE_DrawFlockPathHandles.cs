using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(FlockBehaviour))]
public class SE_DrawFlockPathHandles : Editor
{
    private FlockBehaviour flock;

    public void OnEnable()
    {
        flock = (FlockBehaviour)target;        
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Reset control points", GUILayout.Width(150), GUILayout.Height(40)))
        {
            flock.ResetControlPoints();
        }

        base.DrawDefaultInspector();
    }

    public void OnSceneGUI()
    {
        var positions = flock.positions;
        var controlPoints = flock.controlPoints;

        Handles.color = Color.red;
        for (int i = 1; i < positions.Count + 1; i++)
        {
            var previousPoint = positions[i - 1];
            var currentPoint = positions[i % positions.Count];

            Handles.DrawLine(previousPoint, currentPoint, 4f);
        }
        for (int i = 0; i < positions.Count; i++)
        {
            positions[i] = Handles.PositionHandle(positions[i], Quaternion.identity);
        }

        Handles.color = Color.yellow;
        for (int i = 1; i < positions.Count + 1; i++)
        {
            var previousPoint = positions[i - 1];
            var currentPoint = controlPoints[i - 1];
            var nextPoint = positions[i % positions.Count];

            Handles.DrawDottedLine(previousPoint, currentPoint, 4f);
            Handles.DrawDottedLine(currentPoint, nextPoint, 4f);
        }
        for (int i = 0; i < positions.Count; i++)
        {
            controlPoints[i] = Handles.PositionHandle(controlPoints[i], Quaternion.identity);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(flock);
            EditorSceneManager.MarkSceneDirty(flock.gameObject.scene);
        }
    }
}
