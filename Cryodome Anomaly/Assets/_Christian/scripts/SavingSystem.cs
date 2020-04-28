using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class SavingSystem : MonoBehaviour
{
    public static void SaveScore(Timer timer) {
        BinaryFormatter formatter = new BinaryFormatter();
        string path = Application.persistentDataPath + "/score.dat";
        FileStream stream = new FileStream(path, FileMode.Create);
        
        ScoreData data = new ScoreData(timer);
        
        formatter.Serialize(stream, data);
        stream.Close();
    }
    
    public static ScoreData LoadData() {
        string path = Application.persistentDataPath + "/score.dat";
        if (File.Exists(path)) {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            ScoreData data = formatter.Deserialize(stream) as ScoreData;
            stream.Close();
            
            return data;
        } else {
            Debug.LogError("Save file not found at: " + path);
            return null;
        }
    }
}
