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
    List<string> keypadNames;
    GameObject[] itemSpawns;
    public List<GameObject> items;

    void Awake(){
        keypadNames = new List<string>();
        audioClips = new List<AudioSource>();
        foreach(AudioSource a in GetComponents<AudioSource>()) {
            audioClips.Add(a);
        }
        death = Resources.Load<AudioClip>("Sounds/Misc/death");
        deathObj = transform.GetChild(0).GetChild(0).gameObject;
        deathSprite = deathObj.GetComponent<SpriteRenderer>();

        itemSpawns = GameObject.FindGameObjectsWithTag("ItemMarker");
        GenerateItemSpawns();
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
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            SceneManager.LoadScene(0);
        }
    }

    // For now, just generate random values with specific colors.
    public string generateNewKeypadName(int keypadAccessLevel) {
        string final = "BIG CHUNGUS WAS HERE";
        char c = (char)('A' + Random.Range(0, 26));
        int i = Random.Range(0, 10);
        // In case of duplicate, simply increment. This has the side effect of generating values bigger than 9. Whatevs.
        while(keypadNames.Contains((c + i).ToString())) {
            i++;
        }
        keypadNames.Add((c + i).ToString());
        if (keypadAccessLevel == 0) {
            final = "<b><color=green>" + c + "-" + i + "</color></b>";
        }else if(keypadAccessLevel == 1) {
            final = "<b><color=yellow>" + c + "-" + i + "</color></b>";
        } else if(keypadAccessLevel == 2) {
            final = "<b><color=orange>" + c + "-" + i + "</color></b>";
        }else if(keypadAccessLevel == 3) {
            final = "<b><color=red>" + c + "-" + i + "</color></b>";
        }else if(keypadAccessLevel == 4) {
            final = "<b><color=black>" + c + "-" + i + "</color></b>";
        }

        return (final);
    }

    void GenerateItemSpawns() {
        for(int i = 0; i < itemSpawns.Length; i++) {
            int chance = itemSpawns[i].GetComponent<ItemMarker>().GetOdds();
            GameObject specificItem = itemSpawns[i].GetComponent<ItemMarker>().GetItem();
            if(Random.Range((int)1, (int)101) <= chance) {
                if(specificItem != null) {
                    Instantiate(specificItem, itemSpawns[i].transform.position, Quaternion.identity);
                } else {
                    int j = Random.Range(0, items.Count);
                    Instantiate(items[j], itemSpawns[i].transform.position, Quaternion.identity);
                }
                // TODO: do this always once you make sure item markers are working properly. Until then we can see the ones that do not spawn objects.
                itemSpawns[i].SetActive(false);
            }
        }
    }
}
