using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEyeSwap : MonoBehaviour
{
    int counter = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        counter++;
        if(counter == 50) {
            gameObject.layer = 9;
        }

        if(counter == 100) {
            gameObject.layer = 8;
            counter = 0;
        }
    }
}
