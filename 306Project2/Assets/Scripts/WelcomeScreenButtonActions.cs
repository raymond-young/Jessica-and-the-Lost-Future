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
    public GameObject highScore;
    public GameObject pressEnter;

    public GameObject errorObject;

    public GameObject inputName;

    private string level = "Tutorial";
    private bool lockEnter;

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
        Debug.Log("play clicked");
        string playerName = inputName.GetComponent<InputField>().text;

        if (playerName != null && !playerName.Equals(""))
        {
            if (!lockEnter)
            {
                lockEnter = true;
                //continue and there is a save file
                if (File.Exists(Application.persistentDataPath + "/" + playerName + ".dat") && level == "LevelSelect")
                {
                    //do nothing simply load the level select scene
                    playerTransfer.GetComponent<PlayerNameObjectTransfer>().SetPlayerName(playerName);
                    SceneManager.LoadScene(level);
                }
                else if (File.Exists(Application.persistentDataPath + "/" + playerName + ".dat"))
                {
                    //throw an error as trying to create a new game with a taken user name
                    errorObject.GetComponent<Text>().text = "Username taken, please enter a unique username";
                }
                else if (!File.Exists(Application.persistentDataPath + "/" + playerName + ".dat"))
                {
                    //Start a new game and create the default storage file
                    BinaryFormatter bf = new BinaryFormatter();
                    FileStream file = File.Create(Application.persistentDataPath + "/" + playerName + ".dat");
                    bf.Serialize(file, new SaveData(0, 0, playerName));
                    file.Close();

                    playerTransfer.GetComponent<PlayerNameObjectTransfer>().SetPlayerName(playerName);
                    SceneManager.LoadScene(level);
                }
                else if (!File.Exists(Application.persistentDataPath + "/" + playerName + ".dat") && level == "LevelSelect")
                {
                    //Trying to continue a game without a valid save file throw an error
                    errorObject.GetComponent<Text>().text = "No save file found for username";
                }
                StartCoroutine(LockRoutine());
            }

        }
        else
        {
            errorObject.GetComponent<Text>().text = "Please enter a valid username";
        }
    }
    
    private IEnumerator LockRoutine()
    {
        yield return new WaitForSeconds(0.01f);
        lockEnter = false;
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
        errorObject.GetComponent<Text>().text = "";
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
        if (level == "Tutorial" || level == "LevelSelect")
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
        highScore.SetActive(false);
        main.SetActive(true);
        pressEnter.SetActive(true);
    }

    public void HighScoreButtonClicked()
    {
        main.SetActive(false);
        highScore.SetActive(true);
        pressEnter.SetActive(false);
    }

    public void AchievementsButtonClicked()
    {
        SceneManager.LoadScene("AchievementScene");
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

    public void ContinueButtonClicked(){
        level = "LevelSelect";
        ShowDifficulty();
    }
}
