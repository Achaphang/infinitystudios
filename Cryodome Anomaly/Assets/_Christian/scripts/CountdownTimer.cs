using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : Timer
{
    // Start is called before the first frame update
    public CountdownTimer(float initialTime) {
        seconds = initialTime;
    }
    
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        seconds -= Time.deltaTime;
        if (seconds <= 0.0f) {
            SelfDestruct();
        }
    }
    
    void SelfDestruct() {
        //
    }
}
