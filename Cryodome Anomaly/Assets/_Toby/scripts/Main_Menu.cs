using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu : Menu
{
    public GameObject chooseInput;  //The menu that allows the user to choose their input mode
    public GameObject startButton;  //Enters the rest of the main menu
    public GameObject loadingScreen;  //Shows while the main scene is loading

    private new void Start()
    {
        //Unlock cursor (for transition between scenes)
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        base.Start();  //Execute code from Parent Class
    }

    //Executes when the startButton is clicked
    public void OnPlayGameClick()
    {
        //Hide startButton and show the mainPanel
        mainPanel.SetActive(true);
        startButton.SetActive(false);
        buttonSound.Play();  //Play the fun clicky button sound
    }

    //Activates game in Normal Mode
    public void OnNormalModeClick()
    {
        buttonSound.Play();  //Play the fun clicky button sound

        PlayerPrefs.SetInt("Mode", 1);  //Remember PlayerPref for Scene Change

        //Hide mainMenu Menu and Activate chooseInput Menu
        mainPanel.SetActive(false);
        chooseInput.SetActive(true);
    }

    //Activates game in BC Mode
    public void OnBCModeClick()
    {
        buttonSound.Play();  //Play the fun clicky button sound

        PlayerPrefs.SetInt("Mode", 2);   //Remember PlayerPref for Scene Change
        if (Globals.Instance != null)
            Globals.Instance.difficulty = -1;

        //Hide mainMenu Menu and Activate chooseInput Menu
        mainPanel.SetActive(false);
        chooseInput.SetActive(true);
    }

    //Shows Leaderboard
    public void OnLeaderboardClick()
    {
        buttonSound.Play();  //Play the fun clicky button sound

        //Waiting for "database" to exist
    }
    
    //Starts the game with the VR PlayerPref activated
    public void OnVirtualRealityClick(int diff)
    {
        if (Globals.Instance != null)
            Globals.Instance.difficulty = diff;
        buttonSound.Play();  //Play the fun clicky button sound
        loadingScreen.SetActive(true);  //Show loading screen
        musicVolume.mute = true;  //Stop main menu music

        PlayerPrefs.SetInt("Input", 1);   //Remember PlayerPref for Scene Change

        //Open Game Scene
        StartCoroutine("LoadYourAsyncScene");
    }

    //Starts the game with the Keyboard/Mouse activated
    public void OnKeyboardClick()
    {
        buttonSound.Play();  //Play the fun clicky button sound
        loadingScreen.SetActive(true);  //Show loading screen
        musicVolume.mute = true;  //Stop main menu music

        PlayerPrefs.SetInt("Input", 2);   //Remember PlayerPref for Scene Change

        //Open Game Scene
        StartCoroutine("LoadYourAsyncScene");
    }

    //Exits the game and closes the application
    public override void OnExitClick()
    {
        buttonSound.Play();  //Play the fun clicky button sound

        Application.Quit();   //Quit Application
    }

    //Returns to the mainPanel from the settingsPanel
    public override void OnBackClick()
    {
        buttonSound.Play();  //Play the fun clicky button sound

        //Hide chooseInput Menu and Reactivate mainMenu
        mainPanel.SetActive(true);
        chooseInput.SetActive(false);
        settingsPanel.SetActive(false);
    }
}
