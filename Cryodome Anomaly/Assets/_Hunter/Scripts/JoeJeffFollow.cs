using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
public class JoeJeffFollow : MonoBehaviour
{
    public GameObject player;
    public float moveSpeed;
    NavMeshAgent joeJeff;
    public Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        if (player.active == false)
        {
            player = GameObject.Find("3dPlayerObjs");
        }
        joeJeff = GetComponent<NavMeshAgent>();
    }
    private void FixedUpdate()
    {
        joeJeff.destination = player.transform.position+offset;
    }
}
