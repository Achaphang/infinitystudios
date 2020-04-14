using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Keycard : MonoBehaviour
{
    public int accessLevel = 0;

    public void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.name == "KeycardReader") {
            if (collision.transform.parent.GetComponent<Keypad>().accessLevel <= accessLevel) {
                if (collision.transform.parent.GetComponent<Keypad>().UnlockDoor(true))
                    Destroy(gameObject);
            } else
                collision.transform.parent.GetComponent<Keypad>().EnterPasscode(true);
        }
    }
}
