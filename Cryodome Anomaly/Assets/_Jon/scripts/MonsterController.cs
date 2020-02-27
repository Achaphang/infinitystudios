using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    List<Transform> targets;
    // This object is a square box that acts as a target collider for the monster.
    GameObject markerPrefab;
    NavMeshAgent agent;

    // For testing purposes.
    public Transform exampleTransform;

    // Used for running duration.
    float stamina = 100f;
    bool running = false;
    float walkSpeed;
    float runSpeed;

    void Start() {
        // Generates list, must call constructor
        targets = new List<Transform>();
        markerPrefab = Resources.Load("assets/prefabs/markerPrefab") as GameObject;
        agent = GetComponent<NavMeshAgent>();
        targets.Add(exampleTransform);
        walkSpeed = agent.speed;
        runSpeed = walkSpeed * 2;
    }

    void Update() {
        // If targets is empty, do nothing for now.
        if (targets.Count == 0)
            return;

        agent.destination = targets[0].position;

        if (stamina <= 0f)
            stopRunning();
    }

    public void FixedUpdate() {
        if (running)
            stamina -= .1f;
        else if (stamina < 100f)
            stamina += .05f;
    }

    void stopRunning() {
        running = false;
        agent.speed = walkSpeed;
    }

    // Adds a new target with a given priority. Currently only accepts high or low priority. Generates a target to move to.
    // TODO: Allow targets to be placed as child of object, so that they can move, for example to follow the player. Currently static markers only.
    public void AddTarget(Transform targ, int priority) {
        if (priority == 1) {
            targets.Insert(0, targ);
        } else {
            targets.Add(targ);
        }

        Instantiate(markerPrefab, targ.position, Quaternion.identity);
    }

    public void OnTriggerEnter2D(Collider2D collision) {
        // Checks if the monster has reached its primary destination. If so, remove it from the list, remove target marker.
        if(collision.gameObject.tag == "marker") {
            targets.RemoveAt(0);
            Destroy(collision.gameObject);
        }
    }
}
