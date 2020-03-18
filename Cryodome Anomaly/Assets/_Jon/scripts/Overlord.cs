// This script can be used globally for various functions that need to run but not necessarily on a specific game object.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Overlord : MonoBehaviour { 
    List<AudioSource> audioClips;
    public AudioSource source;
    AudioClip death;


    bool dying = false;
    bool endDying = false;
    float dyingCounter = -1f;
    float scaleCounter = 1f;
    GameObject deathObj;
    SpriteRenderer deathSprite;

    void Start(){
        audioClips = new List<AudioSource>();
        foreach(AudioSource a in GetComponents<AudioSource>()) {
            audioClips.Add(a);
        }
        death = Resources.Load<AudioClip>("Sounds/Misc/death");
        deathObj = transform.GetChild(0).GetChild(0).gameObject;
        deathSprite = deathObj.GetComponent<SpriteRenderer>();
        //soundAlarm();
    }

    public void soundAlarm() {
        audioClips[1].Play();
    }

    public void youDied() {
        Debug.Log("YOU DIED");
        dying = true;
        source.clip = death;
        source.Play();
        transform.GetChild(0).gameObject.SetActive(true);
    }

    void Update() {
        if (dying) {
            beDying();
        }
    }

    void beDying() {
        deathObj.transform.localScale = new Vector3(scaleCounter, scaleCounter, scaleCounter);
        scaleCounter += Time.deltaTime / 5f;
        if (dyingCounter > 0f) {
            if (dyingCounter > 1.05f)
                endDying = true;
            if (endDying)
                dyingCounter -= Time.deltaTime;
            else
                dyingCounter += Time.deltaTime / 4.5f;

        } else if (!endDying) {
            dyingCounter += Time.deltaTime;
        } else {
            dyingCounter -= Time.deltaTime;
        }
        deathSprite.color = new Color(255, 255, 255, dyingCounter);

        if(endDying && dyingCounter < -2.5f) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
