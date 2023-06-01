using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(FlockBehaviour))]
public class SE_DrawFlockPathHandles : Editor
{
    private FlockBehaviour flock;
    private GUIStyle style = new GUIStyle();

    public void OnEnable()
    {
        flock = (FlockBehaviour)target;
        style.normal.textColor = Color.black;
        style.fontSize = 18;
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        if (GUILayout.Button("Add point", GUILayout.Width(150), GUILayout.Height(40)))
        {
            flock.AddPoint();
        }

        if (GUILayout.Button("Remove point", GUILayout.Width(150), GUILayout.Height(40)))
        {
            flock.RemovePoint();
        }

        if (GUILayout.Button("Reset control points", GUILayout.Width(150), GUILayout.Height(40)))
        {
            flock.ResetControlPoints();
        }
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

            Handles.color = Color.red;
            Handles.DrawLine(previousPoint, currentPoint, 4f);

            Handles.Label(positions[i % positions.Count] + Vector3.right, (i % positions.Count).ToString(), style);
        }

        for (int i = 0; i < positions.Count; i++)
        {
            Handles.color = Color.red;
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
            Handles.DrawBezier(previousPoint, nextPoint, currentPoint, currentPoint, Color.yellow, null, 3f);
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

    /*
    [DrawGizmo(GizmoType.Active)]
    private static void MyDick(FlockBehaviour fl, GizmoType fj)
    {
    }
    */
}