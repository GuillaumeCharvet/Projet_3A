using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class S_ChangePlayerPosition : MonoBehaviour
{
    [Serializable]
    public struct SpawnPoint
    {
        public string name;
        public Vector3 position;
        public Vector3 rotation;
    }

    public SpawnPoint[] spawnPoints;

    public void Start()
    {
        for (int i = 3; i < spawnPoints.Length; i++)
        {
            S_Debugger.AddButton(spawnPoints[i].name, RespawnActionFromPosition(spawnPoints[i].position));
        }
    }

    public UnityAction RespawnActionFromPosition(Vector3 pos)
    {
        return () => { transform.position = pos; };
    }
}