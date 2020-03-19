using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterNoiseController : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    List<AudioSource> audioClips;
    AudioClip[] chasingClips;
    AudioClip[] spottedClips;
    AudioClip[] lostClips;
    AudioClip[] movementClips;

    public AudioSource tempSource;
    public AudioSource movementSource;

    bool playerDead = false;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        audioClips = new List<AudioSource>();

        foreach(AudioSource a in GetComponents<AudioSource>()) {
            audioClips.Add(a);
        }

        chasingClips = Resources.LoadAll<AudioClip>("Sounds/Monster Noises/chasing");
        spottedClips = Resources.LoadAll<AudioClip>("Sounds/Monster Noises/spotted");
        lostClips = Resources.LoadAll<AudioClip>("Sounds/Monster Noises/lost");
        movementClips = Resources.LoadAll<AudioClip>("Sounds/Monster Noises/movement");

        StartCoroutine(breathe());
    }

    void Update() {
        float temp = anim.GetCurrentAnimatorStateInfo(0).normalizedTime % 1;
        if(temp > .23f && temp < .3f && agent.velocity.magnitude > .3f) {
            move();
        }

        if (temp > .7f && temp < .77f && agent.velocity.magnitude > .3f) {
            move();
        }
    }

    IEnumerator breathe() {
        while(true){
            //audioClips[0].Play();
            yield return new WaitForSeconds((audioClips[0].clip.length * 1 / audioClips[0].pitch) + .25f);
            //audioClips[1].Play();
            yield return new WaitForSeconds((audioClips[1].clip.length * 1 / audioClips[1].pitch) + Random.Range(.25f, .26f));
        }
    }

    void move() {
        if(!movementSource.isPlaying && !playerDead) {
            movementSource.clip = movementClips[Random.Range(0, movementClips.Length)];
            movementSource.pitch = 1f + agent.speed / 2f;
            movementSource.Play();
        }
    }

    public void locatedPlayer() {
        if (!tempSource.isPlaying && !playerDead) {
            tempSource.clip = spottedClips[Random.Range(0, spottedClips.Length)];
            tempSource.pitch = Random.Range(.7f, .8f);
            tempSource.Play();
        }

    }
    
    public void chasingPlayer() {
        if (!tempSource.isPlaying && !playerDead) {
            tempSource.clip = chasingClips[Random.Range(0, chasingClips.Length)];
            tempSource.pitch = Random.Range(.7f, .8f);
            tempSource.Play();
        }
    }

    public void lostPlayer() {
        if (!tempSource.isPlaying && !playerDead) {
            tempSource.clip = lostClips[Random.Range(0, lostClips.Length)];
            tempSource.pitch = Random.Range(.7f, .8f);
            tempSource.Play();
        }
    }

    public void commitDie() {
        playerDead = true;
        audioClips[3].Play();
    }
}
