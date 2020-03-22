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

    AudioSource doorSource;
    AudioClip doorBreakClip;

    bool open = false;
    bool monsterOpen = false;

    bool isOpen = false;
    MonsterController monster;

    Vector3 defaultDoor1Position;
    Vector3 defaultDoor2Position;

    // Used to enable and disable the doors for both the player and the monster.
    GameObject doorLink1;
    GameObject doorLink2;

    public bool unlocked = true;

    void Start()
    {
        defaultDoor1Position = doorBody1.transform.localPosition;
        defaultDoor2Position = doorBody2.transform.localPosition;

        doorLink1 = transform.GetChild(0).gameObject;
        doorLink2 = transform.GetChild(1).gameObject;

        if (!unlocked) {
            doorLink1.SetActive(false);
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

        if (direction == OpenDirection.x)
        {
            doorBody1.transform.localPosition = new Vector3(Mathf.Lerp(doorBody1.transform.localPosition.x, defaultDoor1Position.x + (open ? openDistance : 0), Time.deltaTime * openSpeed), doorBody1.transform.localPosition.y, doorBody1.transform.localPosition.z);
        }
        else if (direction == OpenDirection.y)
        {
            doorBody1.transform.localPosition = new Vector3(doorBody1.transform.localPosition.x, Mathf.Lerp(doorBody1.transform.localPosition.y, defaultDoor1Position.y + (open ? openDistance : 0), Time.deltaTime * openSpeed), doorBody1.transform.localPosition.z);
        }
        else if (direction == OpenDirection.z)
        {
            doorBody1.transform.localPosition = new Vector3(doorBody1.transform.localPosition.x, doorBody1.transform.localPosition.y, Mathf.Lerp(doorBody1.transform.localPosition.z, defaultDoor1Position.z + (open || monsterOpen ? -openDistance - (monsterOpen ? Random.Range(-4.02f, 4.4f) : 0) : 0), Time.deltaTime * openSpeed / (monsterOpen ? 13 : 1)));
            doorBody2.transform.localPosition = new Vector3(doorBody2.transform.localPosition.x, doorBody2.transform.localPosition.y, Mathf.Lerp(doorBody2.transform.localPosition.z, defaultDoor2Position.z + (open || monsterOpen ? openDistance + (monsterOpen ? Random.Range(-4.02f, 4.4f) : 0) : 0), Time.deltaTime * openSpeed / (monsterOpen ? 13 : 1)));
        }

        if (doorBody1.transform.localPosition.z < -1) 
            isOpen = true;
         else
            isOpen = false;

        if(monster != null) {
            monster.GetDoorData(isOpen);
        }
    }

    public bool getIsOpen() {
        return isOpen;
    }

    public void SetDoorUnlocked(bool tf) {
        unlocked = tf;
        doorLink1.SetActive(tf);
        doorLink2.SetActive(tf);
    }

    // Activate the Main function when Player enter the trigger area
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ActualPlayer"))
        {
            open = true;
            //monsterOpen = false;
        }else if (other.CompareTag("Monster")) {
            monster = other.transform.parent.GetComponent<MonsterController>();
            monster.GetDoorData(false);
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
            monsterOpen = false;
            monster = null;
        }
    }
}
