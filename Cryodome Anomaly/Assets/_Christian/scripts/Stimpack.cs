using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stimpack : MonoBehaviour
{
    public Transform stimpackPrefab;
    
    void OnCollisionEnter(Collision col) {
        if (col.collider.name == "3dPlayerObjs") {
            Debug.Log("Collided! now 2");
        }
    }
}
