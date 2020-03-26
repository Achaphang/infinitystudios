using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beeper : MonoBehaviour
{
    AudioSource audio;
    AudioClip primeClip;
    AudioClip activateClip;
    MonsterController monster;

    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        primeClip = Resources.Load<AudioClip>("Sounds/Items/Beeper/prime");
        activateClip = Resources.Load<AudioClip>("Sounds/Items/Beeper/activate");
        monster = GameObject.Find("Monster").GetComponent<MonsterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Prime() {
        audio.clip = primeClip;
        StartCoroutine(Beep());
    }

    IEnumerator Beep() {
        for(int i = 0; i < 5; i++) {
            audio.Play();
            yield return new WaitForSeconds(1);
        }

        Activate();
    }

    IEnumerator Activate() {
        for(int i = 0; i < 15; i++) {
            audio.Play();
            monster.AddTarget(gameObject, 2);
            yield return new WaitForSeconds(1);
        }
    }
}
