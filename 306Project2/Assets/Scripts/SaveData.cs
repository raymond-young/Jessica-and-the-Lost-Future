using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData {

    private float score;
    private int level;
    private string playerName;
    private int differculty;

    public SaveData(float score, int level, string playerName, int differculty)
    {
        this.score = score;
        this.level = level;
        this.differculty = differculty;
        this.playerName = playerName;
    }

    public float GetScore()
    {
        return score;
    }

    public int GetLevel()
    {
        return level;
    }

    public string GetPlayerName()
    {
        return playerName;
    }

    public int GetDifferculty()
    {
        return differculty;
    }
}
