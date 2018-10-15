using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeScreenButtonActions : MonoBehaviour {

    private GameObject main;
    private GameObject options;

	// Use this for initialization
	void Start () {
        main = GameObject.Find("Main");
        options = GameObject.Find("Options");

        options.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void PlayButtonClicked()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void OptionsButtonClicked()
    {
        main.SetActive(false);
        options.SetActive(true);
    }

    public void BackButtonClicked()
    {
        options.SetActive(false);
        main.SetActive(true);
    }

    public void SettingsButtonClicked()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
