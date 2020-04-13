using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Main_Menu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject chooseInput;
    public GameObject settingsMenu;
    public GameObject startButton;
    public GameObject loadingScreen;
    public Slider masterSlider;
    public Slider musicSlider;
    public AudioSource musicVolume;
    public AudioSource buttonSound;

    public void OnPlayGameClick()
    {
        mainMenu.SetActive(true);
        startButton.SetActive(false);
        buttonSound.Play();
    }

    public void OnNormalModeClick()
    {
        buttonSound.Play();

        PlayerPrefs.SetInt("Mode", 1);   //Remember PlayerPref for Scene Change

        //Hide mainMenu Menu and Activate chooseInput Menu
        mainMenu.SetActive(false);
        chooseInput.SetActive(true);
    }

    public void OnBCModeClick()
    {
        buttonSound.Play();

        PlayerPrefs.SetInt("Mode", 2);   //Remember PlayerPref for Scene Change

        //Hide mainMenu Menu and Activate chooseInput Menu
        mainMenu.SetActive(false);
        chooseInput.SetActive(true);
    }

    public void OnSettingsClick()
    {
        buttonSound.Play();

        //Hide mainMenu Menu and Activate settingsMenu Menu
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OnLeaderboardClick()
    {
        buttonSound.Play();

        //Waiting for database to exist
    }

    public void OnExitGameClick()
    {
        buttonSound.Play();

        Application.Quit();   //Quit Application
    }

    public void OnVirtualRealityClick()
    {
        buttonSound.Play();
        loadingScreen.SetActive(true);
        musicVolume.mute = true;

        PlayerPrefs.SetInt("Input", 1);   //Remember PlayerPref for Scene Change

        //Open Game Scene
        StartCoroutine(LoadYourAsyncScene());
    }

    public void OnKeyboardClick()
    {
        buttonSound.Play();
        loadingScreen.SetActive(true);
        musicVolume.mute = true;

        PlayerPrefs.SetInt("Input", 2);   //Remember PlayerPref for Scene Change

        //Open Game Scene
        StartCoroutine(LoadYourAsyncScene());
    }

    public void OnBackClick()
    {
        buttonSound.Play();

        //Hide chooseInput Menu and Reactivate mainMenu
        mainMenu.SetActive(true);
        chooseInput.SetActive(false);
        settingsMenu.SetActive(false);
    }

    public void MasterVolumeSlider()
    {
        PlayerPrefs.SetFloat("Master Volume", masterSlider.value);

        AudioListener.volume = masterSlider.value;
    }

    public void MusicVolumeSlider()
    {
        PlayerPrefs.SetFloat("Music Volume", musicSlider.value);

        musicVolume.volume = musicSlider.value;
    }

    IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSeconds(1);

        AsyncOperation load = SceneManager.LoadSceneAsync("main");

        // Wait until the asynchronous scene fully loads
        while (!load.isDone)
        {
            yield return null;
        }
    }
}
