using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class WelcomeScreenButtonActions : MonoBehaviour {

    public GameObject main;
    public GameObject options;
    public GameObject levelSelect;
    public GameObject difficulty;
    public GameObject playerTransfer;

    public GameObject inputName;

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
        string playerName = inputName.GetComponent<InputField>().text;
        
        if (playerName != null && !playerName.Equals(""))
        {
            if (File.Exists(Application.persistentDataPath + "/" + playerName + ".dat"))
            {
                //ToDo give some feedback that the name is taken
            }
            else
            {
                BinaryFormatter bf = new BinaryFormatter();
                FileStream file = File.Create(Application.persistentDataPath + "/" + playerName + ".dat");
                bf.Serialize(file, new SaveData(0, 0, playerName));
                file.Close();
            }
            playerTransfer.GetComponent<PlayerNameObjectTransfer>().SetPlayerName(playerName);

            SceneManager.LoadScene(level);
        }
        else
        {
            //TODO error, enter name here screen
        }

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
        SceneManager.LoadScene("HighScore");
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

    public void HighScoreButtonClick()
    {
        SceneManager.LoadScene("HighScore");
    }
}
