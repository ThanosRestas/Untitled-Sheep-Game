using System.Collections;
using System.Collections.Generic;
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
    // List<Vector3> can't be serialized for some reason in Unity so I
    // had to create a custom type via the PoopPosition class
    public List<PoopPosition> poopPositions;
    // ------------------------------------------------

}

[System.Serializable]
public class PoopPosition
{
    public float x;
    public float y;
    public float z;

    public PoopPosition (float x1, float y1, float z1)
    {
        x = x1;
        y = y1;
        z = z1;
    }

    public Vector3 returnVector ()
    {
        Vector3 poopVector;
        poopVector.x = x;
        poopVector.y = y;
        poopVector.z = z;

        return poopVector;
    }

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

        Debug.Log (Application.persistentDataPath);
        // Check specified path exists
        if (!File.Exists (savePath))
            return null;

        BinaryFormatter binaryFormatter = new BinaryFormatter ();

        FileStream stream = new FileStream (savePath, FileMode.Open);

        // Assign deserialized binary to class var so we can return after closing the stream
        SaveData saveData = binaryFormatter.Deserialize (stream) as SaveData;
        stream.Close ();

        saveData.dateOfLoad = System.DateTime.Now;
        Debug.Log ("Saved :" + saveData.dateOfSave);
        Debug.Log ("Loaded :" + saveData.dateOfLoad);

        loadMinusSave = saveData.dateOfLoad - saveData.dateOfSave;
        Debug.Log ("Time passed in seconds :" + (int) loadMinusSave.TotalSeconds);

        return saveData;
    }

}