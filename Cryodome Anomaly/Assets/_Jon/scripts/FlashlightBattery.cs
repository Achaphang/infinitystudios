using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightBattery : MonoBehaviour
{
    public void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.name == "FlashlightObject") {
            collision.transform.GetChild(0).GetComponent<Flashlight>().RestorePower();
            Destroy(gameObject);
        }
    }
}
