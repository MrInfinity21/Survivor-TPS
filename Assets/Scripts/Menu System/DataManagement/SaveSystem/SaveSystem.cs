using UnityEngine;
using System.IO;
using UnityEditor;
public static class SaveSystem 
{
    private static string saveFilePath => Application.persistentDataPath + "/playerdata.json";
    
    public static void SavePlayer(Vector3 position)
    {
        PlayerData data = new PlayerData(position);
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(saveFilePath, json);
        Debug.Log("Player position saved: " + position);
    }

    public static Vector3 LoadPlayer()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            Vector3 loadedPosition = data.GetPosition();
            Debug.Log("Player position loaded: " + loadedPosition);
            return loadedPosition;
        }
        else
        {
            Debug.LogWarning("No save file found.");
            return Vector3.zero;
        }
    }

    public static bool HasSavedData()
    {
        return File.Exists(saveFilePath);
    }
    
}
