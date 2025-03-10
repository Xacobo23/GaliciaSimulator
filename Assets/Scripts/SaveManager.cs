using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class SaveManager
{
    private static string saveFilePath = Application.persistentDataPath + "/saveData.json";

    public static void SavePlantedCells(Dictionary<SerializableVector3Int, bool> plantedCells)
    {
        string json = JsonUtility.ToJson(new SerializableDictionary<SerializableVector3Int, bool>(plantedCells));
        File.WriteAllText(saveFilePath, json);
    }

    public static Dictionary<SerializableVector3Int, bool> LoadPlantedCells()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            return JsonUtility.FromJson<SerializableDictionary<SerializableVector3Int, bool>>(json).ToDictionary();
        }
        return new Dictionary<SerializableVector3Int, bool>();
    }

    public static void ResetSaveData()
    {
        if (File.Exists(saveFilePath))
        {
            File.Delete(saveFilePath);
        }
    }
}

[System.Serializable]
public class SerializableDictionary<TKey, TValue>
{
    [SerializeField]
    private List<TKey> keys = new List<TKey>();
    [SerializeField]
    private List<TValue> values = new List<TValue>();

    public SerializableDictionary(Dictionary<TKey, TValue> dictionary)
    {
        keys = new List<TKey>(dictionary.Keys);
        values = new List<TValue>(dictionary.Values);
    }

    public Dictionary<TKey, TValue> ToDictionary()
    {
        Dictionary<TKey, TValue> dictionary = new Dictionary<TKey, TValue>();
        for (int i = 0; i < keys.Count; i++)
        {
            dictionary.Add(keys[i], values[i]);
        }
        return dictionary;
    }
}

[System.Serializable]
public struct SerializableVector3Int
{
    public int x;
    public int y;
    public int z;

    public SerializableVector3Int(Vector3Int vector)
    {
        x = vector.x;
        y = vector.y;
        z = vector.z;
    }

    public Vector3Int ToVector3Int()
    {
        return new Vector3Int(x, y, z);
    }
}
