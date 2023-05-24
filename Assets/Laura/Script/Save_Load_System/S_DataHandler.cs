using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.Properties;
//CreatingTextFiles

public class S_DataHandler
{
    private string dataDirPath = "";
    private string dataFileName = "";

    public void FileDataHandler(string dataDirPath, string dataFileName)
        
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }

    public S_GameData Load()
    {
        //Using Path.Combine to account for script with different path separators
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        S_GameData loadedData = null;
        if (File.Exists(fullPath))
        {
            try
            {
                string dataToLoad = "";
                using (FileStream stream = new FileStream(fullPath, FileMode.Open))
                                                                //FileMode.Open = Open the JSON to Load the data inside
                {
                    using (StreamReader reader   = new StreamReader(stream))
                    {
                        dataToLoad = reader.ReadToEnd();
                    }
                }

                //Need to deserialize (transforming data in a format Unity can store + reconstruct) the data from JSON back to C#
            loadedData = JsonUtility.FromJson<S_GameData>(dataToLoad);

            }
            catch (Exception e)
            {

                Debug.LogError("Error when trying to load" + fullPath + "/n" + e);
            }
        }
        return loadedData;
    }

    public void Save(S_GameData data)
    {
        //Using Path.Combine to account for script with different path separators
        string fullPath = Path.Combine(dataDirPath, dataFileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            // using JSON to keep the data, the data can be change with Json, better use XOR ? Binary ?
            string dataToStore = JsonUtility.ToJson(data, true);
            //can change the JSON data by putting true as an optional second parameter.
        
            //Using using() ensure that the connection to that file is closed one we're done reading or writing to it.
            using(FileStream stream = new FileStream(fullPath, FileMode.Create))
            {                                               //FileMode.Create = Since we want to create and write to a file
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
        }
        catch (Exception e)
        {
            Debug.LogError("Error when trying to save data file: " + fullPath + "/n" + e);
        }
    }
}
