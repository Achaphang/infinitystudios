using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGame : MonoBehaviour
{
    public Image fade;
    public GameObject creditText;
    public GameObject marker;
    public GameObject exitButton;
    public Transform playerTransform;
    public AudioSource gameEndMusic;
    public AudioSource currentMusic;
    public Timer timer;

    private void OnTriggerEnter(Collider player)
    {
        //Set player speed to 0 - cant move
        playerTransform.GetComponent<PlayerController3D>().speed = 0;

        //Set player sensitivity to 0 - cant look around
        playerTransform.Find("Camera").GetComponent<MouseController>().sensitivity = 0;
        playerTransform.Find("Camera").GetComponent<MouseController>().end = true;

        //Stop Timer
        timer.stopTimer(true);

        //Display Canvas
        transform.Find("Canvas").gameObject.SetActive(true);

        //Play End Music and Stop Current Music
        currentMusic.Stop();
        gameEndMusic.Play();
        gameEndMusic.volume = PlayerPrefs.GetFloat("Music Volume");

        //Start fade coroutine
        StartCoroutine("fadeScreen");

        //Start move coroutine
        StartCoroutine("moveToTheEnd");
    }

    IEnumerator moveToTheEnd()
    {
        //Make sure facing exactly where we want
        playerTransform.position = new Vector3(4,playerTransform.position.y,playerTransform.position.z);
        playerTransform.rotation = Quaternion.identity;
        playerTransform.Find("Camera").localRotation = Quaternion.identity;

        //Start move loop forward
        for (int i = 0; i < 1000; i++)
        {
            playerTransform.Translate(0, 0, 0.05f);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator fadeScreen()
    {
        //Start fade loop
        for (float i = 0; i < 1; i+=0.001f)
        {
            fade.color = new Color(255,255,255,i);
            yield return new WaitForSeconds(0.01f);
        }

        float distanceFromStop = marker.transform.position.y - exitButton.transform.position.y;
        distanceFromStop /= 3000;
        //After loop
        while (creditText.transform.position.y < 1650)
        {
            creditText.transform.Translate(0, 1, 0);
            Debug.Log(creditText.transform.position.y);
            yield return new WaitForSeconds(0.03f);
        }

        //Unlock mouse
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        //Unlock mouse
        //Exit game button visable after some time, because maybe we want them to have to read the credits
    }
}
