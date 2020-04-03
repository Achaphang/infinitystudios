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

}
