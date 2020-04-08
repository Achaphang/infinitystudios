using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Make an empty GameObject and call it "Door"
//Drag and drop your Door model into Scene and rename it to "Body"
//Move the "Body" Object inside "Door"
//Add a Collider (preferably SphereCollider) to "Door" Object and make it much bigger then the "Body" model, mark it as Trigger
//Assign this script to a "Door" Object (the one with a Trigger Collider)
//Make sure the main Character is tagged "Player"
//Upon walking into trigger area the door should Open automatically
public class DoorAnimation : MonoBehaviour
{
    // Sliding door
    public enum OpenDirection { x, y, z }
    public OpenDirection direction = OpenDirection.y;
    public float openDistance = 1f; //How far should door slide (change direction by entering either a positive or a negative value)
    public float openSpeed = 2.0f; //Increasing this value will make the door open faster
    public GameObject doorBody1; //Door body Transform
    public GameObject doorBody2;
    public GameObject doorBodyElevator;

    AudioSource doorSource;
    AudioClip doorBreakClip;

    bool open = false;
    bool monsterOpen = false;
    bool forceOpen = false;

    bool isOpen = false;
    List<MonsterController> monsters;

    Vector3 defaultDoor1Position;
    Vector3 defaultDoor2Position;
    Vector3 defaultDoorElevatorPosition;

    // Used to enable and disable the doors for both the player and the monster.
    GameObject doorLink1;
    GameObject doorLink2;

    public bool unlocked = true;

    void Start()
    {
        monsters = new List<MonsterController>();
        if(doorBody1 != null)
            defaultDoor1Position = doorBody1.transform.localPosition;
        if(doorBody2 != null)
            defaultDoor2Position = doorBody2.transform.localPosition;
        if(doorBodyElevator != null)
            defaultDoorElevatorPosition = doorBodyElevator.transform.localPosition;

        if (transform.childCount > 0) {
            doorLink1 = transform.GetChild(0).gameObject;
            doorLink2 = transform.GetChild(1).gameObject;
        }

        if (!unlocked) {
            if(doorLink1 != null)
                doorLink1.SetActive(false);
            if(doorLink2 != null)
                doorLink2.SetActive(false);
        }

        doorSource = GetComponent<AudioSource>();
        doorBreakClip = Resources.Load<AudioClip>("Sounds/Misc/breakDoor");
    }

    // Main function
    void Update()
    {
        if (!unlocked)
            return;

        if(doorBodyElevator != null) {
            doorBodyElevator.transform.localPosition = new Vector3(doorBodyElevator.transform.localPosition.x, Mathf.Lerp(doorBodyElevator.transform.localPosition.y, defaultDoorElevatorPosition.y + (forceOpen || open || monsterOpen ? openDistance + (monsterOpen ? Random.Range(-4.02f, 4.4f) : 0) : 0), Time.deltaTime * openSpeed / (monsterOpen ? 13 : 1)), doorBodyElevator.transform.localPosition.z);

            if (doorBodyElevator.transform.localPosition.y > 4)
                isOpen = true;
            else
                isOpen = false;
        }

        if (doorBody1 != null) {
            doorBody1.transform.localPosition = new Vector3(doorBody1.transform.localPosition.x, doorBody1.transform.localPosition.y, Mathf.Lerp(doorBody1.transform.localPosition.z, defaultDoor1Position.z + (forceOpen || open || monsterOpen ? -openDistance - (monsterOpen ? Random.Range(-4.02f, 4.4f) : 0) : 0), Time.deltaTime * openSpeed / (monsterOpen ? 13 : 1)));

            if (doorBody1.transform.localPosition.z < -1)
                isOpen = true;
            else
                isOpen = false;
        }

        if(doorBody2 != null)
            doorBody2.transform.localPosition = new Vector3(doorBody2.transform.localPosition.x, doorBody2.transform.localPosition.y, Mathf.Lerp(doorBody2.transform.localPosition.z, defaultDoor2Position.z + (forceOpen || open || monsterOpen ? openDistance + (monsterOpen ? Random.Range(-4.02f, 4.4f) : 0) : 0), Time.deltaTime * openSpeed / (monsterOpen ? 13 : 1)));

        foreach(MonsterController m in monsters) 
            m.GetDoorData(isOpen);
        
    }

    public bool getIsOpen() {
        return isOpen;
    }

    public void SetDoorUnlocked(bool tf) {
        unlocked = tf;
        if(doorLink1 != null)
            doorLink1.SetActive(tf);
        if(doorLink2 != null)
            doorLink2.SetActive(tf);
    }

    public void SetForceOpen(bool tf) {
        forceOpen = tf;
    }

    // Activate the Main function when Player enter the trigger area
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ActualPlayer"))
        {
            open = true;
        }else if (other.CompareTag("Monster")) {
            monsters.Add(other.transform.parent.GetComponent<MonsterController>());
            monsters[monsters.Count - 1].GetDoorData(false);
            monsterOpen = true;
            if (!doorSource.isPlaying && unlocked) {
                doorSource.clip = doorBreakClip;
                doorSource.Play();
            }
        }
    }

    // Deactivate the Main function when Player exit the trigger area
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ActualPlayer"))
        {
            open = false;
        }

        if (other.CompareTag("Monster")) {
            monsters.Remove(other.transform.parent.GetComponent<MonsterController>());
            if (monsters.Count > 0)
                return;
            monsterOpen = false;
        }
    }
}
