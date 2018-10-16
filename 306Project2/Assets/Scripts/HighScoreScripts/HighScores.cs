using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

    private float startPosForFormat = 180f;
    private float heightInterval = 40f;

    private GameObject highScorePanel;

    //Initialize differculty to 0 for easy (default) at 1 for hard
    private int selectedDifferculty;

    private int levelSelected;

    private Font font;

    private int maxScoreNum = 7;

	// Use this for initialization
	void Start () {

        font = (Font) Resources.Load("Font/earthorbiter");

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

        //Test ONLY
        //saveManager.SaveLevel(10, 2, "e", 0);

        List<SaveData> saves = saveManager.LoadSave();

        List<SaveData> sortedSaves = SortLevels(saves);

        float format = startPosForFormat;

        int previousLevel = 0;
        //Loads players to appropriate level
        for (int i = 0; i < sortedSaves.Count; i++)
        {
            if (previousLevel != sortedSaves[i].GetLevel())
            {
                format = startPosForFormat;
            }

            previousLevel = sortedSaves[i].GetLevel();

            format = format - heightInterval;

            SaveData data = sortedSaves[i];

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


    //Reads files related to gamemode and level to show high scores
    private void ReadFile(int level, int gameMode, SaveData data, float format)
    {
        GameObject parentPanel = null;
        float score = 0;

        if (gameMode == 0)
        {
            if (level == 0)
            {
                score = data.TotalScore();
                parentPanel = totalPanelEasy;
            }
            else if (level == 1)
            {
                score = data.GetLevel1Score();
                parentPanel = level1PanelEasy;
            }
            else if (level == 2)
            {
                score = data.GetLevel2Score();
                parentPanel = level2PanelEasy;
            }
            else if (level == 3)
            {
                score = data.GetLevel3Score();
                parentPanel = level3PanelEasy;
            }
        }
        else if (gameMode == 1)
        {
            if (level == 0)
            {
                score = data.TotalScore();
                parentPanel = totalPanelHard;
            }
            else if (level == 1)
            {
                score = data.GetLevel1Score();
                parentPanel = level1PanelHard;
            }
            else if (level == 2)
            {
                score = data.GetLevel2Score();
                parentPanel = level2PanelHard;
            }
            else if (level == 3)
            {
                score = data.GetLevel3Score();
                parentPanel = level3PanelHard;
            }
        }

        GameObject p = Instantiate(playerListPrefab, parentPanel.transform);

        p.transform.localPosition = new Vector2(0f, format);
        p.GetComponent<Text>().text = data.GetPlayerName() + " " + score.ToString();
        p.GetComponent<Text>().font = font;
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

    private List<SaveData> SortLevels(List<SaveData> saveData)
    {

        List<SaveData> sortedData = new List<SaveData>();

        List<SaveData> level1Easy = saveData.Where(s => s.GetLevel() == 1 && s.GetDifferculty() == 0).ToList();
        List<SaveData> level2Easy = saveData.Where(s => s.GetLevel() == 2 && s.GetDifferculty() == 0).ToList();
        List<SaveData> level3Easy = saveData.Where(s => s.GetLevel() == 3 && s.GetDifferculty() == 0).ToList();
        
        List<SaveData> level1Hard = saveData.Where(s => s.GetLevel() == 1 && s.GetDifferculty() == 1).ToList();
        List<SaveData> level2Hard = saveData.Where(s => s.GetLevel() == 2 && s.GetDifferculty() == 1).ToList();
        List<SaveData> level3Hard = saveData.Where(s => s.GetLevel() == 3 && s.GetDifferculty() == 1).ToList();


        List<SaveData> tempEasy = saveData.Where(s => s.GetDifferculty() == 0).ToList();
        List<SaveData> tempHard = saveData.Where(s => s.GetDifferculty() == 1).ToList();
        List<SaveData> totalEasyTemp = tempEasy.OrderByDescending(s => s.GetTotalScore()).ToList();
        List<SaveData> totalHardTemp = tempHard.OrderByDescending(s => s.GetTotalScore()).ToList();

        level1Easy = level1Easy.OrderByDescending(s => s.GetLevel1Score()).ToList();
        level2Easy = level2Easy.OrderByDescending(s => s.GetLevel2Score()).ToList();
        level3Easy = level3Easy.OrderByDescending(s => s.GetLevel3Score()).ToList();

        level1Hard = level1Hard.OrderByDescending(s => s.GetLevel1Score()).ToList();
        level2Hard = level2Hard.OrderByDescending(s => s.GetLevel2Score()).ToList();
        level3Hard = level3Hard.OrderByDescending(s => s.GetLevel3Score()).ToList();


        List<SaveData> level1EasyData = new List<SaveData>();
        List<SaveData> level2EasyData = new List<SaveData>();
        List<SaveData> level3EasyData = new List<SaveData>();

        List<SaveData> level1HardData = new List<SaveData>();
        List<SaveData> level2HardData = new List<SaveData>();
        List<SaveData> level3HardData = new List<SaveData>();

        for (int i = 0; i < maxScoreNum; i++)
        {

            if (level1Easy.Count > i)
            {
                level1EasyData.Add(level1Easy[i]);
            }

            if (level2Easy.Count > i)
            {
                level2EasyData.Add(level2Easy[i]);
            }

            if (level3Easy.Count > i)
            {
                level3EasyData.Add(level3Easy[i]);
            }

            if (level1Hard.Count > i)
            {
                level1HardData.Add(level1Hard[i]);
            }

            if (level2Hard.Count > i)
            {
                level2HardData.Add(level2Hard[i]);
            }

            if (level3Hard.Count > i)
            {
                level3HardData.Add(level3Hard[i]);
            }

        }

        List<SaveData> totalEasy = new List<SaveData>();
        List<SaveData> totalHard = new List<SaveData>();

        for (int i = 0; i < maxScoreNum; i++)
        {
            if (totalEasyTemp.Count > i)
            {
                float score = totalEasyTemp[i].GetTotalScore();
                SaveData tempData = new SaveData(score, 0,
                    totalEasyTemp[i].GetPlayerName(), totalEasyTemp[i].GetDifferculty()); 
                totalEasy.Add(tempData);
            }

            if (totalHardTemp.Count > i)
            {
                float score = totalHardTemp[i].GetTotalScore();
                SaveData tempData = new SaveData(score, 0,
                    totalHardTemp[i].GetPlayerName(), totalHardTemp[i].GetDifferculty());
                totalHard.Add(tempData);
            }

        }

        sortedData.AddRange(level1EasyData);
        sortedData.AddRange(level2EasyData);
        sortedData.AddRange(level3EasyData);
        sortedData.AddRange(level1HardData);
        sortedData.AddRange(level2HardData);
        sortedData.AddRange(level3HardData);
        sortedData.AddRange(totalEasy);
        sortedData.AddRange(totalHard);
 

        return sortedData;
    }
}
