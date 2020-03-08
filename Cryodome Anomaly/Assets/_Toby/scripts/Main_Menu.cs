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
    public Slider masterSlider;
    public Slider musicSlider;
    public AudioSource musicVolume;

    public void OnNormalModeClick()
    {
        PlayerPrefs.SetInt("Mode", 1);   //Remember PlayerPref for Scene Change

        //Hide mainMenu Menu and Activate chooseInput Menu
        mainMenu.SetActive(false);
        chooseInput.SetActive(true);
    }

    public void OnBCModeClick()
    {
        PlayerPrefs.SetInt("Mode", 2);   //Remember PlayerPref for Scene Change

        //Hide mainMenu Menu and Activate chooseInput Menu
        mainMenu.SetActive(false);
        chooseInput.SetActive(true);
    }

    public void OnSettingsClick()
    {
        //Hide mainMenu Menu and Activate settingsMenu Menu
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OnLeaderboardClick()
    {

    }

    public void OnExitGameClick()
    {
        Application.Quit();   //Quit Application
    }

    public void OnVirtualRealityClick()
    {
        PlayerPrefs.SetInt("Input", 1);   //Remember PlayerPref for Scene Change

        //Open Game Scene
        SceneManager.LoadScene("main");
    }

    public void OnKeyboardClick()
    {
        PlayerPrefs.SetInt("Input", 2);   //Remember PlayerPref for Scene Change

        //Open Game Scene
        SceneManager.LoadScene("main");
    }

    public void OnBackClick()
    {
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
}
