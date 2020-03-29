using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    float seconds = 0.0f;
    int minutes = 0;

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;
        if (seconds >= 60.0f) {
            minutes++;
            seconds -= 60.0f;
        }
        if (minutes < 10) {
            timerText.text = "0" + minutes.ToString() + ":" + seconds.ToString("F2");

        } else {
            timerText.text = minutes.ToString() + ":" + seconds.ToString("F2");
        }
    }
}
