using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class JoeJeffFollow : MonoBehaviour
{
    public GameObject player;
    private NavMeshAgent myNavMeshAgent;
    public Vector3 offset;
    public Animator animation;
    // Start is called before the first frame update
    void Start()
    {
        if (player.active == false)
        {
            // Disable joe jeff because he keeps running into the player. Fix this please
            //Destroy(gameObject);
            player = GameObject.Find("3dPlayerObjs");
        }
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        animation = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        offset = new Vector3(1.0f, 0, 1.0f);
        if (player.transform.hasChanged)
        {
            animation.gameObject.SetActive(true);
            myNavMeshAgent.SetDestination(player.transform.position + offset);
        }
        else
        {
            animation.gameObject.SetActive(false);
        }
        
    }
}
