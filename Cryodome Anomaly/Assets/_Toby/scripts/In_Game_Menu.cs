using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class In_Game_Menu : MonoBehaviour
{
    public GameObject gameMenu;
    public GameObject gameSettingsMenu;
    public GameObject exitMenu;
    public GameObject yesButton;
    public GameObject noButton;
    public GameObject darkBackground;
    //public GameObject loadingScreen;
    public Slider masterSlider;
    public Slider musicSlider;
    public AudioSource musicVolume;
    public AudioSource buttonSound;

    public bool menuActive = false;

    public void OnBackToGameClick()
    {
        buttonSound.Play();
        menuActive = false;
        Debug.Log("mA false");

        //Hide gameMenu
        gameMenu.SetActive(false);
        darkBackground.SetActive(false);

        //Unpause
        Time.timeScale = 1;
    }

    public void OnGameSettingsClick()
    {
        buttonSound.Play();

        //Hide gameMenu Menu and activate exitMenu
        gameMenu.SetActive(false);
        gameSettingsMenu.SetActive(true);
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

    public void OnBackClick()
    {
        buttonSound.Play();

        //Hide chooseInput Menu and Reactivate mainMenu
        gameMenu.SetActive(true);
        gameSettingsMenu.SetActive(false);
    }

    public void OnExitGameClick()
    {
        buttonSound.Play();

        //Hide gameMenu Menu and activate exitMenu
        gameMenu.SetActive(false);
        exitMenu.SetActive(true);
    }

    public void OnYesClick()
    {
        buttonSound.Play();
        menuActive = false;
        Debug.Log("mA false");

        //Unpause
        Time.timeScale = 1;

        //Go back to Main_Menu scene
        StartCoroutine(LoadYourAsyncScene());
    }

    public void OnNoClick()
    {
        buttonSound.Play();

        //Hide chooseInput Menu and Reactivate mainMenu
        gameMenu.SetActive(true);
        exitMenu.SetActive(false);
    }

    IEnumerator LoadYourAsyncScene()
    {
        yield return new WaitForSeconds(1);

        //Unlock cursor
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        AsyncOperation load = SceneManager.LoadSceneAsync("MainMenu");

        // Wait until the asynchronous scene fully loads
        while (!load.isDone)
        {
            yield return null;
        }
    }

    private void Update()
    {
        if (menuActive == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                //Activate gameMenu
                gameMenu.SetActive(true);
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
