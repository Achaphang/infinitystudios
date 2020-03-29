using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class SpiderMovement : MonoBehaviour
{
    //SpiderSpawn SpiderSpawn;
    private NavMeshAgent myNavMeshAgent;
    public Animator animation;
    public float wanderRadius;
    // Start is called before the first frame update
    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        animation = GetComponentInChildren<Animator>();
        //SpiderSpawn = GetComponentInParent<SpiderSpawn>();
    }

    //Need to get animation working
    void Update()
    {
        FindMoveLocation(); //Move the spiders in the list
        if (myNavMeshAgent.velocity.magnitude > .2f)
        {
           // animation.Play("Walk");

        }
        else
        {
           // animation.Play("Idle");
        }
    }
    public void FindMoveLocation()
    {
        Vector3 moveLocation = RandomNavSphere(transform.position, wanderRadius, -1);
        myNavMeshAgent.SetDestination(moveLocation);

    }


    public static Vector3 RandomNavSphere(Vector3 origin, float dist, int layermask)
    {
        Vector3 randDirection = Random.insideUnitSphere * dist;
        randDirection += origin;
        NavMeshHit navHit;
        NavMesh.SamplePosition(randDirection, out navHit, dist, layermask);
        return navHit.position;
    }

    /*Vector3 GetRandomLocation()
    {
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
    */
}
