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
    SpiderSpawn s1;
    // Start is called before the first frame update
    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        animation = GetComponentInChildren<Animator>();
        s1 = GetComponent<SpiderSpawn>();
    }

    //Need to get animation working
    void Update()
    {
            /*foreach (GameObject spider in s1.spiders)
            {
                FindMoveLocation(); //Move the spiders in the list
            } */
            if(myNavMeshAgent.velocity.magnitude < .3f){
                FindMoveLocation();
            }
    }


   public void FindMoveLocation()
    {
        // Vector3 moveLocation = RandomNavSphere(transform.position, wanderRadius, -1);
        Vector3 moveLocation = GetRandomLocation();
        myNavMeshAgent.SetDestination(moveLocation);
           
    }


    Vector3 GetRandomLocation()
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

}
