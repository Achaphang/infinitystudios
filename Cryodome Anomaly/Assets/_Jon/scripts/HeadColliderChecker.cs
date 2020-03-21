using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HeadColliderChecker : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision) {
        Debug.Log("I AMCOLLIDERAA");
        SteamVR_Fade.Start(Color.black, 1f);
    }

    public void OnCollisionExit(Collision collision) {
        SteamVR_Fade.Start(Color.clear, 1f);
    }
}
