using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class JoeJeffFollow : MonoBehaviour
{
    public GameObject player;
    public LayerMask detectionLayer;
    private Transform myTransform;
    private NavMeshAgent myNavMeshAgent;
    private Collider[] hitColliders;
    private float checkRate;
    private float nextCheck;
    private float detectionRadius=50;
    // Start is called before the first frame update
    void Start()
    {
        if (player.active == false)
        {
            // Disable joe jeff because he keeps running into the player. Fix this please
            //Destroy(gameObject);
            player = GameObject.Find("3dPlayerObjs");
        }
        setInitialReferences();
        //joeJeff = GetComponent<NavMeshAgent>();
        //offset = new Vector3(UnityEngine.Random.Range(0.0f, 0.5f), 0, UnityEngine.Random.Range(0.0f, 0.5f));
    }

    void Update()
    {
        checkIfPlayerInRange();
    }

    void setInitialReferences()
    {
        myTransform = transform;
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        checkRate = Random.Range(0.8f, 1.2f);   //how often to check for player
    }
    void checkIfPlayerInRange()
    {
        if(Time.deltaTime > nextCheck && myNavMeshAgent == true)
        {
            nextCheck = Time.deltaTime + checkRate;
            hitColliders = Physics.OverlapSphere(myTransform.position, detectionRadius, detectionLayer);
            //found the player
            if(hitColliders.Length >0)
            {
                myNavMeshAgent.SetDestination(hitColliders[0].transform.position);
            }
        }
    }
}
