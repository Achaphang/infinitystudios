using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseController : MonoBehaviour
{
    public Transform body;
    public float sensitivity = 100f;
    float x = 0f;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        x -= mouseY;
        x = Mathf.Clamp(x, -90f, 90f);

        transform.localRotation = Quaternion.Euler(x, 0f, 0f);
        body.Rotate(Vector3.up * mouseX);
    }
}
