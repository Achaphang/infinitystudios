using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterVisibilityController : MonoBehaviour
{
    List<GameObject> players;
    MonsterController monsterController;
    bool canSee = false;
    float seeCooldown;

    // Start is called before the first frame update
    void Start()
    {
        players = new List<GameObject>();
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("ActualPlayer")) {
            players.Add(obj);
        }
        monsterController = GetComponentInParent<MonsterController>();
    }

    // Update is called once per frame
    void Update()
    {
        seeCooldown -= Time.deltaTime;
        if (canSee && seeCooldown <= 0f) {
            RaycastHit hit;
            foreach(GameObject p in players) {
                if (Physics.Raycast(transform.position, (p.transform.position - transform.position), out hit)) {
                    monsterController.AddTarget(p, 1);
                    Debug.Log("YUPP");
                    seeCooldown = 1f;
                }
            }
        }
    }

    // Todo: fix this function. Something in the middle of the level is triggering it, find the invisible player object. I'd guess the flashlight?
    public void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "ActualPlayer") {
            canSee = true;
        }
    }

    public void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "ActualPlayer") {
            canSee = false;
        }
    }
}
