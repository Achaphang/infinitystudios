using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorDoorOpen : MonoBehaviour
{
    public GameObject door;
    public bool isOpened = false;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.transform.gameObject.name);

        if (other.transform.gameObject.name == "3dPlayerObjs" && isOpened == false)
        {
            Debug.Log("I see you");
            StartCoroutine("DoorOpener");

            isOpened = true;
        }
    }

    IEnumerator DoorOpener()
    {
        Debug.Log("Coroutine Started");
        Debug.Log(door.transform.position);
        while (door.transform.position.y < 4.45f)
        {
            Debug.Log("Position changing");
            door.transform.Translate(new Vector3(0, 0.02f, 0));
            yield return new WaitForSeconds(0.01f);
        }
        Debug.Log(door.transform.position);
        Debug.Log("Coroutine About to End");
        yield return new WaitForSeconds(2);
        isOpened = false;
    }

}
