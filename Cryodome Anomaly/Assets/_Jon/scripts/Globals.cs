using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Globals : MonoBehaviour
{
    private static Globals _instance;

    public static Globals Instance {  get { return _instance; } }

    // 1-3: easy through hard. -1 = dr bc mode
    // Default: 2
    public int difficulty = 2;
    public int demoFlip = 1;

    private void Awake() {
        if(_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
