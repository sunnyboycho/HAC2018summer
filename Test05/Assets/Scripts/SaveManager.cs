using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class SaveManager {

    public static void SaveProgress(ProgressManager progressManager)
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Create);

        ProgressData data = new ProgressData(progressManager);

        bf.Serialize(stream, data);
        stream.Close();
    }

    public static int LoadProgress()
    {
        if (File.Exists(Application.persistentDataPath + "/player.sav"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream stream = new FileStream(Application.persistentDataPath + "/player.sav", FileMode.Open);

            ProgressData data = bf.Deserialize(stream) as ProgressData;

            stream.Close();

            return data.progress;
        }
        else
        {
            Debug.LogError("File does not exist.");
            return 0;
        }
    }
}

[Serializable]
public class ProgressData
{
    public int progress;

    public ProgressData(ProgressManager progressManager)
    {
        progress = progressManager.progress;
    }
}