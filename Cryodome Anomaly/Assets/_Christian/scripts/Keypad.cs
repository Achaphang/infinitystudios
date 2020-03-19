using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    Overlord overlord;
    Light keypadLight;
    int[] passcode = new int[4];
    int[] passcodeEntered = new int[4];
    string visiblePasscode;
    int passcodeCounter = 0;
    public bool correctPasscode = false;
    public int accessLevel = 0;
    bool canPress = true;
    Text txt;
    Text title;
    public DoorAnimation door;

    AudioSource keypadSource;
    AudioClip beepClip;

    void Start()
    {
        overlord = GameObject.Find("Overlord").GetComponent<Overlord>();
        keypadLight = GetComponentInChildren<Light>();
        for (int i = 0; i < 4; i ++) {
            passcode[i] = UnityEngine.Random.Range(0, 9);
        }

        txt = transform.Find("KeypadText").GetChild(0).GetComponent<Text>();
        title = transform.Find("KeypadText").GetChild(1).GetComponent<Text>();
        title.text = overlord.generateNewKeypadName(accessLevel);
        txt.text = string.Join("", passcode);
        if (correctPasscode) {
            txt.text = "<color=lime>" + txt.text + "</color>";
        }

        keypadSource = GetComponent<AudioSource>();
        beepClip = Resources.Load<AudioClip>("Sounds/Misc/button");
    }

    public void EnterValue(int val) {
        if (!canPress || correctPasscode)
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
        if (correctPasscode)
            return;

        for (int i = 0; i < 4; i++) {
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
        keypadLight.color = Color.green;
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

    public void beep() {
        keypadSource.clip = beepClip;
        keypadSource.Play();
    }
}
