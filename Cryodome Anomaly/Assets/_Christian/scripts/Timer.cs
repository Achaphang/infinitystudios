using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

/* Timer class for tracking time (i.e. current score)
 */
public class Timer : MonoBehaviour
{
    public Text timerText;
    protected float seconds = 0.0f;
    int minutes = 0;
    string time = "";
    public string finalTime = "";
    public Text bestTime;
    public string best = "";

    // Update is called once per frame
    void Update()
    {
        if (finalTime == "") {
            updateTime();
        }
    }
    
    // Tick the time forward
    void updateTime() {
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
        // Format the time to 2 decimal places
        time += seconds.ToString("F2");
        timerText.text = time;
    }
    
    // Stop timer if timer has not yet been stopped
    public void stopTimer(bool won) {
        if (finalTime == "") {
            updateTime();
            finalTime = time;
            if (won) {
                if (String.Compare(finalTime, best) < 0 || best == "") {
                    best = finalTime;
                    bestTime.text = "NEW BEST: " + best;
                    SavingSystem.SaveScore(this);
                }
            }
        }
    }
    
    // Reset the timer
    public void resetTimer() {
        finalTime = "";
        time = "";
    }
}
