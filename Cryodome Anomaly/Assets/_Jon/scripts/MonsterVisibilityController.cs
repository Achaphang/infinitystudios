using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterVisibilityController : MonoBehaviour
{
    GameObject player;
    MonsterController monsterController;
    bool canSee = false;
    float spottingTimer = 0f;
    RaycastHit hit;

    // Start is called before the first frame update
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
        if (canSee) {
            Physics.Raycast(transform.position, (player.transform.position - transform.position), out hit);
            if (hit.transform.tag == "ActualPlayer") {
                spottingTimer += Time.deltaTime;
                if(spottingTimer > .75f) {
                    monsterController.AddTarget(player, 1);
                    spottingTimer = 0;
                }
            } else {
                spottingTimer = 0f;
            }
        }
    }

    // Todo: fix this function. Something in the middle of the level is triggering it, find the invisible player object. I'd guess the flashlight?
    public void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "ActualPlayer") {
            canSee = true;
        }
    }

    public void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "ActualPlayer") {
            canSee = false;
        }
    }
}
