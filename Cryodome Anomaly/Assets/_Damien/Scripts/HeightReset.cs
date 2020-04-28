using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightReset : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, 0, transform.position.z);
    }
}
