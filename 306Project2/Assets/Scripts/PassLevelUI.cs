using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PassLevelUI : MonoBehaviour {

    private string score;
    public UnityEngine.UI.Text scoreText;
	// Use this for initialization
	void Start () {
        GameObject object1 = GameObject.FindGameObjectWithTag("scoreTransferObject");
        score = object1.GetComponent<ScoreTransferScript>().getScore();
        scoreText.text = score.ToString();
	}
	
	// Update is called once per frame
	void Update () {

	}

    public void continueButtonClicked()
    {
        SceneManager.LoadScene("Level-1");
    }

    public void mainMenuButtonClicked()
    {
        SceneManager.LoadScene("WelcomeScene");
    }

    public void setScore(float score)
    {
        scoreText.text = score.ToString();
    }
}
