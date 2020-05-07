using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class Menu : MonoBehaviour
{
    public GameObject mainPanel;  //The current menu panel
    public GameObject settingsPanel;  //The current settings panel
    public Slider masterSlider;  //Adjusts the master volume
    public Slider musicSlider;  //Adjusts the music volume
    public AudioSource musicVolume;  //The music source
    public AudioSource buttonSound;  //The fun clicky button sound source
    public string sceneToLoad;  //Stores the next scene to load if the user exits the current menu

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Remember PlayerPrefs for Volume
        masterSlider.value = PlayerPrefs.GetFloat("Master Volume");
        musicSlider.value = PlayerPrefs.GetFloat("Music Volume");
    }

    public void OnSettingsClick()
    {
        //Play the fun clicky button sound
        buttonSound.Play();

        //Hide mainMenu Menu and Activate settingsMenu Menu
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public void MasterVolumeSlider()
    {
        //Attribute Master Volume to the masterSlider
        PlayerPrefs.SetFloat("Master Volume", masterSlider.value);

        //Set the slider
        AudioListener.volume = masterSlider.value;
    }

    public void MusicVolumeSlider()
    {
        //Attribute Music Volume to the musicSlider
        PlayerPrefs.SetFloat("Music Volume", musicSlider.value);

        //Set the slider
        musicVolume.volume = musicSlider.value;
    }

    IEnumerator LoadYourAsyncScene()
    {
        //Provide time for beep sounds and loading screens to play
        yield return new WaitForSeconds(0.5f);

        //Loads into the next relevant scene
        AsyncOperation load = SceneManager.LoadSceneAsync(sceneToLoad);

        // Wait until the asynchronous scene fully loads
        while (!load.isDone)
        {
            yield return null;
        }
    }

    //Operates code that executes when the button to completely exit the menu is clicked
    public abstract void OnExitClick();

    //Operates code that executes when the button to head back to the main panel (after
    //returning from the settings panel) is clicked
    public abstract void OnBackClick();
}
