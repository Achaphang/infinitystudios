using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightRotation : MonoBehaviour
{
    private Vector3 myVector;
    public float speed = 7.0f;
    private void Start()
    {

    }
    // Update is called once per frame
    void Update()
    {
        myVector = new Vector3(Random.Range(-1, 1), Random.Range(-1, 1), Random.Range(-1, 1));
        transform.Rotate(myVector * speed * Time.deltaTime);
    }
}
