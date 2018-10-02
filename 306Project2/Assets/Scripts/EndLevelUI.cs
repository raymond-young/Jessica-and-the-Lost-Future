using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelUI : MonoBehaviour {

    private float score;
    public UnityEngine.UI.Text scoreText;
	// Use this for initialization
	void Start () {
        Debug.Log("start has been called");
        GameObject object1 = GameObject.FindGameObjectWithTag("scoreTransferObject");
        score = object1.GetComponent<ScoreTransferScript>().getScore();
        scoreText.text = "score: " + score;
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

    public void setScore(float score)
    {
        scoreText.text = score.ToString();
    }
}
