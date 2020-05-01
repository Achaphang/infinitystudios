using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class In_Game_Menu : Menu
{
    public GameObject exitPanel;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject darkBackground;

    public bool menuActive = false;

    public void OnBackToGameClick()
    {
        buttonSound.Play();
        menuActive = false;
        Debug.Log("mA false");

        //Hide gameMenu
        mainPanel.SetActive(false);
        darkBackground.SetActive(false);

        //Unpause
        Time.timeScale = 1;
    }

    public override void OnBackClick()
    {
        buttonSound.Play();

        //Hide chooseInput Menu and Reactivate mainMenu
        mainPanel.SetActive(true);
        settingsPanel.SetActive(false);
    }

    public override void OnExitClick()
    {
        buttonSound.Play();

        //Hide gameMenu Menu and activate exitMenu
        mainPanel.SetActive(false);
        exitPanel.SetActive(true);
    }

    public void OnYesClick()
    {
        buttonSound.Play();
        menuActive = false;
        Debug.Log("mA false");

        //Unpause
        Time.timeScale = 1;

        //Go back to Main_Menu scene
        StartCoroutine("LoadYourAsyncScene");
    }

    public void OnNoClick()
    {
        buttonSound.Play();

        //Hide chooseInput Menu and Reactivate mainMenu
        mainPanel.SetActive(true);
        exitPanel.SetActive(false);
    }

    private void Update()
    {
        if (menuActive == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //Activate gameMenu
                mainPanel.SetActive(true);
                menuActive = true;
                darkBackground.SetActive(true);

                //Unlock cursor
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;

                //Pause game
                Time.timeScale = 0;

                Debug.Log("Game Menu Activated");
            }
        }
    }
}
