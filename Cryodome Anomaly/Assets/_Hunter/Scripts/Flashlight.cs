using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : MonoBehaviour
{
    Light light;
    Light pointLight;
    float powerPercentage = 100f;
    float originalIntensity;
    float originalRange;
    float originalAngle;
    int flickerChecker = 1;

    bool grabbed = false;

    // Start is called before the first frame update
    void Awake()
    {
        light = GetComponent<Light>();
        pointLight = transform.parent.transform.parent.GetChild(1).GetComponent<Light>();
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
        flickerChecker = 1;
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
        if(powerPercentage > 0f)
            powerPercentage -= .25f;
        else if(flickerChecker != -1){
            light.intensity = 0f;
            pointLight.intensity = .2f;
            flickerChecker = -1;
            return;
        }
        // Once we hit below 30% battery, start to flicker the flashlight until it goes out.
        if (powerPercentage > 30f) {
            flickerChecker = 1;
            light.intensity = (originalIntensity * (powerPercentage / 100f));
            pointLight.intensity = (originalIntensity * (powerPercentage / 100f));
        } else if(flickerChecker == 1){
            flickerChecker = 0;
            StartCoroutine(Flicker());
        }
    }


    IEnumerator Flicker (){
        while(flickerChecker == 0) {
            int flickerDecider = (int)(100 / (powerPercentage * 3 + 1)) + Random.Range(0, 4);
            for(int i = 0; i < flickerDecider; i++) {
                light.enabled = false;
                yield return new WaitForSeconds(Random.Range(0.02f, 0.065f + (1 - powerPercentage / 100) / 5f));
                light.enabled = true;
                yield return new WaitForSeconds(Random.Range(0.02f + powerPercentage / 200f, 0.065f + powerPercentage / 200f));
            }
            yield return new WaitForSeconds(Random.Range(.2f + (powerPercentage / 7f), 1f + (powerPercentage / 7f)));
        }
    }
}
