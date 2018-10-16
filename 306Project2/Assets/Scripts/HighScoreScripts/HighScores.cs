using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScores : MonoBehaviour {

    public GameObject totalPanelEasy;
    public GameObject level1PanelEasy;
    public GameObject level2PanelEasy;
    public GameObject level3PanelEasy;

    public GameObject totalPanelHard;
    public GameObject level1PanelHard;
    public GameObject level2PanelHard;
    public GameObject level3PanelHard;

    public GameObject easyButton;
    public GameObject hardButton;

    public GameObject playerListPrefab;

    private Color color = new Color(0.9433962f, 0.511748f, 0.511748f);
    private Color levelColor = new Color(0.1135636f, 0.5811083f, 0.8301887f);

    private float startPosForFormat = 130f;
    private float heightInterval = 30f;

    private GameObject highScorePanel;

    //Initialize gamemode to 0 for easy (default) at 1 for hard
    private int gameMode = 0;

    private int selectedDifferculty;
    private int levelSelected;

    private List<GameObject> currentPlayers = new List<GameObject>();

	// Use this for initialization
	void Start () {

        selectedDifferculty = 0;
        levelSelected = 0;

        //Initalize reading from file here into (highScorePlayers)
        level1PanelEasy.SetActive(false);
        level2PanelEasy.SetActive(false);
        level3PanelEasy.SetActive(false);

        totalPanelHard.SetActive(false);
        level1PanelHard.SetActive(false);
        level2PanelHard.SetActive(false);
        level3PanelHard.SetActive(false);

        DirectoryInfo info = new DirectoryInfo(Application.persistentDataPath);

        SaveManager saveManager = new SaveManager();

        List<SaveData> saves = saveManager.LoadSave();

        float format = 0;
        //Loads players to appropriate level
        for (int i = 0; i < saves.Count; i++)
        {
            format = startPosForFormat - heightInterval - format;
            SaveData data = saves[i];

            ReadFile(data.GetLevel(), data.GetDifferculty(), data, format);
        }
        



        //Sets up buttons, default easy is selected
        easyButton.GetComponent<Image>().color = color;

        //Gets reference to highscorepanel
        highScorePanel = GameObject.FindGameObjectWithTag("HighScorePanel");
    }

    // Update is called once per frame
    void Update () {
        

    }

    //Refreshes players on game completion
    public void RefreshPlayers(string player, int score)
    {
        //Loop through all current players to update
        for (int i = 0; i < currentPlayers.Count; i++)
        {
            //Checks the name of the object is equal to the name of the player
            if (currentPlayers[i].name.Equals(player))
            {
                currentPlayers[i].GetComponent<Text>().text = player + " " + score.ToString();
                break;
            }
        }
    }

    //Reads files related to gamemode and level to show high scores
    private void ReadFile(int level, int gameMode, SaveData data, float format)
    {
        GameObject parentPanel = null;

        if (gameMode == 0)
        {
            if (level == 0)
            {
                parentPanel = totalPanelEasy;
            }
            else if (level == 1)
            {
                parentPanel = level1PanelEasy;
            }
            else if (level == 2)
            {
                parentPanel = level2PanelEasy;
            }
            else if (level == 3)
            {
                parentPanel = level3PanelEasy;
            }
        }
        else if (gameMode == 1)
        {
            if (level == 0)
            {
                parentPanel = totalPanelHard;
            }
            else if (level == 1)
            {
                parentPanel = level1PanelHard;
            }
            else if (level == 2)
            {
                parentPanel = level2PanelHard;
            }
            else if (level == 3)
            {
                parentPanel = level3PanelHard;
            }
        }

        GameObject p = Instantiate(playerListPrefab, parentPanel.transform);

        p.transform.localPosition = new Vector2(0f, format);
        p.GetComponent<Text>().text = data.GetPlayerName() + " " + data.GetScore().ToString();
        p.GetComponent<Text>().fontSize = 40;
        p.name = data.GetPlayerName();

    }

    public void Level1()
    {
        levelSelected = 1;
        if (selectedDifferculty == 0)
        {
            //Toggle panels
            level1PanelEasy.SetActive(true);
            level2PanelEasy.SetActive(false);
            level3PanelEasy.SetActive(false);
            totalPanelEasy.SetActive(false);

            totalPanelHard.SetActive(false);
            level1PanelHard.SetActive(false);
            level2PanelHard.SetActive(false);
            level3PanelHard.SetActive(false);
        }
        else if (selectedDifferculty == 1)
        {
            //Toggle panels
            level1PanelEasy.SetActive(false);
            level2PanelEasy.SetActive(false);
            level3PanelEasy.SetActive(false);
            totalPanelEasy.SetActive(false);

            totalPanelHard.SetActive(false);
            level1PanelHard.SetActive(true);
            level2PanelHard.SetActive(false);
            level3PanelHard.SetActive(false);
        }



        //Color change code
        for (int i = 0; i < highScorePanel.transform.childCount; i++)
        {
            highScorePanel.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
        highScorePanel.transform.GetChild(0).GetComponent<Image>().color = levelColor;

    }

    public void Level2()
    {
        levelSelected = 2;
        if (selectedDifferculty == 0)
        {
            //Toggle panels
            level1PanelEasy.SetActive(false);
            level2PanelEasy.SetActive(true);
            level3PanelEasy.SetActive(false);
            totalPanelEasy.SetActive(false);

            totalPanelHard.SetActive(false);
            level1PanelHard.SetActive(false);
            level2PanelHard.SetActive(false);
            level3PanelHard.SetActive(false);
        }
        else if (selectedDifferculty == 1)
        {
            //Toggle panels
            level1PanelEasy.SetActive(false);
            level2PanelEasy.SetActive(false);
            level3PanelEasy.SetActive(false);
            totalPanelEasy.SetActive(false);

            totalPanelHard.SetActive(false);
            level1PanelHard.SetActive(false);
            level2PanelHard.SetActive(true);
            level3PanelHard.SetActive(false);
        }

        //Color change code
        for (int i = 0; i < highScorePanel.transform.childCount; i++)
        {
            highScorePanel.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
        highScorePanel.transform.GetChild(1).GetComponent<Image>().color = levelColor;
    }

    public void Level3()
    {
        levelSelected = 3;
        if (selectedDifferculty == 0)
        {
            //Toggle panels
            level1PanelEasy.SetActive(false);
            level2PanelEasy.SetActive(false);
            level3PanelEasy.SetActive(true);
            totalPanelEasy.SetActive(false);

            totalPanelHard.SetActive(false);
            level1PanelHard.SetActive(false);
            level2PanelHard.SetActive(false);
            level3PanelHard.SetActive(false);
        }
        else if (selectedDifferculty == 1)
        {
            //Toggle panels
            level1PanelEasy.SetActive(false);
            level2PanelEasy.SetActive(false);
            level3PanelEasy.SetActive(false);
            totalPanelEasy.SetActive(false);

            totalPanelHard.SetActive(false);
            level1PanelHard.SetActive(false);
            level2PanelHard.SetActive(false);
            level3PanelHard.SetActive(true);
        }


        //Color change code
        for (int i = 0; i < highScorePanel.transform.childCount; i++)
        {
            highScorePanel.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
        highScorePanel.transform.GetChild(2).GetComponent<Image>().color = levelColor;
    }

    public void Total()
    {
        levelSelected = 0;
        if (selectedDifferculty == 0)
        {
            //Toggle panels
            level1PanelEasy.SetActive(false);
            level2PanelEasy.SetActive(false);
            level3PanelEasy.SetActive(false);
            totalPanelEasy.SetActive(true);

            totalPanelHard.SetActive(false);
            level1PanelHard.SetActive(false);
            level2PanelHard.SetActive(false);
            level3PanelHard.SetActive(false);
        }
        else if (selectedDifferculty == 1)
        {
            //Toggle panels
            level1PanelEasy.SetActive(false);
            level2PanelEasy.SetActive(false);
            level3PanelEasy.SetActive(false);
            totalPanelEasy.SetActive(false);

            totalPanelHard.SetActive(true);
            level1PanelHard.SetActive(false);
            level2PanelHard.SetActive(false);
            level3PanelHard.SetActive(false);
        }


        //Color change code
        for (int i = 0; i < highScorePanel.transform.childCount; i++)
        {
            highScorePanel.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
        highScorePanel.transform.GetChild(3).GetComponent<Image>().color = levelColor;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("WelcomeScene");
    }

    public void OnEasyBtn()
    {
        selectedDifferculty = 0;
        easyButton.GetComponent<Image>().color = color;
        hardButton.GetComponent<Image>().color = Color.white;

        UpdateLevel();
    }

    public void OnHardBtn()
    {
        selectedDifferculty = 1;
        hardButton.GetComponent<Image>().color = color;
        easyButton.GetComponent<Image>().color = Color.white;

        UpdateLevel();
    }

    private void UpdateLevel()
    {
        if (levelSelected == 1)
        {
            Level1();
        }
        else if (levelSelected == 2)
        {
            Level2();
        }
        else if (levelSelected == 3)
        {
            Level3();
        }
        else if (levelSelected == 0)
        {
            Total();
        }
    }
}
