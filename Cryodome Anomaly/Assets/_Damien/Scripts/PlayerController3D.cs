using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController3D : MonoBehaviour
{
    CharacterController controller;
    public Slider staminaController;
    public float speed = 3f;
    public float sprintForMod = 1.45f;
    public float sprintForLateralMod = 1.25f;
    bool forwardCheck = false;
    bool lateralCheck = false;
    bool backwardCheck = false;
    public float stamPool = 24f;
    public float currentStam = 24f;
    public float consumeStam = 8f;
    public float regenRateTime = 3f;
    public float regenRateAmount = 2.5f;
    float lastRegen;
    float gravity = -9.81f;
    Vector3 velocity;
    int beepers = 0;
    int frameUpdateCounter = 0;
    float frameCounterCeiling = 0f;

    float lastStamChunk = 0f;
    float lastStamValue = 0f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        SetMaxStamina(stamPool);
        lastStamValue = stamPool;
    }

    void Update()
    {
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
            currentStam = currentStam - Time.deltaTime;
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
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.name == "Flashlight" || hit.transform.name == "Flashlight(Clone)" || hit.transform.name == "BatteryFlashlight" || hit.transform.name == "BatteryFlashlight(Clone)")
                {
                    transform.GetChild(1).GetChild(1).gameObject.SetActive(true);
                    transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<Flashlight>().SetGrabbed(true);
                    transform.GetChild(1).GetChild(1).GetChild(0).GetChild(0).GetComponent<Flashlight>().RestorePower();
                    Destroy(hit.transform.gameObject);
                }
                if (hit.transform.name == "Beeper" || hit.transform.name == "Beeper(Clone)")
                {
                    beepers++;
                    Destroy(hit.transform.gameObject);
                }
            }
        }
    }

    void RegenStamina()
    {
        if (Time.time - lastRegen > regenRateTime)
        {
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