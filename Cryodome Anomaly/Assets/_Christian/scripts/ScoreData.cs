using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Class for serealizing the json data
 */
[System.Serializable]
public class ScoreData
{
    public string time;
    
    public ScoreData(Timer timer) {
        time = timer.finalTime;
    }
}
