using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WelcomeScreenButtonActions : MonoBehaviour {

    public GameObject main;
    public GameObject options;
    public GameObject levelSelect;
    public GameObject difficulty;

    private string level = "Tutorial";

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {

    }

    public void NewGameButtonClicked()
    {
        level = "Tutorial";
        ShowDifficulty();
    }

    public void PlayButtonClicked()
    {
        SceneManager.LoadScene(level);
    }

    public void PlayChapterOne()
    {
        level = "Level-1";
        ShowDifficulty();
    }

    public void PlayChapterTwo()
    {
        level = "Level-2";
        ShowDifficulty();
    }

    public void PlayChapterThree()
    {
        level = "Level-3";
        ShowDifficulty();
    }

    public void ShowDifficulty()
    {
        main.SetActive(false);
        levelSelect.SetActive(false);
        difficulty.SetActive(true);
    }

    public void OptionsButtonClicked()
    {
        main.SetActive(false);
        options.SetActive(true);
    }

    public void LevelSelectButtonClicked()
    {
        main.SetActive(false);
        levelSelect.SetActive(true);
    }

    public void DifficultyBackButtonClicked()
    {
        if (level == "Tutorial")
        {
            difficulty.SetActive(false);
            main.SetActive(true);
        }
        else
        {
            difficulty.SetActive(false);
            levelSelect.SetActive(true);
        }
    }

    public void BackButtonClicked()
    {
        options.SetActive(false);
        levelSelect.SetActive(false);
        difficulty.SetActive(false);
        main.SetActive(true);
    }

    public void HighScoreButtonClicked()
    {
        
    }

    public void AchievementsButtonClicked()
    {

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
