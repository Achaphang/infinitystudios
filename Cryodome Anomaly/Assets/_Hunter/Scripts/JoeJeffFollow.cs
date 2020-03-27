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
    private Vector3 offset;
    // Start is called before the first frame update
    void Start()
    {
        if (player.active == false)
        {
            // Disable joe jeff because he keeps running into the player. Fix this please
            Destroy(gameObject);
            player = GameObject.Find("3dPlayerObjs");
        }
        joeJeff = GetComponent<NavMeshAgent>();
        offset = new Vector3(UnityEngine.Random.Range(0.0f, 0.5f), 0, UnityEngine.Random.Range(0.0f, 0.5f));
    }
    private void FixedUpdate()
    {
            
            joeJeff.destination = player.transform.position + offset;
    }
}
