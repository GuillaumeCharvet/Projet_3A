using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[CustomEditor(typeof(FlockBehaviour))]
public class SE_DrawFlockPathHandles : Editor
{
    public void OnSceneGUI()
    {
        FlockBehaviour flock = (FlockBehaviour)target;

        Handles.color = Color.red;
        var positions = flock.positions;

        for (int i = 1; i < positions.Count + 1; i++)
        {
            var previousPoint = positions[i - 1];
            var currentPoint = positions[i % positions.Count];

            Handles.DrawDottedLine(previousPoint, currentPoint, 4f);
        }
        for (int i = 0; i < positions.Count; i++)
        {
            positions[i] = Handles.PositionHandle(positions[i], Quaternion.identity);
        }

        if (GUI.changed)
        {
            EditorUtility.SetDirty(flock);
            EditorSceneManager.MarkSceneDirty(flock.gameObject.scene);
        }
    }
}
