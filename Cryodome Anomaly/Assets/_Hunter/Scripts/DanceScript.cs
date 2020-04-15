using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DanceScript : MonoBehaviour
{
    private Animator anim;
    public float timer = 0.0f;
    public int seconds;
    public bool keepTiming = true;
    public int randNum;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();

        InvokeRepeating("TimerOptimized", 0f, 10f);
    }

    // changed functionality in order to make more effecient
    /*void Update()
    {
        Timer();
        //If timer is greater than 10 generate a new random number
        if (timer >= 10)
        {
            randNum = Random.Range(0, 2);
            ResetTimer();
        }

        if (randNum == 0)
        {
            anim.SetInteger("DanceTransitions", 0);
        }
        else if (randNum == 1)
        {
            anim.SetInteger("DanceTransitions", 1);
        }
  
    }*/

    void TimerOptimized() {
        randNum = Random.Range(0, 2);

        if (randNum == 0) {
            anim.SetInteger("DanceTransitions", 0);
        } else if (randNum == 1) {
            anim.SetInteger("DanceTransitions", 1);
        }
    }

    public void Timer()
    {
        // seconds
        timer += Time.deltaTime;
        // turn float seonds to int
        seconds = (int)(timer % 60);
        //print(seconds);
    }


    public void ResetTimer()
    {
        // seconds
        timer += Time.deltaTime;
        // turn float seonds to int
        seconds = (int)(timer % 60);

        if (seconds > 4)
        {
            // both code runs the same
            //timer = Time.deltaTime;
            timer = 0.0f;
        }
    }
}
