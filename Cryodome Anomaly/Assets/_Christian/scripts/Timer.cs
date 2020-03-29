using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    float seconds = 0.0f;
    int minutes = 0;
    string time;

    // Update is called once per frame
    void Update()
    {
        time = "";
        seconds += Time.deltaTime;
        if (seconds >= 60.0f) {
            minutes++;
            seconds -= 60.0f;
        }
        if (minutes < 10) {
            time += "0";
        }
        time += minutes.ToString() + ":";
        if (seconds < 10) {
            time += "0";
        }
        time += seconds.ToString("F2");
        timerText.text = time;
    }
}
