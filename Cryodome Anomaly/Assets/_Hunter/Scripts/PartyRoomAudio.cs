using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyRoomAudio : MonoBehaviour
{

    public AudioClip audioClip1;
    AudioSource myAudioSource1;
    public GameObject disco;

    public AudioSource musicVolume;

    // Start is called before the first frame update
    void Start()
    {
        myAudioSource1 = AddAudio(true, false, 0.2f);  
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
            disco.active = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ActualPlayer"))
        {
            StopPlayingSounds();
            disco.active = false;
        }
    }
    void StartPlayingSounds()
    {
        myAudioSource1.clip = audioClip1;
        myAudioSource1.Play();

        musicVolume.volume = 0;
    }

    void StopPlayingSounds()
    {
        myAudioSource1.clip = audioClip1;
        myAudioSource1.Stop();

        musicVolume.volume = PlayerPrefs.GetFloat("Music Volume");
    }
}
