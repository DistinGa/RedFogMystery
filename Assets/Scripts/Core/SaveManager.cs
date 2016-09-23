using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public static class SaveManager
{
    public delegate void SMdelegate();
    static List<ISave> objectsToSave = new List<ISave>();

    //Сохранение игры
    public static void Save(string socketname)
    {
        string initPath = Application.streamingAssetsPath + "/Saves/" + socketname + "/";
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream;
        if (!Directory.Exists(initPath))
            Directory.CreateDirectory(initPath);

        //Сохранение данных Гейм Менеджера
        stream = File.Create(initPath + "GMdata.sav");
        try
        {
            bf.Serialize(stream, GameManager.GM.GetGMdata());
            stream.Close();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Ошибка записи данных GameManager " + " " + e.Message);
            stream.Close();
        }

        //Сохранение состояний квестов

        //Сохранение данных отдельных объектов
        List<SavedData> savedDataList = new List<SavedData>();
        foreach (ISave listItem in objectsToSave)
        {
            savedDataList.Add(listItem.GetDataToSave());
        }
        try
        {
            stream = File.Create(initPath + SceneManager.GetActiveScene().name + ".sav");
            bf.Serialize(stream, savedDataList);
            stream.Close();
        }
        catch(System.Exception e)
        {
            Debug.LogError("Ошибка записи объектов " + " " + e.Message);
            stream.Close();
        }
    }

    //Загрузка игры
    //Сделано в корутине, чтобы была возможность дождаться полной загрузки сцены перед восстановлением сохранённых значений.
    public static IEnumerator Load(string socketname)
    {
        string initPath = Application.streamingAssetsPath + "/Saves/" + socketname + "/";
        string filePath;
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream;

        //Загрузка данных Гейм Менеджера
        filePath = initPath + "GMdata.sav";
        if (File.Exists(filePath))
        {
            stream = File.Open(filePath, FileMode.Open);
            SavedGMdata sd = (SavedGMdata)bf.Deserialize(stream);
            stream.Close();
            yield return SceneManager.LoadSceneAsync(sd.sceneName);
            GameManager.GM.SetGMdata(sd);
        }

        LoadTriggers(initPath);
    }

    //Загрузка данных отдельных объектов
    public static void LoadTriggers(string initPath)
    {
        BinaryFormatter bf = new BinaryFormatter();
        string filePath = initPath + SceneManager.GetActiveScene().name + ".sav";
        if (File.Exists(filePath))
        {
            FileStream stream = File.Open(filePath, FileMode.Open);
            List<SavedData> savedDataList = new List<SavedData>();

            savedDataList = (List<SavedData>)bf.Deserialize(stream);
            foreach (ISave listItem in objectsToSave)
            {
                SavedData sd = savedDataList.Find(x => x.ObjectID == listItem.GetObjID());
                if (sd.Data != null)
                    listItem.SetSavedData(sd.Data);
            }
            stream.Close();
        }
    }

    public static void Subscribe(ISave SaveObject)
    {
        objectsToSave.Add(SaveObject);
    }

    public static void Unsubscribe(ISave SaveObject)
    {
        objectsToSave.Remove(SaveObject);
    }
}

[System.Serializable]
public struct SavedData
{
    public int ObjectID;
    public string Data;

    public SavedData(int oID, string data)
    {
        ObjectID = oID;
        Data = data;
    }
}

