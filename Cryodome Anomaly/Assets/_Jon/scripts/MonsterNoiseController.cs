﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterNoiseController : MonoBehaviour
{
    List<AudioSource> audioClips;
    // Start is called before the first frame update
    void Start()
    {
        audioClips = new List<AudioSource>();
        foreach(AudioSource a in GetComponents<AudioSource>()) {
            audioClips.Add(a);
        }
        StartCoroutine(breathe());
        commitDie();
    }

    // Update is called once per frame
    void Update()
    {
        if (!audioClips[2].isPlaying && Random.Range(0, 10000f) > 9989f) {
            locatedPlayer();
        }
    }

    IEnumerator breathe() {
        while(true){
            audioClips[0].Play();
            yield return new WaitForSeconds((audioClips[0].clip.length * 1 / audioClips[0].pitch) + .25f);
            audioClips[1].Play();
            yield return new WaitForSeconds((audioClips[1].clip.length * 1 / audioClips[1].pitch) + Random.Range(.25f, .26f));
        }
    }

    public void locatedPlayer() {
        if (Random.Range(1f, 5f) > 3f)
            audioClips[2].Play();
        else
            audioClips[4].Play();
    }

    public void commitDie() {
        audioClips[3].Play();
    }
}
