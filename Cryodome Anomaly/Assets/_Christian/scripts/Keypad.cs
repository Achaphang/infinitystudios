using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keypad : MonoBehaviour
{
    int[] passcode = new int[4];

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i ++) {
            passcode[i] = Random.Range(0, 9);
        }
    }
}
