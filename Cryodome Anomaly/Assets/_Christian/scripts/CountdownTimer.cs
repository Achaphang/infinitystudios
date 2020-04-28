using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountdownTimer : Timer
{
    Overlord overlord;
    // Start is called before the first frame update
    public void Start() {
        overlord = GameObject.Find("Overlord").GetComponent<Overlord>();
    }
    
    public CountdownTimer(float initialTime) {
        seconds = initialTime;
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
        overlord.youDied();
    }
}
