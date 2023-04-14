using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
