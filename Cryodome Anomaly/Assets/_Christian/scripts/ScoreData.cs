using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreData
{
    public string time;
    
    public ScoreData(Timer timer) {
        time = timer.finalTime;
    }
}
