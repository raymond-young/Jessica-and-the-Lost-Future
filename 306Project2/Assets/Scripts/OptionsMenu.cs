using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour {

    private Resolution[] screenResolutions;

    public Dropdown resolutionDropdown;
    public AudioMixer mixer;

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

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = current;
        resolutionDropdown.RefreshShownValue();

    }

    public void SetVolume(float volume)
    {
        mixer.SetFloat("volume", volume);
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



}
