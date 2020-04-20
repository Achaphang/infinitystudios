using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateBubbles : MonoBehaviour
{
    public GameObject bubbles;
    // Start is called before the first frame update
    void Start()
    {
       bubbles = GameObject.FindGameObjectWithTag("Bubbles");
    }

    void isThrown()
    {
        bubbles.SetActive(true);
    }
}
