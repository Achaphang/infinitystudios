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
        foreach(GameObject obj in GameObject.FindGameObjectsWithTag("Player")) {
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
                    seeCooldown = 1f;
                }
            }
        }
    }

    public void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Player") {
            canSee = true;
        }
    }

    public void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Player") {
            canSee = false;
        }
    }
}
