using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeScreenButtonActions : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void playButtonClicked()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void settingsButtonClicked()
    {
        SceneManager.LoadScene("SettingsScene");
    }

    public void quitButtonClicked()
    {
        Application.Quit();
    }

    public void HighScoreButtonClick()
    {
        SceneManager.LoadScene("HighScore");
    }
}
