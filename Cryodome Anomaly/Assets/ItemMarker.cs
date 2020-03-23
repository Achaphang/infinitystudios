using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMarker : MonoBehaviour
{
    // Higher the number, higher the odds of it spawning
    // 1 = 1% chance of spawning
    public int odds = 50;
    // If this is NOT null, a specific item can potentially spawn here.
    public GameObject specificItem = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetOdds() {
        return odds;
    }

    public GameObject GetItem() {
        return specificItem;
    }
}
