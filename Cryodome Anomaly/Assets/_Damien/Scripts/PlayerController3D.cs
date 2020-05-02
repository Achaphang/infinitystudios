using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController3D : MonoBehaviour
{
    CharacterController controller;
    public Image crosshair;
    public Image beeperUI;
    public Slider staminaController;
    public float speed = 3f;
    public float sprintForMod = 1.25f;
    public float sprintForLateralMod = 1.15f;
    bool forwardCheck = false;
    bool lateralCheck = false;
    bool backwardCheck = false;
    public float stamPool = 24f;
    public float stimpackInUse = 0;
    public float currentStam = 24f;
    public float consumeStam = 6f;
    public float regenRateTime = 3f;
    public float regenRateAmount = 2.5f;
    float lastRegen;
    float gravity = -9.81f;
    Vector3 velocity;
    public int beepers = 0;
    int frameUpdateCounter = 0;
    float frameCounterCeiling = 0f;

    float lastStamChunk = 0f;
    float lastStamValue = 0f;

    int accessLevel = 0;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        SetMaxStamina(stamPool);
        lastStamValue = stamPool;
    }

    void Update()
    {
        if (beepers >= 1)
            beeperUI.color = new Color32(0, 188, 54, 255);
        else
            beeperUI.color = new Color32(255, 66, 60, 255);

        forwardCheck = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow));
        lateralCheck = (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow));
        backwardCheck = (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow));
        
        if (Input.GetKey(KeyCode.LeftShift) && forwardCheck && lateralCheck && !backwardCheck && currentStam > 0)
        {
            speed = speed * sprintForLateralMod;
            MovePlayer();
            speed = speed / sprintForLateralMod;
            currentStam -= consumeStam * Time.deltaTime;
            if (currentStam < 0)
                currentStam = 0f;
            lastRegen = Time.time;
        }
        else if (Input.GetKey(KeyCode.LeftShift) && forwardCheck && !backwardCheck && currentStam > 0)
        {
            speed = speed * sprintForMod;
            MovePlayer();
            speed = speed / sprintForMod;
            currentStam -= consumeStam * Time.deltaTime;
            if (currentStam < 0)
                currentStam = 0f;
            lastRegen = Time.time;
        }
        else if (!Input.GetKey(KeyCode.LeftShift) || (!forwardCheck && !lateralCheck && !backwardCheck))
        {
            MovePlayer();
            if (currentStam < stamPool)
                RegenStamina();
        }
        else
        {
            MovePlayer();
        }

        frameCounterCeiling = (1 / Time.deltaTime) / 15;

        if(frameUpdateCounter >= frameCounterCeiling || lastStamChunk < lastStamValue - currentStam)
        {
            SetCurrentStam(currentStam);
            lastStamChunk = lastStamValue - currentStam;
            lastStamValue = currentStam;
            frameUpdateCounter = 0;
        }

        frameUpdateCounter++;
        if (Input.GetKey(KeyCode.LeftAlt) || Input.GetKey(KeyCode.RightAlt))
            crosshair.enabled = false;
        else
            crosshair.enabled = true;

        DetectClicks();
    }

    void MovePlayer()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move.Normalize();
        controller.Move(move * speed * Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void DetectClicks()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 1.5f))
        {
            if (hit.transform.name == "Flashlight" || hit.transform.name == "Flashlight(Clone)" || hit.transform.name == "BatteryFlashlight"
                || hit.transform.name == "BatteryFlashlight(Clone)" || hit.transform.name == "BatteryFlashlight2" || hit.transform.name == "BatteryFlashlight2(Clone)"
                || hit.transform.name == "Beeper" || hit.transform.name == "Beeper(Clone)" || hit.transform.name == "Stimpack" || hit.transform.name == "Stimpack(Clone)"
                || hit.transform.name == "SuperStimpack" || hit.transform.name == "SuperStimpack(Clone)" || hit.transform.name == "Level1Keycard" 
                || hit.transform.name == "Level1Keycard(Clone)" || hit.transform.name == "Level1Keycard (1)" || hit.transform.name == "Level1Keycard (1)(Clone)"
                || hit.transform.name == "Level2Keycard" || hit.transform.name == "Level2Keycard(Clone)" || hit.transform.name == "Level3Keycard" || hit.transform.name == "Level3Keycard(Clone)"
                || hit.transform.name == "Level4Keycard" || hit.transform.name == "Level4Keycard(Clone)" || hit.transform.name == "Level5Keycard" || hit.transform.name == "Level5Keycard(Clone)")
            {
                crosshair.color = new Color32(0, 188, 54, 255);
            }
            else
            {
                crosshair.color = new Color32(255, 66, 60, 255);
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (hit.transform.name == "Flashlight" || hit.transform.name == "Flashlight(Clone)")
                {
                    transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<Flashlight>().SetGrabbed(true);
                    transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<Flashlight>().RestorePower();
                    Destroy(hit.transform.gameObject);
                }

                if(hit.transform.name == "BatteryFlashlight" || hit.transform.name == "BatteryFlashlight(Clone)" || hit.transform.name == "BatteryFlashlight2" || hit.transform.name == "BatteryFlashlight2(Clone)") {
                    if(hit.transform.GetComponent<FlashlightBatteryAdvanced>() != null)
                    {
                        transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                        transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<Flashlight>().SetGrabbed(true);
                        transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<Flashlight>().UpgradeFlashlight();
                        transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<Flashlight>().RestorePower();
                        Destroy(hit.transform.gameObject);
                    }
                    else
                    {
                        transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                        transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<Flashlight>().SetGrabbed(true);
                        transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<Flashlight>().RestorePower(75f);
                        Destroy(hit.transform.gameObject);
                    }
                }

                if ((hit.transform.name == "Beeper" || hit.transform.name == "Beeper(Clone)") && !Input.GetKeyUp(KeyCode.G) && beepers < 1)
                {
                    beepers++;
                    Destroy(hit.transform.gameObject);
                }

                if(hit.transform.name == "Stimpack" || hit.transform.name == "Stimpack(Clone)")
                {
                    stimpackInUse = hit.transform.GetComponent<Stimpack>().GetStimpackValue();
                    Destroy(hit.transform.gameObject);
                }
                else if(hit.transform.name == "SuperStimpack" || hit.transform.name == "SuperStimpack(Clone)")
                {
                    stimpackInUse = hit.transform.GetComponent<SuperStimpack>().GetStimpackValue();
                    Destroy(hit.transform.gameObject);
                }

                if(hit.transform.name == "Level1Keycard" || hit.transform.name == "Level1Keycard(Clone)" || hit.transform.name == "Level1Keycard (1)" || hit.transform.name == "Level1Keycard (1)(Clone)")
                {
                    Destroy(hit.transform.gameObject);
                    if (accessLevel < 1)
                        accessLevel = 1;
                }
                else if(hit.transform.name == "Level2Keycard" || hit.transform.name == "Level2Keycard(Clone)")
                {
                    Destroy(hit.transform.gameObject);
                    if (accessLevel < 2)
                        accessLevel = 2;
                }
                else if(hit.transform.name == "Level3Keycard" || hit.transform.name == "Level3Keycard(Clone)")
                {
                    Destroy(hit.transform.gameObject);
                    if (accessLevel < 3)
                        accessLevel = 3;
                }
                else if (hit.transform.name == "Level4Keycard" || hit.transform.name == "Level4Keycard(Clone)")
                {
                    Destroy(hit.transform.gameObject);
                    if (accessLevel < 4)
                        accessLevel = 4;
                }
                else if (hit.transform.name == "Level5Keycard" || hit.transform.name == "Level5Keycard(Clone)")
                {
                    Destroy(hit.transform.gameObject);
                    if (accessLevel < 5)
                        accessLevel = 5;
                }
            }
        }else
        {
            crosshair.color = new Color32(255, 66, 60, 255);
        }
    }

    void RegenStamina()
    {
        if (Time.time - lastRegen > regenRateTime)
        {
            if (stimpackInUse > 0)
            {
                currentStam += regenRateAmount * 3;
                stimpackInUse -= regenRateTime;
            }
            currentStam += regenRateAmount;
            if (currentStam > stamPool)
                currentStam = stamPool;

            lastRegen = Time.time;
        }
    }

    public void SetMaxStamina(float stamina)
    {
        staminaController.maxValue = stamina;
        staminaController.value = stamina;
    }

    public void SetCurrentStam(float stamina)
    {
        staminaController.value = stamina;
    }

}