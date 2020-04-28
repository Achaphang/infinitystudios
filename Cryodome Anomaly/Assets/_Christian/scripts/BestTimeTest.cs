using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* The green test block will trigger the event when the player collides with it.
 * This is to test best score mechanism.
 */

public class BestTimeTest : MonoBehaviour
{
    Overlord overlord;

    public void Start() {
        overlord = GameObject.Find("Overlord").GetComponent<Overlord>();
    }

    public void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "ActualPlayer") {
            overlord.youWonnered();
        }
    }
}
