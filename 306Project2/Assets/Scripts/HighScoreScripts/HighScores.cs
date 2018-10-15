using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScores : MonoBehaviour {

    public GameObject parentPanel;
    public GameObject level1Panel;
    public GameObject level2Panel;
    public GameObject level3Panel;

    public GameObject easyButton;
    public GameObject hardButton;

    public GameObject playerListPrefab;

    private Color color = new Color(0.9433962f, 0.511748f, 0.511748f);
    private Color levelColor = new Color(0.1135636f, 0.5811083f, 0.8301887f);

    private float heightInterval = 30;

    private GameObject highScorePanel;

    //Initialize gamemode to 0 for easy (default) at 1 for hard
    private int gameMode = 0;

    private List<GameObject> currentPlayers = new List<GameObject>();

	// Use this for initialization
	void Start () {
        //Initalize reading from file here into (highScorePlayers)

        level1Panel.SetActive(false);
        level2Panel.SetActive(false);
        level3Panel.SetActive(false);

        //Loads players to appropriate level
        ReadFile(0);

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

    private void ReadFile(int level)
    {

        if (level == 0)
        {
            GameObject p = Instantiate(playerListPrefab, parentPanel.transform);
            p.transform.localPosition = new Vector2(0f, 160f);
            p.GetComponent<Text>().text = "yianni 50";
            p.GetComponent<Text>().fontSize = 40;
            p.name = "yianni";

            //Read file code here
            currentPlayers.Add(p);
        }
        else if (level == 1)
        {

        }
        else if (level == 2)
        {

        }
        else if (level == 3)
        {

        }

    }

    public void Level1()
    {
        RefreshPlayers("yianni", 200);

        //Toggle panels
        level1Panel.SetActive(true);
        level2Panel.SetActive(false);
        level3Panel.SetActive(false);
        parentPanel.SetActive(false);

        //Color change code
        for (int i = 0; i < highScorePanel.transform.childCount; i++)
        {
            highScorePanel.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
        highScorePanel.transform.GetChild(0).GetComponent<Image>().color = levelColor;

    }

    public void Level2()
    {
        //Toggle panels
        level1Panel.SetActive(false);
        level2Panel.SetActive(true);
        level3Panel.SetActive(false);
        parentPanel.SetActive(false);

        //Color change code
        for (int i = 0; i < highScorePanel.transform.childCount; i++)
        {
            highScorePanel.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
        highScorePanel.transform.GetChild(1).GetComponent<Image>().color = levelColor;
    }

    public void Level3()
    {
        //Toggle panels
        level1Panel.SetActive(false);
        level2Panel.SetActive(false);
        level3Panel.SetActive(true);
        parentPanel.SetActive(false);


        //Color change code
        for (int i = 0; i < highScorePanel.transform.childCount; i++)
        {
            highScorePanel.transform.GetChild(i).GetComponent<Image>().color = Color.white;
        }
        highScorePanel.transform.GetChild(2).GetComponent<Image>().color = levelColor;
    }

    public void Total()
    {
        //Toggle panels
        level1Panel.SetActive(false);
        level2Panel.SetActive(false);
        level3Panel.SetActive(false);
        parentPanel.SetActive(true);

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
        easyButton.GetComponent<Image>().color = color;
        hardButton.GetComponent<Image>().color = Color.white;
    }

    public void OnHardBtn()
    {
        hardButton.GetComponent<Image>().color = color;
        easyButton.GetComponent<Image>().color = Color.white;
    }
}
