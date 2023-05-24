using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Playables;
// LINQ : https://unity3d.college/2017/07/01/linq-unity-developers/

[System.Serializable]

public interface S_DataPersistence : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;


    private GameData gameData;
    private List<S_DataPersistence> dataPersistenceObjects;

    private FileDataHandler dataHandler;
    public static GameData instance { get; private set; }

    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one data");
        }
        instance = this;
    }

    private void Start()
    {
        //Application.persistentDataPath give the operating system standard directory
        // for persisting data in a unity projet
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistenceObjects = FindAllDataPersistenceObjects;
        //I don't understand this ^^

        //Game loaded when starded
        LoadGame();
    }

    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void LoadGame()
    {
        //load any saved data from a file using the data handler
        this.gameData = dataHandler.Load();

        //load any saved data. If no data new game
        if (this.gameData == null)
        {
            Debug.Log("No data found.");
            NewGame();
        }

        foreach (S_DataPersistence dataPersistence in dataPersistenceObjects)
        {
            dataPersistenceObjects.LoadData(gameData);
        }
    }

    public void SaveGame()
    {
        //Save the data in a script and update it if needed
        foreach (S_DataPersistence dataPersistence in dataPersistenceObjects)
        {
            dataPersistenceObjects.SaveData(gameData);
        }

        Debug.Log("New game");

        dataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        //save the game if leaving the game  
        SaveGame();
    }

    private List<S_DataPersistence> FindAllDataPersistenceObjects()
    {
        IEnumerable<S_DataPersistence> dataPersistenceObjects = FindAllDataPersistenceObjects()
            .OfType<S_DataPersistence>();

        return new List<S_DataPersistence>(dataPersistenceObjects);
    }
}

