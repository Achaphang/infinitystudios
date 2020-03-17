using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterNoiseController : MonoBehaviour
{
    List<AudioSource> audioClips;
    AudioClip[] chasingClips;
    AudioClip[] spottedClips;
    AudioClip[] lostClips;

    public AudioSource tempSource;
    
    void Start()
    {
        audioClips = new List<AudioSource>();

        foreach(AudioSource a in GetComponents<AudioSource>()) {
            audioClips.Add(a);
        }

        chasingClips = Resources.LoadAll<AudioClip>("Sounds/Monster Noises/chasing");
        spottedClips = Resources.LoadAll<AudioClip>("Sounds/Monster Noises/spotted");
        lostClips = Resources.LoadAll<AudioClip>("Sounds/Monster Noises/lost");

        StartCoroutine(breathe());
    }

    IEnumerator breathe() {
        while(true){
            //audioClips[0].Play();
            yield return new WaitForSeconds((audioClips[0].clip.length * 1 / audioClips[0].pitch) + .25f);
            //audioClips[1].Play();
            yield return new WaitForSeconds((audioClips[1].clip.length * 1 / audioClips[1].pitch) + Random.Range(.25f, .26f));
        }
    }

    public void locatedPlayer() {
        /*if (audioClips[2].isPlaying || audioClips[4].isPlaying)
            return;

        if (Random.Range(1f, 5f) > 3f)
            audioClips[2].Play();
        else
            audioClips[4].Play();*/
        if (!tempSource.isPlaying) {
            tempSource.clip = spottedClips[Random.Range(0, spottedClips.Length)];
            tempSource.Play();
        }

    }
    
    public void chasingPlayer() {
        if (!tempSource.isPlaying) {
            tempSource.clip = chasingClips[Random.Range(0, chasingClips.Length)];
            tempSource.Play();
        }
    }

    public void commitDie() {
        audioClips[3].Play();
    }
}
