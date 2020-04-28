using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ScoreData : MonoBehaviour
{
    public string time;
    public ScoreData(Timer timer) {
        time = timer.finalTime;
    }
}
