using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public abstract class Menu : MonoBehaviour
{
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public Slider masterSlider;
    public Slider musicSlider;
    public AudioSource musicVolume;
    public AudioSource buttonSound;
    public string sceneToLoad;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        //Remember PlayerPrefs for Volume
        masterSlider.value = PlayerPrefs.GetFloat("Master Volume");
        musicSlider.value = PlayerPrefs.GetFloat("Music Volume");
    }

    public void OnSettingsClick()
    {
        buttonSound.Play();

        //Hide mainMenu Menu and Activate settingsMenu Menu
        mainPanel.SetActive(false);
        settingsPanel.SetActive(true);
    }

    public abstract void OnExitClick();

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

        AsyncOperation load = SceneManager.LoadSceneAsync(sceneToLoad);

        // Wait until the asynchronous scene fully loads
        while (!load.isDone)
        {
            yield return null;
        }
    }

    public abstract void OnBackClick();
}
