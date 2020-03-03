using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    int[] passcode = new int[4];
    Text txt;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i ++) {
            passcode[i] = Random.Range(0, 9);
        }

        txt = GetComponentInChildren<Text>();
        txt.text = string.Join("", passcode);
    }
}
