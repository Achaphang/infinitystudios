using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFloorColor : MonoBehaviour
{
    public float speed = 1.0f;
    public Color startColor;
    public Color EndColor;
    public bool repeatable = false;
    float startTime;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        InvokeRepeating("floorChange", 2.0f, .5f);
    }

    // Update is called once per frame
    void floorChange()
    {
        if (!repeatable)
        {
            float t = (Time.time - startTime) * speed;
            GetComponent<Renderer>().material.color = Color.Lerp(startColor, EndColor, t);
        }
        else
        {

            float t = (Mathf.Sin(Time.time - startTime) * speed);
            GetComponent<Renderer>().material.color = Color.Lerp(EndColor, startColor, t);
        }
    }
}
