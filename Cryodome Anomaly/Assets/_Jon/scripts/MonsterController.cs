using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    [SerializeField] List<GameObject> targets;
    List<GameObject> doors;
    GameObject priorityTarget;
    // This object is a square box that acts as a target collider for the monster.
    public GameObject markerPrefab;
    NavMeshAgent agent;

    // For testing purposes.
    public Transform exampleTransform;

    // Used for running duration.
    float stamina = 100f;
    float idleCounter = 20f;
    float forceIdleCounter = 0f;
    bool running = false;
    float walkSpeed;
    float runSpeed;
    float chaseTimer;

    void Start() {
        // Generates list, must call constructor
        targets = new List<GameObject>();
        doors = new List<GameObject>();
        foreach(GameObject g in GameObject.FindGameObjectsWithTag("DoorMarker")) {
            doors.Add(g);
        }
        // Marker prefab temporary solution
        //markerPrefab = Resources.Load("assets/prefabs/markerPrefab") as GameObject;
        agent = GetComponent<NavMeshAgent>();
        //targets.Add(exampleTransform);
        walkSpeed = agent.speed;
        runSpeed = walkSpeed * 2;
        GenerateRandomTarget();
    }

    void Update() {
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

        if (stamina <= 0f)
            stopRunning();
    }

    public void FixedUpdate() {
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

    void stopRunning() {
        running = false;
        agent.speed = walkSpeed;
    }

    // Adds a new target with a given priority. Currently only accepts high or low priority. Generates a target to move to.
    // TODO: Allow targets to be placed as child of object, so that they can move, for example to follow the player. Currently static markers only.
    public void AddTarget(GameObject targ, int priority) {
        GameObject temp = Instantiate(markerPrefab, targ.transform.position, Quaternion.identity);
        if (priority == 1) {
            forceIdleCounter = 0f;
            priorityTarget = targ;
            chaseTimer = 5f;
        }else if(priority == 2) {
            // This is called when the monster loses sight of the player.
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

    public void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Player" && chaseTimer > 0f) { 
            Debug.Log("Ate player, good job");
            chaseTimer = 0f;
            priorityTarget = null;
        }
    }

    public void OnTriggerEnter(Collider collision) {
        // Checks if the monster has reached its primary destination. If so, remove it from the list, remove target marker.
        if(collision.gameObject.tag == "MonsterMarker") {
            if(priorityTarget == null)
                forceIdleCounter = 10f;
            targets.RemoveAt(0);
            Destroy(collision.gameObject);
            // TODO: Remove this
            if(targets.Count <= 0)
                GenerateRandomTarget();
        }
    }

    // Monster stuck? Use this function to find a nearby door to go into.
    void MoveToNearestDoor() {

    }

    // Used for test cases.
    public int GetTargetCount() {
        return targets.Count;
    }
}
