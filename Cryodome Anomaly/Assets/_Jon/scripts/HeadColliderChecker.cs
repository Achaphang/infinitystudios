using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HeadColliderChecker : MonoBehaviour
{
    public GameObject camera;
    public GameObject playerParent;

    public void OnCollisionStay(Collision collision) {
        SteamVR_Fade.Start(Color.black, .25f);
        //transform.localScale = new Vector3(.1f, .1f, .1f);
    }

    public void OnCollisionExit(Collision collision) {
        //camera.transform.position = new Vector3(playerParent.transform.position.x, camera.transform.position.y, playerParent.transform.position.z);
        //Valve.VR.OpenVR.System.ResetSeatedZeroPose();
        //transform.localScale = new Vector3(.04f, .04f, .04f);
        SteamVR_Fade.Start(Color.clear, .25f);
    }
}
