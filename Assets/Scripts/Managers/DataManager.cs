using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[Serializable]
public class Data
{
    [Serializable]
    public struct ClearData
    {
        public ulong stageID;
        public bool clear;
        public string dateTimeString;
    }

    public List<ClearData> clearDataList = new List<ClearData>();

    public void RecordStageClearLog(ulong stageID)
    {
        for (int i = 0; i < clearDataList.Count; i++)
        {
            if (clearDataList[i].stageID == stageID)
            {
                return;
            }
        }

        ClearData clearData;

        clearData.stageID = stageID;
        clearData.clear = true;
        clearData.dateTimeString = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss}";

        clearDataList.Add(clearData);
    }

    public string GetClearDateStringWithStageID(ulong stageID)
    {
        for (int i = 0; i < clearDataList.Count; i++)
        {
            if (clearDataList[i].stageID == stageID)
            {
                return clearDataList[i].dateTimeString;
            }
        }

        return null;
    }
}


public class DataManager : MonoBehaviour
{
    private static string dataPath;

    public static Data Data { get; private set; } = new Data();

    private void Awake()
    {
        dataPath = Path.Combine(Application.persistentDataPath, "Data.bwd");
    }

    public static void Save()
    {
        WriteJSON();
    }

    private static void WriteJSON()
    {
        string json = JsonUtility.ToJson(Data);

        File.WriteAllText(dataPath, json);
    }

    public static void Load()
    {
        if (!ReadJSON())
        {
            Data = new Data();
        }
    }

    private static bool ReadJSON()
    {
        if (!File.Exists(dataPath))
        {
            return false;
        }

        string json = File.ReadAllText(dataPath);

        Data = JsonUtility.FromJson<Data>(json);

        return true;
    }
}
