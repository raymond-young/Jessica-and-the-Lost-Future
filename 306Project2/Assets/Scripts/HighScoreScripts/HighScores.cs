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

    public GameObject playerListPrefab;

    private float heightInterval = 30;

    private List<GameObject> currentPlayers = new List<GameObject>();

	// Use this for initialization
	void Start () {
        //Initalize reading from file here into (highScorePlayers)

        level1Panel.SetActive(false);
        level2Panel.SetActive(false);
        level3Panel.SetActive(false);

        //Loads players to appropriate level
        ReadFile(0);
       
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
            p.transform.localPosition = new Vector2(0f, 105f);
            p.GetComponent<Text>().text = "yianni 50";
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
        level1Panel.SetActive(true);
        level2Panel.SetActive(false);
        level3Panel.SetActive(false);
        parentPanel.SetActive(false);
    }

    public void Level2()
    {
        level1Panel.SetActive(false);
        level2Panel.SetActive(true);
        level3Panel.SetActive(false);
        parentPanel.SetActive(false);
    }

    public void Level3()
    {
        level1Panel.SetActive(false);
        level2Panel.SetActive(false);
        level3Panel.SetActive(true);
        parentPanel.SetActive(false);
    }

    public void Total()
    {
        level1Panel.SetActive(false);
        level2Panel.SetActive(false);
        level3Panel.SetActive(false);
        parentPanel.SetActive(true);
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("WelcomeScene");
    }
}
