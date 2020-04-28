using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class SavingSystem : MonoBehaviour
{
    public static void SaveScore(Timer timer) {
        string path = Application.persistentDataPath + "/score.dat";
        FileStream stream = new FileStream(path, FileMode.Create);
        ScoreData data = new ScoreData(timer);
        string json = JsonUtility.ToJson(data);
        
        using (StreamWriter writer = new StreamWriter(stream)) {
            writer.Write(json);
        }
    }
    
    public static ScoreData LoadData() {
        string path = Application.persistentDataPath + "/score.dat";
        if (File.Exists(path)) {
            using (StreamReader reader = new StreamReader(path)) {
                string json = reader.ReadToEnd();
                ScoreData data = JsonUtility.FromJson<ScoreData>(json);
                return data;
            }
        } else {
            Debug.LogError("Save file not found at: " + path);
            return null;
        }
    }
}
