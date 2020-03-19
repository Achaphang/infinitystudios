using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterVisibilityController : MonoBehaviour
{
    GameObject player;
    MonsterController monsterController;
    float spottingTimer = 0f;
    RaycastHit hit;

    // Used in the new vision system, angle generated a cone, distance is how far the monster can see.
    public float viewAngle = 35f;
    public float viewDistance = 20f;

    void Start()
    {
        if(GameObject.Find("3dPlayerObjs").active == true) {
            player = GameObject.Find("3dPlayerObjs");
        } else {
            player = GameObject.Find("HeadCollider");
        }

        monsterController = GetComponentInParent<MonsterController>();
    }

    // Update is called once per frame
    void FixedUpdate(){
        Vector3 direction = player.transform.position - transform.position;

        if(direction.magnitude <= viewDistance) {
            if (Vector3.Angle(direction, transform.forward) < viewAngle) {
                Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit);
                if (hit.transform.tag == "ActualPlayer") {
                    spottingTimer += Time.deltaTime;
                    if (spottingTimer > .55f) {
                        monsterController.AddTarget(player, 1);
                        spottingTimer = .45f;
                    }
                } else {
                    spottingTimer = 0f;
                }
            }
        }
    }
}
