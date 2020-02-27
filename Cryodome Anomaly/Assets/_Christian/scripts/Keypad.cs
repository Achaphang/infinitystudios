using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random;

public class Keypad : MonoBehaviour
{
    int[] passcode = new int[4];

    // Start is called before the first frame update
    void Start()
    {
        Random rng = new Random();
        for (int i = 0; i < 4; i ++) {
            passcode[i] = rng.Next(0, 9);
        }
    }
}
