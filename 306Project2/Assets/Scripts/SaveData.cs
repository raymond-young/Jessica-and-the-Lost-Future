using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {

    private int level;
    private string playerName;

    private float level1SaveScore;
    private float level2SaveScore;
    private float level3SaveScore;
    private float totalScore;
    public Dictionary<string, bool> levels = new Dictionary<string, bool>();

    public SaveData(float score, int level, string playerName)
    {
        this.level = level;

        if (level == 0)
        {
            totalScore = score;
        }
        else if (level == 1)
        {
            level1SaveScore = score;
        }
        else if (level == 2)
        {
            level2SaveScore = score;
        }
        else if (level == 3)
        {
            level3SaveScore = score;
        }

        this.playerName = playerName;

        levels.Add("level2", false);
        levels.Add("level3", false);
    }

    public float TotalScore()
    {
        return totalScore;
    }

    public float GetTotalScore()
    {
        float scoreTemp = level1SaveScore + level2SaveScore + level3SaveScore;
        return scoreTemp;
    }

    public float GetLevel1Score()
    {
        return level1SaveScore;
    }

    public float GetLevel2Score()
    {
        return level2SaveScore;
    }

    public float GetLevel3Score()
    {
        return level3SaveScore;
    }

    public int GetLevel()
    {
        return level;
    }

    public string GetPlayerName()
    {
        return playerName;
    }


    public void SetTotalScore(float score)
    {
        totalScore = score;
    }

    public void SetLevel1Score(float score)
    {
        level1SaveScore = score;
    }

    public void SetLevel2Score(float score)
    {
        level2SaveScore = score;
    }

    public void SetLevel3Score(float score)
    {
        level3SaveScore = score;
    }


}
