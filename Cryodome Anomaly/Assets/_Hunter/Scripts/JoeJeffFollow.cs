using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class JoeJeffFollow : MonoBehaviour
{
    GameObject player;
    private NavMeshAgent myNavMeshAgent;
    private Vector3 offset;
    public Animator animation;
    // Start is called before the first frame update
    void Start()
    {
        myNavMeshAgent = GetComponent<NavMeshAgent>();
        animation = GetComponentInChildren<Animator>();

        if (GameObject.Find("Player Variant") == null)
            return;

        player = GameObject.Find("Player Variant").transform.GetChild(0).GetChild(3).GetChild(0).gameObject;    //Jon helped me with this line
        if (player.active == false)
        {
            // Disable joe jeff because he keeps running into the player. Fix this please
            //Destroy(gameObject);
            player = GameObject.Find("3dPlayerObjs");
        }
    }

    void FixedUpdate()
    {
        if (player == null) {
            player = GameObject.Find("DemoPlayer(Clone)");
            return;
        }

        offset = new Vector3(1.0f, 0, 1.0f);
        if (myNavMeshAgent.velocity.magnitude > .2f)
        {
            myNavMeshAgent.SetDestination(player.transform.position + offset);
            animation.Play("HumanoidRun");

        }
        else
        {
            animation.Play("HumanoidIdle");
        }
        
    }
}
