using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelUI : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void restartButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void nextLevelButtonClicked()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void mainMenuButtonClicked()
    {
        SceneManager.LoadScene("WelcomeScene");
    }
}
