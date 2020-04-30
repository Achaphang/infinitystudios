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
    public float duration = 1.0f;
    Color color0 = Color.red;
    Color color1 = Color.yellow;

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
    //player found a battery so restore the power back to 100%
    public void RestorePower(float percentage = 100f) {
        powerPercentage = powerPercentage + percentage;
        if (powerPercentage > 100f)
            powerPercentage = 100f;
        flickerChecker = 1;
    }
    //upgrade the light of the flashlight to increase the intensity
    public void UpgradeFlashlight() {
        originalIntensity = originalIntensity * 1.15f;
        originalRange = originalRange * 1.15f;
        originalAngle = originalAngle * 1.15f;
        light.color = Color.white;
    }
    //used to activate light if its grabbed
    public void SetGrabbed(bool tf) {
        grabbed = tf;
        if(light == null)
            light = GetComponent<Light>();
        light.enabled = tf;
    }

    //gradually decrease the power of the flashlight
    void DecreasePower() {
        if (!grabbed)
            return;
        if(powerPercentage > 0f)
            powerPercentage -= 0.25f;
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
            //ChangeCol();
        }
    }

    //Change color of the flashlight, currently dont use as it was voted against using by the team. 
    private void ChangeCol(){
        float t = Mathf.PingPong(Time.time, duration) / duration;
        light.color = Color.Lerp(color0, color1, t);
    }

    //Flicker the lighting on and off for a random amount of time to add a horror effect
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
