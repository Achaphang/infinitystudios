using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightBattery : MonoBehaviour
{
    public void OnTriggerEnter(Collider collision) {
        if(collision.gameObject.name == "FlashlightObject") {
            UseBattery(collision.transform.GetChild(0).GetComponent<Flashlight>());
        }
    }

    public virtual void UseBattery(Flashlight f) {
        f.RestorePower(75f);
        Destroy(gameObject);
    }
}
