using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Beeper : MonoBehaviour
{
    AudioSource audio;
    AudioClip primeClip;
    AudioClip activateClip;
    AudioClip[] explosionClips;
    List<MonsterController> monsters;

    bool primed = false;

    // Start is called before the first frame update
    void Start()
    {
        monsters = new List<MonsterController>();
        audio = GetComponent<AudioSource>();
        primeClip = Resources.Load<AudioClip>("Sounds/Items/Beeper/prime");
        activateClip = Resources.Load<AudioClip>("Sounds/Items/Beeper/activate");
        explosionClips = Resources.LoadAll<AudioClip>("Sounds/Misc/Explosions");
        //monsters = GameObject.Find("Monster").GetComponent<MonsterController>();
        foreach(GameObject m in GameObject.FindGameObjectsWithTag("Monster")) {
            if (m.GetComponent<MonsterController>() != null)
                monsters.Add(m.GetComponent<MonsterController>());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Prime() {
        if (primed)
            return;
        primed = true;
        audio.clip = primeClip;
        StartCoroutine(Beep());
    }

    IEnumerator Beep() {
        for(int i = 0; i < 5; i++) {
            audio.Play();
            foreach(MonsterController m in monsters)
                m.AddTarget(gameObject, 3);
            yield return new WaitForSeconds(.75f);
        }
        audio.clip = activateClip;
        audio.pitch = 1.75f;
        StartCoroutine(Activate());
    }

    IEnumerator Activate() {
        for(int i = 0; i < 30; i++) {
            audio.Play();
            foreach (MonsterController m in monsters)
                m.AddTarget(gameObject, 2);
            yield return new WaitForSeconds(.33f);
        }

        StartCoroutine(Explode());
    }

    IEnumerator Explode() {
        audio.clip = explosionClips[Random.Range(0, explosionClips.Length)];
        audio.pitch = 1;
        audio.Play();
        transform.GetChild(0).GetChild(0).gameObject.SetActive(false);
        transform.GetChild(0).GetChild(1).gameObject.SetActive(false);

        while (audio.isPlaying) {
            yield return new WaitForSeconds(.1f);
        }

        Destroy(gameObject);
    }
}
