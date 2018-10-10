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

	// Use this for initialization
	void Start () {
        //Initalize reading from file here into (highScorePlayers)

        level1Panel.SetActive(false);
        level2Panel.SetActive(false);
        level3Panel.SetActive(false);

        //Add players to list here dependent on file contents
        GameObject p = Instantiate(playerListPrefab, parentPanel.transform);
        p.transform.localPosition = new Vector2(0f, 105f);
        p.GetComponent<Text>().text = "hello";
       
    }

    // Update is called once per frame
    void Update () {
        

    }

    public void Level1()
    {
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
