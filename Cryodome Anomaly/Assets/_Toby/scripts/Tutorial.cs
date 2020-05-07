using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialPanel;
    public AudioSource buttonSound;  //The fun clicky button sound source
    public GameObject darkBackground;

    public bool tutorialActive = false;

    public void OnBackButtonClick()
    {
        buttonSound.Play();
        
        //Hide chooseInput Menu and Reactivate mainMenu
        tutorialPanel.SetActive(false);
        darkBackground.SetActive(false);

        //Unpause game
        Time.timeScale = 1;

        //release mouse
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (tutorialPanel.activeInHierarchy)
        {
            Debug.Log("I'm active you idiot");
            darkBackground.SetActive(true);

            //Unlock cursor
            tutorialActive = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            //Pause game
            Time.timeScale = 0;
        }
    }
}
