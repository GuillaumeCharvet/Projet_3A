using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(FlockBehaviour2))]
public class SE_DrawFlockPathHandles2 : Editor
{
    private FlockBehaviour2 flock;
    private GUIStyle style = new GUIStyle();

    private bool toggleDisplayControlPointsHandles = false;
    private bool toggleDisplayPathPointsHandles = false;

    public void OnEnable()
    {
        flock = (FlockBehaviour2)target;
        style.normal.textColor = Color.black;
        style.fontSize = 18;
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        GUILayout.BeginHorizontal();

        GUILayout.BeginVertical();

        if (GUILayout.Button("Add point", GUILayout.Width(150), GUILayout.Height(40)))
        {
            flock.AddPoint();
            SceneView.RepaintAll();
        }

        if (GUILayout.Button("Remove point", GUILayout.Width(150), GUILayout.Height(40)))
        {
            flock.RemovePoint();
            SceneView.RepaintAll();
        }

        if (GUILayout.Button("Reset control points", GUILayout.Width(150), GUILayout.Height(40)))
        {
            flock.ResetControlPoints();
            SceneView.RepaintAll();
        }

        if (GUILayout.Button("Recenter points", GUILayout.Width(150), GUILayout.Height(40)))
        {
            flock.RecenterPathPoints();
            SceneView.RepaintAll();
        }

        GUILayout.EndVertical();

        GUILayout.BeginVertical();
        if (GUILayout.Button("Display/Hide control points handles", GUILayout.Width(300), GUILayout.Height(40)))
        {
            toggleDisplayControlPointsHandles = !toggleDisplayControlPointsHandles;
            SceneView.RepaintAll();
        }

        if (GUILayout.Button("Display/Hide path points handles", GUILayout.Width(300), GUILayout.Height(40)))
        {
            toggleDisplayPathPointsHandles = !toggleDisplayPathPointsHandles;
            SceneView.RepaintAll();
        }

        if (GUILayout.Button("Rotate starting position", GUILayout.Width(300), GUILayout.Height(40)))
        {
            flock.RotateStartingPpoint();
            SceneView.RepaintAll();
        }

        GUILayout.EndVertical();

        GUILayout.EndHorizontal();
    }

    public void OnSceneGUI()
    {
        var positions = flock.positions;
        var controlPoints1 = flock.controlPoints1;
        var controlPoints2 = flock.controlPoints2;

        Handles.color = Color.red;

        for (int i = 1; i < positions.Count + 1; i++)
        {
            var previousPoint = positions[i - 1];
            var currentPoint = positions[i % positions.Count];

            Handles.color = Color.red;
            Handles.DrawLine(previousPoint, currentPoint, 4f);

            Handles.Label(positions[i % positions.Count] + Vector3.right, (i % positions.Count).ToString(), style);
        }

        if (toggleDisplayPathPointsHandles)
        {
            for (int i = 0; i < positions.Count; i++)
            {
                Handles.color = Color.red;
                positions[i] = Handles.PositionHandle(positions[i], Quaternion.identity);
            }
        }

        if (toggleDisplayControlPointsHandles)
        {
            Handles.color = Color.yellow;
            for (int i = 1; i < positions.Count + 1; i++)
            {
                var previousPoint = positions[i - 1];
                var currentPoint1 = controlPoints1[i - 1];
                var currentPoint2 = controlPoints2[i - 1];
                var nextPoint = positions[i % positions.Count];

                Handles.DrawDottedLine(previousPoint, currentPoint1, 4f);
                Handles.DrawBezier(previousPoint, nextPoint, currentPoint1, currentPoint2, Color.yellow, null, 3f);
                Handles.DrawDottedLine(currentPoint2, nextPoint, 4f);
            }

            for (int i = 0; i < positions.Count; i++)
            {
                controlPoints1[i] = Handles.PositionHandle(controlPoints1[i], Quaternion.identity);
                controlPoints2[i] = Handles.PositionHandle(controlPoints2[i], Quaternion.identity);
            }
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