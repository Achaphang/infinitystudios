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

    void Start()
    {
        if (odds != 100)
            if (Globals.Instance != null)
                if (Globals.Instance.difficulty != -1)
                    odds += (35 - (Globals.Instance.difficulty * 10));
    }

    public int GetOdds() {
        return odds;
    }

    public GameObject GetItem() {
        return specificItem;
    }
}
