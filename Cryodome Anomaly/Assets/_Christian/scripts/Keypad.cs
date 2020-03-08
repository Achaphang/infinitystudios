using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    int[] passcode = new int[4];
    int[] passcodeEntered = new int[4];
    string visiblePasscode;
    int passcodeCounter = 0;
    bool correctPasscode = false;
    bool canPress = true;
    Text txt;

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 4; i ++) {
            passcode[i] = UnityEngine.Random.Range(0, 9);
        }

        txt = transform.FindChild("KeypadText").GetComponentInChildren<Text>();
        txt.text = string.Join("", passcode);
    }

    public void EnterValue(int val) {
        if (!canPress)
            return;
        if (correctPasscode)
            return;
        if(passcodeCounter < 4) {
            canPress = false;
            if (passcodeCounter == 0)
                ClearPasscode();
            passcodeEntered[passcodeCounter] = val;
            visiblePasscode = visiblePasscode.Insert(passcodeCounter, val.ToString());
            passcodeCounter++;
            //txt.text = string.Join("", passcodeEntered);
            txt.text = visiblePasscode;
        }
    }

    public void EnterPasscode() {
        if (!canPress)
            return;
        for(int i = 0; i < 4; i++) {
            if (passcodeEntered[i] != passcode[i]) {
                // Make a really dang loud noise cause you messed up
                txt.text = "<color=red>" + txt.text + "</color>";
                passcodeCounter = 0;
                canPress = false;
                return;
            }
                
        }
        // Do something cool
        correctPasscode = true;
        txt.text = "<color=lime>" + txt.text + "</color>";
        canPress = false;
    }

    public void ClearPasscode() {
        Array.Clear(passcodeEntered, 0, passcodeEntered.Length);
        visiblePasscode = "";
        txt.text = "";
        passcodeCounter = 0;
        canPress = false;
    }

    public void ResetPress() {
        canPress = true;
    }
}
