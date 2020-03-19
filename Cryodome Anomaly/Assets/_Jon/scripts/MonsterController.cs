using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    [SerializeField] List<GameObject> targets;
    GameObject priorityTarget;
    // This object is a square box that acts as a target collider for the monster.
    public GameObject markerPrefab;
    NavMeshAgent agent;
    Animator anim;

    // Used for running duration.
    float stamina = 20f;
    float forceIdleCounter = 0f;
    // Used to generate a new marker if the monster is stuck.
    float idleResetCounter = 5f;
    bool running = false;
    float walkSpeed;
    float runSpeed;
    float chaseTimer;

    // Used for boundry traversal
    float doorSpeed;
    bool isTraversing = false;

    bool hasDied = false;
    MonsterNoiseController noiseController;

    void Start() {
        targets = new List<GameObject>();
        noiseController = GetComponent<MonsterNoiseController>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        agent.Warp(new Vector3(-26, 1, -16));
        walkSpeed = agent.speed;
        runSpeed = walkSpeed * 2.5f;
        doorSpeed = agent.speed * .5f;

        GenerateRandomTarget();
    }

    void Update() {
        // This if statement basically checks for all possible times that the monster SHOULD be idle but might not be.
        if(agent.speed == 0 || agent.velocity.magnitude < .3f || targets.Count == 0 || forceIdleCounter > 0f) {
            anim.Play("idle");
        }else if(agent.speed <= walkSpeed) {
            anim.Play("walk");
        } else{
            anim.Play("run");
        }

        if (agent.isOnOffMeshLink && !isTraversing)
            StartCoroutine(TraverseBoundry());
        else if (!agent.isOnOffMeshLink && isTraversing) {
            isTraversing = false;
            agent.speed = walkSpeed;
        }

        // Prevents other things from happening while breaking through a door.
        if (isTraversing)
            return;

        if(agent.velocity.magnitude < .3f) {
            idleResetCounter -= Time.deltaTime;
            if(idleResetCounter <= 0f) {
                Destroy(targets[0]);
                targets.RemoveAt(0);
                if(targets.Count == 0)
                    GenerateRandomTarget();
                idleResetCounter = 5f;
            }
        } else {
            idleResetCounter = 5f;
        }

        forceIdleCounter -= Time.deltaTime;

        if ((targets.Count == 0 && priorityTarget == null) || forceIdleCounter > 0f)
            return;

        if (priorityTarget != null)
            agent.destination = priorityTarget.transform.position;
        else if (forceIdleCounter <= 0f)
            agent.destination = targets[0].transform.position;

        if (!running)
            agent.speed = walkSpeed;
        if (stamina <= 0f)
            StopRunning();
    }

    public void FixedUpdate() {
        if (running && !isTraversing) {
            agent.speed = runSpeed;
            stamina -= Time.deltaTime;
            if (Random.Range(0f, 9999f) > 9888f)
                noiseController.chasingPlayer();
        }

        if (stamina < 20f && !running)
            stamina += Time.deltaTime * 2;

        if(chaseTimer <= 0f && priorityTarget != null) {
            AddTarget(priorityTarget, 2);
            priorityTarget = null;
            noiseController.lostPlayer();
        } else {
            chaseTimer -= Time.deltaTime;
        }
    }

    Vector3 GetRandomLocation() {
        //Calculates and returns triangulation of navmesh containing vertices, triangle indices and navmesh layers
        NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

        // Pick the first instance of a random triangle in the nav mesh
        int t = Random.Range(0, navMeshData.indices.Length - 3);

        // Select a random point on the first instance of a random trangle on the nav mesh
        //Set our point as a fraction inbetween indices[t] and indices[t+1] (Done using lerp)
        Vector3 point = Vector3.Lerp(navMeshData.vertices[navMeshData.indices[t]], navMeshData.vertices[navMeshData.indices[t + 1]], Random.value);
        //lerp our point again with 3rd indice of triangle
        Vector3.Lerp(point, navMeshData.vertices[navMeshData.indices[t + 2]], Random.value);

        return point;
    }

    void GenerateRandomTarget() {
        AddTarget(GetRandomLocation());
    }

    void StartRunning() {
        if (stamina < 20f)
            return;
        running = true;
        //agent.speed = runSpeed;
    }

    void StopRunning() {
        if (isTraversing)
            return;
        running = false;
        agent.speed = walkSpeed;
    }

    // Used in conjunction with off mesh links to allow pathfinding through doors but while still keeping them as obstacles.
    IEnumerator TraverseBoundry() {
        agent.speed = 0;
        isTraversing = true;
        yield return new WaitForSeconds(6);
        agent.speed = doorSpeed;

    }

    // Adds a new target with a given priority. Currently only accepts high or low priority. Generates a target to move to.
    public void AddTarget(GameObject targ, int priority) {
        GameObject temp = Instantiate(markerPrefab, targ.transform.position, Quaternion.identity);
        if (priority == 1) {
            forceIdleCounter = 0f;
            if (priorityTarget == null)
                noiseController.locatedPlayer();
            priorityTarget = targ;
            chaseTimer = 10f;
            StartRunning();
        }else if(priority == 2) {
            // This is called when the monster loses sight of the player to go to the last known position.
            // Also should be called when an alarm goes off.
            // Removes all other markers as the monster has a high priority target, but it's not the player.
            foreach (GameObject t in targets)
                Destroy(t);
            targets.Clear();
            targets.Insert(0, temp);
            StartRunning();
        }
        else {
            targets.Add(temp);
        }

    }

    public void AddTarget(Vector3 targ) {
        GameObject temp = Instantiate(markerPrefab, targ, Quaternion.identity);
        targets.Add(temp);
    }

    public void OnTriggerEnter(Collider collision) {
        // Checks if the monster has reached its primary destination. If so, remove it from the list, remove target marker.
        if(collision.gameObject.tag == "MonsterMarker") {
            if(priorityTarget == null)
                forceIdleCounter = 3f + Random.Range(0, 5f);
            targets.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            // TODO: Remove this?? why
            if(targets.Count <= 0)
                GenerateRandomTarget();
        }
        if(collision.gameObject.tag == "ActualPlayer") {
            if (!hasDied) {
                noiseController.commitDie();
                hasDied = true;
                collision.transform.parent.GetComponentInChildren<Camera>().enabled = false;
                GameObject.Find("Overlord").GetComponent<Overlord>().youDied();
            }
            chaseTimer = 0f;
            priorityTarget = null;
        }
    }

    // Used for test cases.
    public int GetTargetCount() {
        return targets.Count;
    }
}
