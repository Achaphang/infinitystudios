﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{
    MonsterController[] monsters;

    Overlord overlord;
    Light keypadLight;
    Canvas canvas;
    int[] passcode = new int[4];
    int[] passcodeEntered = new int[4];
    string visiblePasscode;
    int passcodeCounter = 0;
    public bool correctPasscode = false;
    public int accessLevel = 0;
    bool canPress = true;
    Text txt;
    Text title;
    public List<DoorAnimation> doors;
    bool lockedOut = false;

    AudioSource keypadSource;
    AudioClip beepClip;
    AudioClip alarmClip;
    AudioClip successClip;
    AudioClip scanningClip;

    bool keycardScanning = false;
    float keycardScanningDur = 0f;

    void Start(){
        overlord = GameObject.Find("Overlord").GetComponent<Overlord>();
        keypadLight = GetComponentInChildren<Light>();
        canvas = GetComponentInChildren<Canvas>();
        StartCoroutine(LateStart(1));
        if(Globals.Instance != null) {
            if (Globals.Instance.difficulty != -1) {
                for (int i = 0; i < 4; i++) {
                    passcode[i] = UnityEngine.Random.Range(0, 10);
                }
            } else {
                passcode[0] = 0;
                passcode[1] = 5;
                passcode[2] = 1;
                passcode[3] = 3;
            }
        } else {
            for (int i = 0; i < 4; i++) {
                passcode[i] = UnityEngine.Random.Range(0, 10);
            }
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
        alarmClip = Resources.Load<AudioClip>("Sounds/Misc/keypadAlarm");
        successClip = Resources.Load<AudioClip>("Sounds/Misc/keypadSuccess");
        scanningClip = Resources.Load<AudioClip>("Sounds/Misc/scanning");

        monsters = GameObject.FindObjectsOfType<MonsterController>();
    }

    public void Update() {
        if (correctPasscode)
            return;
        if (keycardScanning) {
            keycardScanningDur += Time.deltaTime;
            if (keycardScanningDur >= 3)
                UnlockDoor();
        } else {
            keycardScanningDur = 0f;
        }
    }

    IEnumerator LateStart(float wait) {
        yield return new WaitForSeconds(wait);
        canvas.worldCamera = Camera.current;
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

    public void EnterPasscode(bool keycard = false) {
        if (correctPasscode || lockedOut)
            return;

        for (int i = 0; i < 4; i++) {
            if (passcodeEntered[i] != passcode[i] || keycard) {
                // Make a really dang loud noise cause you messed up
                if (keycard)
                    txt.text = "INVALID ACCESS";
                txt.text = "<color=red>" + txt.text + "</color>";
                passcodeCounter = 0;
                canPress = false;
                StartCoroutine(KeypadFailure());
                return;
            }
        }
        // Do something cool
        UnlockDoor();
    }

    public bool UnlockDoor(bool keycard = false) {
        if (correctPasscode)
            return false;
        correctPasscode = true;
        if (keycard)
            txt.text = "OVERRIDE";
        keycardScanning = false;
        keypadSource.Stop();
        txt.text = "<color=lime>" + txt.text + "</color>";
        keypadLight.color = Color.green;
        canPress = false;
        keypadSource.clip = successClip;
        keypadSource.volume = 1;
        keypadSource.pitch = .75f;
        keypadSource.Play();
        foreach (DoorAnimation d in doors) {
            d.SetDoorUnlocked(true);
            if (keycard)
                d.SetForceOpen(true);
        }
        return true;
    }

    public void ClearPasscode() {
        if (correctPasscode || lockedOut)
            return;

        Array.Clear(passcodeEntered, 0, passcodeEntered.Length);
        visiblePasscode = "";
        txt.text = "";
        passcodeCounter = 0;
        canPress = false;
    }

    public void ResetPress() {
        if (correctPasscode || lockedOut)
            return;
        canPress = true;
    }

    public void beep() {
        if (correctPasscode || lockedOut)
            return;
        keypadSource.clip = beepClip;
        keypadSource.Play();
    }

    public string GetTitle() {
        return title.text;
    }

    public int GetCode() {
        int code = 0;

        for(int i = 0; i < 4; i++) {
            code = code + passcode[i] * (int)Mathf.Pow(10, 3 - i);
        }

        return code;
    }

    public int[] GetCodeArr() {
        return passcode;
    }

    IEnumerator KeypadFailure() {
        lockedOut = true;
        keypadLight.color = Color.red;
        keypadSource.clip = alarmClip;
        keypadSource.volume = .65f;

        foreach(MonsterController monster in monsters)
            monster.AddTarget(gameObject, 2);

        for (int i = 0; i < 15; i++) {
            keypadSource.Play();
            yield return new WaitForSeconds(1);
        }
        keypadSource.volume = .25f;
        keypadLight.color = Color.white;
        lockedOut = false;
        ClearPasscode();
        ResetPress();
    }

    public void KeycardEnter() {
        if (correctPasscode)
            return;
        lockedOut = true;
        keypadSource.clip = scanningClip;
        keypadSource.volume = 1;
        keypadSource.pitch = 1.75f;
        keypadSource.Play();
        keycardScanning = true;

    }

    public void KeycardExit() {
        if (correctPasscode)
            return;
        lockedOut = false;
        keypadSource.Stop();
        keycardScanning = false;
    }
}
