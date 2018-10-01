using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CongratulationsUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void MainMenuButtonClicked()
    {
        SceneManager.LoadScene("WelcomeScene");
    }

    public void QuitButtonClicked()
    {
        Application.Quit();
    }
}
