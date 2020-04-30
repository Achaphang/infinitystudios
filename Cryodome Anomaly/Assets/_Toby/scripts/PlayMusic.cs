using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayMusic : MonoBehaviour
{
    AudioSource scaryMusic;

    // Start is called before the first frame update
    void Start()
    {
        scaryMusic = GetComponent<AudioSource>();
        scaryMusic.Play();
        scaryMusic.volume = 1;
    }
}
