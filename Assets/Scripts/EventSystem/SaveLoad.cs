using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class SaveData
{
    public int[,] LinkNode;
}

public class SaveLoad : ISaveLoad<int[,]>
{
    public void SaveData(int[,] linkNode)
    {
        BinaryFormatter bf = new BinaryFormatter();
        SaveData data = new SaveData();
        data.LinkNode = linkNode;
        using (StreamWriter file 
            = File.CreateText(Application.persistentDataPath + "/EventSystem.json"))
        {
            JsonSerializer serializer = new JsonSerializer();
            serializer.Serialize(file, data);
        }
    }

    public int[,] LoadData()
    {
        if (File.Exists(Application.persistentDataPath
    + "/EventSystem.json"))
        {
            using (StreamReader file 
                = File.OpenText(Application.persistentDataPath + "/EventSystem.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                SaveData data = (SaveData)serializer.Deserialize(file, typeof(SaveData));
                int[,] result = data.LinkNode;
                return result;
            }
        }
        else
            return null;
    }

    public void ResetData()
    {
        if (File.Exists(Application.persistentDataPath
          + "/EventSystem.json"))
        {
            File.Delete(Application.persistentDataPath
              + "/EventSystem.json");
        }
    }
}
