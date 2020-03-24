using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    CharacterController controller;

    public float speed = 3f;
    float gravity = -9.81f;
    Vector3 velocity;

    void Start() {
        controller = GetComponent<CharacterController>();
    }

    void Update() {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move.Normalize();
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        DetectClicks();
    }

    void DetectClicks() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit)) {
                if(hit.transform.name == "Flashlight" || hit.transform.name == "Flashlight(Clone)") {
                    transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<Flashlight>().SetGrabbed(true);
                    transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<Flashlight>().RestorePower();
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }
}
