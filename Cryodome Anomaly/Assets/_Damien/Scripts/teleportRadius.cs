using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class TeleportRadius : MonoBehaviour
{
    float timeHeld = 0.08f;
    Vector3 defaultRad;
    public Transform targetPosition;

    void Start()
    {
        defaultRad = new Vector3(.2f, 0.01f, .2f);
    }

    // Update is called once per frame
    void Update()
    {

        if ((SteamVR_Input.GetAction<SteamVR_Action_Boolean>("Teleport").GetState(SteamVR_Input_Sources.Any))) {
            transform.position = new Vector3(targetPosition.position.x, 0, targetPosition.position.z);
            timeHeld += Time.deltaTime;
            if(timeHeld <= 3)
                UpdateRadius();
        }else {
            gameObject.transform.localScale = defaultRad;
            timeHeld = .08f;
        }
    }

    void UpdateRadius() {
        gameObject.transform.localScale = new Vector3(.2f + (timeHeld * 1.25f), 1, .2f + (timeHeld * 1.25f));
    }
}
