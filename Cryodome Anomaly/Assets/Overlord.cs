// This script can be used globally for various functions that need to run but not necessarily on a specific game object.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Overlord : MonoBehaviour { 
    List<AudioSource> audioClips;

    void Start(){
        audioClips = new List<AudioSource>();
        foreach(AudioSource a in GetComponents<AudioSource>()) {
            audioClips.Add(a);
        }
        //soundAlarm();
    }

    void soundAlarm() {
        audioClips[1].Play();
    }
}
