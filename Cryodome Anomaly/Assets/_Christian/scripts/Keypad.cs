using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    Overlord overlord;
    Light light;
    int[] passcode = new int[4];
    int[] passcodeEntered = new int[4];
    string visiblePasscode;
    int passcodeCounter = 0;
    bool correctPasscode = false;
    bool canPress = true;
    Text txt;
    Text title;
    public DoorAnimation door;

    // Start is called before the first frame update
    void Start()
    {
        overlord = GameObject.Find("Overlord").GetComponent<Overlord>();
        light = GetComponentInChildren<Light>();
        for (int i = 0; i < 4; i ++) {
            passcode[i] = UnityEngine.Random.Range(0, 9);
        }

        txt = transform.Find("KeypadText").GetChild(0).GetComponent<Text>();
        title = transform.Find("KeypadText").GetChild(1).GetComponent<Text>();
        title.text = overlord.generateNewKeypadName(0);
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
        if (!canPress || correctPasscode)
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
        light.color = Color.green;
        canPress = false;
        if(door != null)
            door.SetDoorUnlocked(true);
    }

    public void ClearPasscode() {
        if (correctPasscode)
            return;
        Array.Clear(passcodeEntered, 0, passcodeEntered.Length);
        visiblePasscode = "";
        txt.text = "";
        passcodeCounter = 0;
        canPress = false;
    }

    public void ResetPress() {
        if (correctPasscode)
            return;
        canPress = true;
    }
}
