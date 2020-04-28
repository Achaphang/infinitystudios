using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stimpack : MonoBehaviour
{
    public float stimpackValue;
    
    public virtual float GetStimpackValue() {
        return 20.0F;
    }
}
