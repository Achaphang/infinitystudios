using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

/* System for saving the highest score.
 * The score is saved to a local file
 * where it can be retrieved for a later time.
 * The data is stored as a json string.
 */
public class SavingSystem : MonoBehaviour
{
    // Save best score as json string to local file
    public static void SaveScore(Timer timer) {
        string path = Application.persistentDataPath + "/score.dat";
        FileStream stream = new FileStream(path, FileMode.Create);
        ScoreData data = new ScoreData(timer);
        string json = JsonUtility.ToJson(data);
        
        using (StreamWriter writer = new StreamWriter(stream)) {
            writer.Write(json);
        }
    }
    
    // Load best score, null if non existent
    public static ScoreData LoadData() {
        string path = Application.persistentDataPath + "/score.dat";
        if (File.Exists(path)) {
            using (StreamReader reader = new StreamReader(path)) {
                string json = reader.ReadToEnd();
                ScoreData data = JsonUtility.FromJson<ScoreData>(json);
                return data;
            }
        } else {
            FileStream stream = new FileStream(path, FileMode.Create);
            return null;
        }
    }
}
