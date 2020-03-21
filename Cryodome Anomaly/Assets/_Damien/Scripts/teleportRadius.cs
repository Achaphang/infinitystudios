using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class teleportRadius : MonoBehaviour
{
    float xVal;
    float zVal;
    int iteration;
    Vector3 defaultRad;
    public GameObject temp;
    // Start is called before the first frame update
    void Start()
    {
        xVal = 0.08f;
        zVal = 0.08f;
        iteration = 1;
        defaultRad = new Vector3(.2f, 0.01f, .2f);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if ((SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport").GetState(SteamVR_Input_Sources.Any)) && iteration <= 100) {
            gameObject.transform.localScale = new Vector3(.2f + xVal * iteration, 1, .2f + zVal * iteration);
            //temp.transform.localScale = new Vector3(.2f + xVal * iteration, 0.5f, .2f + zVal * iteration);
            iteration += 1;
        }else if(!(SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport").GetState(SteamVR_Input_Sources.Any))) {
            gameObject.transform.localScale = defaultRad;
            //temp.transform.localScale = new Vector3(.2f + xVal * iteration, 0.5f, .2f + zVal * iteration);
            iteration = 1;
        }
    }
}
