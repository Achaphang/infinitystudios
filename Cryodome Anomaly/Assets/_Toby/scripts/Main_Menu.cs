using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu : Menu
{
    public GameObject chooseInput;
    public GameObject startButton;
    public GameObject loadingScreen;

    private new void Start()
    {
        //Unlock cursor (for transition between scenes)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        base.Start();
    }

    public void OnPlayGameClick()
    {
        mainPanel.SetActive(true);
        startButton.SetActive(false);
        buttonSound.Play();
    }

    public void OnNormalModeClick()
    {
        buttonSound.Play();

        PlayerPrefs.SetInt("Mode", 1);   //Remember PlayerPref for Scene Change

        //Hide mainMenu Menu and Activate chooseInput Menu
        mainPanel.SetActive(false);
        chooseInput.SetActive(true);
    }

    public void OnBCModeClick()
    {
        buttonSound.Play();

        PlayerPrefs.SetInt("Mode", 2);   //Remember PlayerPref for Scene Change

        //Hide mainMenu Menu and Activate chooseInput Menu
        mainPanel.SetActive(false);
        chooseInput.SetActive(true);
    }

    public void OnLeaderboardClick()
    {
        buttonSound.Play();

        //Waiting for database to exist
    }
    

    public void OnVirtualRealityClick()
    {
        buttonSound.Play();
        loadingScreen.SetActive(true);
        musicVolume.mute = true;

        PlayerPrefs.SetInt("Input", 1);   //Remember PlayerPref for Scene Change

        //Open Game Scene
        StartCoroutine("LoadYourAsyncScene");
    }

    public void OnKeyboardClick()
    {
        buttonSound.Play();
        loadingScreen.SetActive(true);
        musicVolume.mute = true;

        PlayerPrefs.SetInt("Input", 2);   //Remember PlayerPref for Scene Change

        //Open Game Scene
        StartCoroutine("LoadYourAsyncScene");
    }

    public override void OnExitClick()
    {
        buttonSound.Play();

        Application.Quit();   //Quit Application
    }

    public override void OnBackClick()
    {
        buttonSound.Play();

        //Hide chooseInput Menu and Reactivate mainMenu
        mainPanel.SetActive(true);
        chooseInput.SetActive(false);
        settingsPanel.SetActive(false);
    }
}
