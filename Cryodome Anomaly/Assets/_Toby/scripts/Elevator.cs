using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    bool upperLevel = false;

    // Update is called once per frame
    void Update()
    {
        Ray r = FindObjectOfType<Camera>().ScreenPointToRay(Input.mousePosition);
        RaycastHit ray;

        //Checks if the player is near the door so it can open
        if(true)
        {

        }

        //Checks if the buttons are clicked
        if (Input.GetMouseButtonDown(0))
        {
            if (Physics.Raycast(r, out ray) && ray.transform.gameObject.name == "Up Button")
            {
                upperLevel = true;
                Debug.Log("Registered Up Click");

                while (transform.position != new Vector3(-0.052f, 3, 0))
                {
                    transform.Translate(new Vector3(0, 0.02f, 0));
                }

            }

            if (Physics.Raycast(r, out ray) && ray.transform.gameObject.name == "Down Button")
            {
                upperLevel = false;
                Debug.Log("Registered Down Click");

                transform.position = new Vector3(-0.052f, 0, 0);
            }
        }
    }
}
