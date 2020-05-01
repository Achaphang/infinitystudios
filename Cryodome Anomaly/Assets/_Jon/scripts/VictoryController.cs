using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryController : MonoBehaviour {
    Overlord overlord;

    public void Start() {
        overlord = GameObject.Find("Overlord").GetComponent<Overlord>();
    }

    public void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "ActualPlayer") {
            overlord.youWonnered();
        }else if(other.gameObject.tag == "NpcPlayer") {
            overlord.youWonneredDemo();
        }
    }
}
