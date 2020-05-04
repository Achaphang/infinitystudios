using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour
{
    // 0 = basic. 1 = sprinter. >1 = who knows? Maybe BOB HIMSELF?
    // 2 = stalker type, really quiet but slow
    public int monsterType = 0;
    List<GameObject> targets;
    GameObject priorityTarget;
    // This object is a square box that acts as a target collider for the monster.
    public GameObject markerPrefab;
    NavMeshAgent agent;
    Animator anim;

    // Used for running duration.
    float staminaMax = 20f;
    float stamina = 0f;
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
    bool doorIsOpen = false;

    bool hasDied = false;
    MonsterNoiseController noiseController;
    public Collider boundsController;

    // Used for the sprinter types when they're hiding in a specific location, such as the roof of the big area.
    float perchTimer = 0f;

    int randomWalk = 1;

    void Start() {
        targets = new List<GameObject>();
        noiseController = GetComponent<MonsterNoiseController>();
        agent = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        agent.Warp(new Vector3(transform.position.x, 1, transform.position.z));
        // Harder difficulties make the monster move slightly faster
        if (Globals.Instance != null) 
            if (Globals.Instance.difficulty != -1) 
                agent.speed = agent.speed - ((2 - Globals.Instance.difficulty) * .125f);

        walkSpeed = agent.speed;
        runSpeed = walkSpeed * 2.75f;
        doorSpeed = agent.speed * .5f;

        GenerateRandomTarget();
        InvokeRepeating("ChasePlayerNoises", 0f, 2f);
        InvokeRepeating("UpdateStamina", 0f, 1f);
        // Simple way to make the sprinters always sprint
        if(monsterType == 1) {
            running = true;
            stamina = staminaMax;
        }

        if (monsterType != 2)
            randomWalk = Random.Range(1, 4);
    }

    void Update() {
        // This if statement basically checks for all possible times that the monster SHOULD be idle but might not be.
        if(agent.speed == 0 || agent.velocity.magnitude < .3f || (targets.Count == 0 && priorityTarget == null) || forceIdleCounter > 0f) {
            anim.Play("idle");
        }else if(agent.speed <= walkSpeed) {
            if(randomWalk == 1)
                anim.Play("walk");
            if (randomWalk == 2)
                anim.Play("walk2");
            if (randomWalk == 3)
                anim.Play("walk3");
        } else{
            anim.Play("run");
        }

        if (running && !isTraversing) 
            agent.speed = runSpeed;

        if (chaseTimer <= 0f && priorityTarget != null) {
            AddTarget(priorityTarget, 2);
            StartCoroutine(AddTargetLater(priorityTarget, .75f));
            priorityTarget = null;

            noiseController.lostPlayer();
        } else {
            chaseTimer -= Time.deltaTime;
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

        if(agent.velocity.magnitude < .3f && forceIdleCounter < 0f) {
            idleResetCounter -= Time.deltaTime;
            if(idleResetCounter <= 0f) {
                if(targets.Count > 0) {
                    Destroy(targets[0]);
                    targets.RemoveAt(0);
                }

                if(targets.Count == 0)
                    GenerateRandomTarget();
                idleResetCounter = 5f;
            }
        } else {
            idleResetCounter = .1f;
        }

        forceIdleCounter -= Time.deltaTime;

        if ((targets.Count == 0 && priorityTarget == null) || forceIdleCounter > 0f)
            return;

        if (priorityTarget != null)
            agent.destination = priorityTarget.transform.position;
        else if (forceIdleCounter <= 0f)
            if(agent.destination != targets[0].transform.position)
                agent.destination = targets[0].transform.position;

        if (!running)
            agent.speed = walkSpeed;
        if (stamina <= 0f)
            StopRunning();
    }

    void UpdateStamina() {
        if (monsterType != 1)
            stamina -= 1f;

        if (stamina < staminaMax && !running)
            stamina += 5;
    }

    // Use InvokeRepeating
    void ChasePlayerNoises() {
        if (!running || isTraversing || forceIdleCounter > 0f)
            return;

        if (Random.Range(0f, 100f) > 75f)
            noiseController.chasingPlayer();
    }

    // Code taken from FlashlightSpawn
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
        Vector3 newPath;
        // TODO: Fix this, weird navmesh issues decided to disable for now.
        /*if (Random.Range(0f, 1f + (monsterType / 10f)) > .97f && boundsController.name == "Level2Bounds") {
            perchTimer = 20f + Random.Range(-10f, 10f + monsterType * 10);
            newPath = new Vector3(Random.Range(13f, 13.54f), 3.43f, Random.Range(115.65f, 124.5f));
            AddTarget(newPath);
            return;
        }
        else*/
        newPath = GetRandomLocation();

        if (boundsController != null)
            if (boundsController.bounds.Contains(newPath))
                AddTarget(newPath);
            else
                GenerateRandomTarget();
        else
            AddTarget(newPath);
            
    }

    void StartRunning() {
        if (stamina < staminaMax || monsterType == 2)
            return;
        running = true;
    }

    void StopRunning() {
        if (isTraversing)
            return;
        running = false;
    }

    // Used in conjunction with off mesh links to allow pathfinding through doors but while still keeping them as obstacles.
    IEnumerator TraverseBoundry() {
        agent.speed = 0;
        isTraversing = true;
        float maxTimer = 5f;
        while(doorIsOpen == false && maxTimer > 0f) {
            maxTimer -= .25f;
            yield return new WaitForSeconds(.25f);
        }
        if (!running)
            agent.speed = doorSpeed;
        else
            agent.speed = doorSpeed * 3.25f;

    }

    public void GetDoorData(bool tf) {
        doorIsOpen = tf;
    }

    void ClearTargets() {
        foreach (GameObject t in targets)
            Destroy(t);
        targets.Clear();
    }

    // Adds a new target with a given priority. Currently only accepts high or low priority. Generates a target to move to.
    public void AddTarget(GameObject targ, int priority) {
        if (priority == 1) {
            stamina = staminaMax;
            ClearTargets();
            forceIdleCounter = 0f;
            if (priorityTarget == null)
                noiseController.locatedPlayer();
            priorityTarget = targ;
            chaseTimer = 1.75f;
            StartRunning();
            return;
        }
        Vector3 newPath = targ.transform.position;
        if(boundsController != null)
            if (!boundsController.bounds.Contains(newPath) && priority == 2) 
                return;
        GameObject temp = Instantiate(markerPrefab, targ.transform.position, Quaternion.identity);
        temp.GetComponent<MarkerController>().SetMonster(gameObject);
        if(priority == 2) {
            // This is called when the monster loses sight of the player to go to the last known position.
            // Also should be called when an alarm goes off.
            // Removes all other markers as the monster has a high priority target, but it's not the player.
            ClearTargets();
            targets.Insert(0, temp);
            StartRunning();
        }
        else {
            targets.Add(temp);
        }

    }

    public void AddTarget(Vector3 targ) {
        GameObject temp = Instantiate(markerPrefab, targ, Quaternion.identity);
        temp.GetComponent<MarkerController>().SetMonster(gameObject);
        targets.Add(temp);
    }

    IEnumerator AddTargetLater(GameObject targ, float wait) {
        yield return new WaitForSeconds(wait);
        AddTarget(targ.transform.position);
    }

    public void OnTriggerEnter(Collider collision) {
        // Checks if the monster has reached its primary destination. If so, remove it from the list, remove target marker.
        if(collision.gameObject.tag == "MonsterMarker") {
            if(collision.GetComponent<MarkerController>().GetMonster() != null)
                if (collision.GetComponent<MarkerController>().GetMonster() != gameObject)
                    return;

            if(priorityTarget == null && !running) {
                forceIdleCounter = 1f + Random.Range(4f, 18f);
                if (monsterType == 2)
                    forceIdleCounter += Random.Range(0f, 20f);
            }else if(priorityTarget == null && monsterType == 1) {
                forceIdleCounter = 1f + perchTimer;
                perchTimer = 0f;
            }
            targets.Remove(collision.gameObject);
            Destroy(collision.gameObject);
            if(targets.Count <= 0 && priorityTarget == null) {
                if(monsterType != 1)
                    StopRunning();
                GenerateRandomTarget();
            }
        }
        if(collision.gameObject.tag == "ActualPlayer" || collision.gameObject.tag == "NpcPlayer") {
            if (!hasDied) {
                collision.enabled = false;
                noiseController.commitDie();
                hasDied = true;
                Camera temp = collision.transform.parent.GetComponentInChildren<Camera>();
                if (temp == null)
                    temp = collision.transform.parent.GetComponent<Camera>();
                collision.transform.parent.GetComponentInChildren<Camera>().enabled = false;
                GameObject.Find("Overlord").GetComponent<Overlord>().youDied();
            }
            chaseTimer = 0f;
            priorityTarget = null;
        }
    }

    public void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "NpcPlayer") {
            Debug.Log("AAAAA");
        }
    }

    // Used for test cases.
    public int GetTargetCount() {
        return targets.Count;
    }
}
