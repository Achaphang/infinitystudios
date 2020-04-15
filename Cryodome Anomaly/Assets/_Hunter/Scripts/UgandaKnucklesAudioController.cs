using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UgandaKnucklesAudioController : MonoBehaviour
{
    public AudioClip audioClip1;
    public AudioClip audioClip2;
    AudioSource myAudioSource1;
    AudioSource myAudioSource2;
    void Start()
    {
        myAudioSource1 = AddAudio(true, false, 0.2f);
        myAudioSource2 = AddAudio(false, false, 0.3f);
        //StartPlayingSounds();
    }
    public AudioSource AddAudio(bool loop, bool playAwake, float vol)
    {
        AudioSource newAudio = gameObject.AddComponent<AudioSource>();
        //newAudio.clip = clip; 
        //newAudio.loop = loop;
        //newAudio.playOnAwake = playAwake;
        newAudio.volume = vol;
        return newAudio;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ActualPlayer"))
        {
            StartPlayingSounds();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ActualPlayer"))
        {
            StopPlayingSounds();
        }
    }
    void StartPlayingSounds()
    {
        myAudioSource1.clip = audioClip1;
        myAudioSource1.Play();
        myAudioSource2.clip = audioClip2;
        myAudioSource2.Play();
    }

    void StopPlayingSounds()
    {
        myAudioSource1.clip = audioClip1;
        myAudioSource1.Stop();
        myAudioSource2.clip = audioClip2;
        myAudioSource2.Stop();
    }
   

}
