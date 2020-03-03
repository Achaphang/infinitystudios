using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    List<Vector3> targets;
    // This object is a square box that acts as a target collider for the monster.
    public GameObject markerPrefab;
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
        targets = new List<Vector3>();
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
        if (targets.Count == 0)
            return;

        agent.destination = targets[0];

        if (stamina <= 0f)
            stopRunning();
    }

    public void FixedUpdate() {
        if (running)
            stamina -= .1f;
        else if (stamina < 100f)
            stamina += .05f;
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
        AddTarget(GetRandomLocation(), 3);
    }

    void stopRunning() {
        running = false;
        agent.speed = walkSpeed;
    }

    // Adds a new target with a given priority. Currently only accepts high or low priority. Generates a target to move to.
    // TODO: Allow targets to be placed as child of object, so that they can move, for example to follow the player. Currently static markers only.
    public void AddTarget(Vector3 targ, int priority) {
        if (priority == 1) {
            targets.Insert(0, targ);
        } else {
            targets.Add(targ);
        }

        Instantiate(markerPrefab, targ, Quaternion.identity);
    }

    public void OnTriggerEnter(Collider collision) {
        Debug.Log("AAAA");
        // Checks if the monster has reached its primary destination. If so, remove it from the list, remove target marker.
        if(collision.gameObject.tag == "MonsterMarker") {
            targets.RemoveAt(0);
            Destroy(collision.gameObject);
            // TODO: Remove this
            GenerateRandomTarget();
        }
    }
}
