using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

//Class data that is saved/loaded
[System.Serializable]
public class SaveData
{
    //REPLACE WITH YOUR VARIABLES
    // ------------------------------------------------
    public int love;
    public int hunger;
    public int tidyNess;
    public System.DateTime dateOfSave;
    public System.DateTime dateOfLoad;
    // ------------------------------------------------
}

// Static so we dont need an instace of the class
public static class SaveGameManager
{
    // Get unity's default save data path per platform and save to specified file
    public static string savePath = Application.persistentDataPath + "/player.sf";
    // Time difference between Load time and Save time
    public static System.TimeSpan loadMinusSave;
    public static void Save (SaveData saveData)
    {   
        // Get the time on save / close;
        saveData.dateOfSave = System.DateTime.Now;
        BinaryFormatter binaryFormatter = new BinaryFormatter ();

        // Create path on device
        FileStream stream = new FileStream (savePath, FileMode.Create);

        // Serialize class to save as binary
        binaryFormatter.Serialize (stream, saveData);
        stream.Close ();
    }

    public static SaveData Load ()
    {

        
        // Check specified path exists
        if (!File.Exists (savePath))
            return null;

        BinaryFormatter binaryFormatter = new BinaryFormatter ();

        FileStream stream = new FileStream (savePath, FileMode.Open);

        // Assign deserialized binary to class var so we can return after closing the stream
        SaveData saveData = binaryFormatter.Deserialize (stream) as SaveData;
        stream.Close ();

        saveData.dateOfLoad = System.DateTime.Now;
        Debug.Log("Saved :" + saveData.dateOfSave);
        Debug.Log("Loaded :" + saveData.dateOfLoad);

        loadMinusSave = saveData.dateOfLoad - saveData.dateOfSave;
        Debug.Log("Time passed in seconds :" + (int)loadMinusSave.TotalSeconds);
        
        return saveData;
    }
}