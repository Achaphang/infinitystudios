using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterNoiseController : MonoBehaviour
{
    NavMeshAgent agent;
    Animator anim;
    AudioClip[] chasingClips;
    AudioClip[] spottedClips;
    AudioClip[] lostClips;
    AudioClip[] movementClips;
    AudioClip willhelm;

    public AudioSource tempSource;
    public AudioSource movementSource;

    int monsterType;

    bool playerDead = false;
    
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();

        chasingClips = Resources.LoadAll<AudioClip>("Sounds/Monster Noises/chasing");
        spottedClips = Resources.LoadAll<AudioClip>("Sounds/Monster Noises/spotted");
        lostClips = Resources.LoadAll<AudioClip>("Sounds/Monster Noises/lost");
        movementClips = Resources.LoadAll<AudioClip>("Sounds/Monster Noises/movement");
        willhelm = Resources.Load<AudioClip>("Sounds/Monster Noises/unsorted/willhelm");

        monsterType = GetComponent<MonsterController>().monsterType;
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

    void move() {
        if(!movementSource.isPlaying && !playerDead) {
            movementSource.clip = movementClips[Random.Range(0, movementClips.Length)];
            if (monsterType == 2)
                movementSource.volume = .1f;
            movementSource.pitch = 1f + agent.speed / 2f;
            movementSource.Play();
        }
    }

    public void locatedPlayer() {
        if (!tempSource.isPlaying && !playerDead && monsterType != 2) {
            tempSource.clip = spottedClips[Random.Range(0, spottedClips.Length)];
            tempSource.pitch = Random.Range(.7f, .8f) + (monsterType / 3f);
            tempSource.Play();
        }

    }
    
    public void chasingPlayer() {
        if (!tempSource.isPlaying && !playerDead && monsterType != 2) {
            tempSource.clip = chasingClips[Random.Range(0, chasingClips.Length)];
            tempSource.pitch = Random.Range(.7f, .8f) + (monsterType / 3f);
            tempSource.Play();
        }
    }

    public void lostPlayer() {
        if (!tempSource.isPlaying && !playerDead && monsterType != 2) {
            tempSource.clip = lostClips[Random.Range(0, lostClips.Length)];
            tempSource.pitch = Random.Range(.7f, .8f) + (monsterType / 3f);
            tempSource.Play();
        }
    }

    public void commitDie() {
        playerDead = true;
        tempSource.clip = willhelm;
        tempSource.pitch = 1;
        tempSource.Play();
    }
}
