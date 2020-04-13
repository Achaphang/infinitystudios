using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBallRotation : MonoBehaviour
{
    public float speed = 7.0f;
    private void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector2.up * speed * Time.deltaTime, Space.World);
    }
}
