using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

//Save and load the player position with the character controler
// And after the monobehaviour : IDataPersistence and the LoadData + SaveData 

public class S_GameData
{
    public Vector3 playerPosition;

    public S_GameData()
    {
       // playerPosition = Vector3();
    }
}