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
            if(myNavMeshAgent.velocity.magnitude < .02f){
                FindMoveLocation();
            }
    }


   public void FindMoveLocation()
    {
        Vector3 moveLocation = GetRandomLocation();
        myNavMeshAgent.SetDestination(moveLocation);
           
    }


    Vector3 GetRandomLocation()
    {
        Vector2 temp = Random.insideUnitCircle * 3;
        Vector3 point = new Vector3(transform.position.x + temp.x, transform.position.y, transform.position.z + temp.y);
        return point;
    }

}
