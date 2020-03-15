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

    // Used for running duration.
    float stamina = 100f;
    float idleCounter = 20f;
    float forceIdleCounter = 0f;
    bool running = false;
    float walkSpeed;
    float runSpeed;
    float chaseTimer;

    // Used for boundry traversal
    float doorSpeed;
    bool isTraversing = false;
    float traverseTime = 0;

    bool hasDied = false;
    MonsterNoiseController noiseController;

    void Start() {
        // Generates list, must call constructor
        targets = new List<GameObject>();
        // Marker prefab temporary solution
        //markerPrefab = Resources.Load("assets/prefabs/markerPrefab") as GameObject;
        noiseController = GetComponent<MonsterNoiseController>();
        agent = GetComponent<NavMeshAgent>();
        agent.Warp(new Vector3(-26, 1, -16));
        walkSpeed = agent.speed;
        runSpeed = walkSpeed * 2;
        doorSpeed = agent.speed * .5f;

        GenerateRandomTarget();
    }

    void Update() {
        if (agent.isOnOffMeshLink && !isTraversing)
            StartCoroutine(TraverseBoundry());
        else if (!agent.isOnOffMeshLink && isTraversing) {
            isTraversing = false;
            agent.speed = walkSpeed;
        }

        // Prevents other things from happening while breaking through a door.
        if (isTraversing)
            return;

        // If targets is empty, do nothing for now.
        if(agent.velocity.magnitude == 0) {
            idleCounter -= Time.deltaTime;
            if(idleCounter <= 0f) {
                idleCounter = 20f;
                foreach(GameObject g in targets) {
                    Destroy(g);
                }
                targets.Clear();
                GenerateRandomTarget();
            }
        } else {
            idleCounter = 20f;
        }

        forceIdleCounter -= Time.deltaTime;

        if ((targets.Count == 0 && priorityTarget == null) || forceIdleCounter > 0f)
            return;

        if (priorityTarget != null)
            agent.destination = priorityTarget.transform.position;
        else
            agent.destination = targets[0].transform.position;

        if (!running)
            agent.speed = walkSpeed;
        if (stamina <= 0f)
            StopRunning();
    }

    public void FixedUpdate() {
        if (isTraversing)
            return;

        if (running)
            stamina -= Time.deltaTime;
        else if (stamina < 100f)
            stamina += Time.deltaTime;

        if(chaseTimer <= 0f && priorityTarget != null) {
            AddTarget(priorityTarget, 2);
            priorityTarget = null;
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

    void StopRunning() {
        running = false;
        agent.speed = walkSpeed;
    }

    // Used in conjunction with off mesh links to allow pathfinding through doors but while still keeping them as obstacles.
    IEnumerator TraverseBoundry() {
        agent.speed = 0;
        isTraversing = true;
        yield return new WaitForSeconds(10);
        agent.speed = doorSpeed;
    }

    // Adds a new target with a given priority. Currently only accepts high or low priority. Generates a target to move to.
    public void AddTarget(GameObject targ, int priority) {
        GameObject temp = Instantiate(markerPrefab, targ.transform.position, Quaternion.identity);
        if (priority == 1) {
            forceIdleCounter = 0f;
            if (priorityTarget != targ)
                noiseController.locatedPlayer();
            priorityTarget = targ;
            chaseTimer = 5f;
        }else if(priority == 2) {
            // This is called when the monster loses sight of the player to go to the last known position.
            targets.Insert(0, temp);
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
            targets.RemoveAt(0);
            Destroy(collision.gameObject);
            // TODO: Remove this
            if(targets.Count <= 0)
                GenerateRandomTarget();
        }
        if(collision.gameObject.tag == "ActualPlayer") {
            if (!hasDied) {
                noiseController.commitDie();
                hasDied = true;
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
