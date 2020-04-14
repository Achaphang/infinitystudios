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
    public float viewAngle = 45f;
    public float viewDistance = 45f;
    public float closeViewDistance = 2f;

    void Start()
    {
        //GameObject.Find("3dPlayerObjs").active == true

        monsterController = GetComponentInParent<MonsterController>();
        if (GameObject.Find("Player Variant") == null)
            return;

        player = GameObject.Find("Player Variant").transform.GetChild(0).GetChild(3).GetChild(0).gameObject;
        if (player.active == false) {
            player = GameObject.Find("3dPlayerObjs");
        } 
    }

    // Update is called once per frame
    void FixedUpdate(){
        if (player == null) {
            player = GameObject.Find("DemoPlayer(Clone)");
            return;
        }
        Vector3 direction = player.transform.position - transform.position;

        if(direction.magnitude <= viewDistance) {
            if (Vector3.Angle(direction, transform.forward) < viewAngle) {
                Physics.Raycast(new Vector3(transform.position.x, transform.position.y, transform.position.z), (player.transform.position - transform.position), out hit);
                Debug.DrawRay(new Vector3(transform.position.x, transform.position.y, transform.position.z), (player.transform.position - transform.position));
                if (hit.transform.tag == "ActualPlayer" || hit.transform.tag == "NpcPlayer") {
                    spottingTimer += Time.deltaTime;
                    if (spottingTimer > .2f) {
                        monsterController.AddTarget(player, 1);
                        spottingTimer = .15f;
                    }
                } else {
                    spottingTimer = 0f;
                }
            }else if(direction.magnitude < closeViewDistance) {
                Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit);
                if (hit.transform.tag == "ActualPlayer" || hit.transform.tag == "NpcPlayer") {
                    spottingTimer += Time.deltaTime;
                    if (spottingTimer > 1f) {
                        monsterController.AddTarget(player, 1);
                        spottingTimer = .5f;
                    }
                } else {
                    spottingTimer = 0f;
                }
            }
        }
    }
}
