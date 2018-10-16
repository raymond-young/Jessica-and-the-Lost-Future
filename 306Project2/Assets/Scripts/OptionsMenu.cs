using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class OptionsMenu : MonoBehaviour {

    private Resolution[] screenResolutions;

    public Dropdown resolutionDropdown;
    public AudioMixer mixer;
    public Slider musicSlider;
    public Slider effectsSlider;
    public GameObject optionsMenu;
    public static bool isPaused = false;

    void Start ()
    {
        screenResolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int current = 0;

        for (int i = 0; i < screenResolutions.Length; i++)
        {
            options.Add(screenResolutions[i].width + " x " + screenResolutions[i].height);

            if (screenResolutions[i].width == Screen.currentResolution.width &&
                screenResolutions[i].height == Screen.currentResolution.height)
            {
                current = i;
            }
        }
        float effectsValue = 0;
        float musicValue = 0;

        mixer.GetFloat("effects", out effectsValue);
        mixer.GetFloat("music", out musicValue);

        Debug.Log(effectsValue);
        Debug.Log(musicValue);


        effectsSlider.value = Mathf.Pow(10f, effectsValue / 20);
        musicSlider.value = Mathf.Pow(10f, musicValue / 20);

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = current;
        resolutionDropdown.RefreshShownValue();

    }

    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Debug.Log("resume");
                Resume();
            } else
            {
                Debug.Log("pause");
                Pause();
            }
        }
    }

    public void SetMusicVolume(float volume)
    {
        mixer.SetFloat("music", Mathf.Log10(volume) * 20);
    }

    public void SetEffectsVolume(float volume)
    {
        mixer.SetFloat("effects", Mathf.Log10(volume) * 20);
    }

    public void SetFullscreen(bool fullscreen)
    {
        Screen.fullScreen = fullscreen;
    }

    public void SetQuality(int quality)
    {
        QualitySettings.SetQualityLevel(quality);
    }

    public void SetResolution(int index)
    {
        Resolution resolution = screenResolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void Resume()
    {
        optionsMenu.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void Quit()
    {
        Resume();
        SceneManager.LoadScene("WelcomeScene");
    }

    private void Pause()
    {
        optionsMenu.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }


}
