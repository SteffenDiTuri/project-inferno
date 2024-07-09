using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void SavePlayer(Player player)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = null;

        try
        {
            stream = new FileStream(path, FileMode.Create);
            PlayerData data = new PlayerData(player);
            formatter.Serialize(stream, data);
            Debug.Log("SAVED");
        }
        catch (System.Exception e)
        {
            Debug.Log("Failed to save player data: " + e.Message);
        }
        finally
        {
            if (stream != null)
            {
                stream.Close();
                Debug.Log("Stream Closed");
            }
        }
    }

    public static PlayerData LoadPlayer()
    {
        string path = Application.persistentDataPath + "/player.save";
        FileStream stream = null;

        try
        {
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                stream = new FileStream(path, FileMode.Open);
                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                Debug.Log("LOADED");
                return data;
            }
            else
            {
                Debug.Log("Save file not found in " + path);
                return null;
            }
        }
        catch (System.Exception e)
        {
            Debug.Log("Failed to load player data: " + e.Message);
            return null;
        }
        finally
        {
            if (stream != null)
            {
                stream.Close();
                Debug.Log("Stream Closed");
            }
        }
    }
}
