using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SaveManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //Saves at the end of a level
    public void SaveLevel(float startOfLevelScore, int currentLevel, string playerName)
    {
        //Checks if existing level exists
        SaveData save = CheckScoreSingleSave(startOfLevelScore, currentLevel, playerName);

        string destination = Application.persistentDataPath + "/" + playerName + ".dat";
        FileStream file;

        file = File.OpenWrite(destination);
        SaveData data = new SaveData(startOfLevelScore, currentLevel, playerName);

        //Checks if current attempt score better than previous attempt, keep the best score, do this for all levels
        if (data.GetLevel1Score() > save.GetLevel1Score())
        {
            data.SetLevel1Score(data.GetLevel1Score());
        }
        else
        {
            data.SetLevel1Score(save.GetLevel1Score());
        }
        

        if (data.GetLevel2Score() > save.GetLevel2Score())
        {
            data.SetLevel2Score(data.GetLevel2Score());
        }
        else
        {
            data.SetLevel2Score(save.GetLevel2Score());
        }


        if (data.GetLevel3Score() > save.GetLevel3Score())
        {
            data.SetLevel3Score(data.GetLevel3Score());
        }
        else
        {
            data.SetLevel3Score(save.GetLevel3Score());
        }
            
        
        //For saving level state
        switch (currentLevel)
        {
            case 1:
                data.levels["level2"] = true;
                break;
            case 2:
                data.levels["level2"] = true;
                data.levels["level3"] = true;
                break;
        }
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
        


    }

    //Checks if already file exists i.e. player already played a game
    private SaveData CheckScoreSingleSave(float startOfLevelScore, int currentLevel, string playerName)
    {
        string[] fileName = Directory.GetFiles(Application.persistentDataPath, playerName + ".dat");

        if (fileName.Length > 0)
        {
            //Loads previous attempt
            FileStream file;
            file = File.OpenRead(fileName[0]);
            BinaryFormatter bf = new BinaryFormatter();
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            return data;
      
        }
        else
        {
            //Return score of current game if prevoius doesnt exist
            return new SaveData(startOfLevelScore, currentLevel, playerName);
        }
        

    }

    //Loads all saved games for display on high score screen
    public List<SaveData> LoadSave()
    {
        List<SaveData> saves = new List<SaveData>();

        // Loop through all applications
        foreach (string fileName in Directory.GetFiles(Application.persistentDataPath, "*.dat"))
        {
            FileStream file;
            file = File.OpenRead(fileName);
            BinaryFormatter bf = new BinaryFormatter();
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            saves.Add(data);
        }

        return saves;
    }


}

