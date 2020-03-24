using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Light light;
    float powerPercentage = 100;
    float originalIntensity;
    float originalRange;
    float originalAngle;

    bool grabbed = false;

    // Start is called before the first frame update
    void Awake()
    {
        light = GetComponent<Light>();
        originalIntensity = light.intensity;
        originalRange = light.range;
        originalAngle = light.spotAngle;
        InvokeRepeating("DecreasePower", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RestorePower() {
        powerPercentage = 100f;
    }

    public void SetGrabbed(bool tf) {
        grabbed = tf;
        if(light == null)
            light = GetComponent<Light>();
        light.enabled = tf;
    }

    void DecreasePower() {
        if (!grabbed)
            return;
        // All flashlights are at least a little useful still.
        if(powerPercentage > 40f)
            powerPercentage -= .1f;
        light.intensity = (originalIntensity * (powerPercentage / 100f));
    }
}
