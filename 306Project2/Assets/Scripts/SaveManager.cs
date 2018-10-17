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


    public void SaveLevel(float startOfLevelScore, int currentLevel, string playerName)
    {

        SaveData save = CheckScoreSingleSave(startOfLevelScore, currentLevel, playerName);

        string destination = Application.persistentDataPath + "/" + playerName + ".dat";
        FileStream file;

        file = File.OpenWrite(destination);
        SaveData data = new SaveData(startOfLevelScore, currentLevel, playerName);

     
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
            
        

        switch (currentLevel)
        {
            case 1:
                data.levels["level2"] = true;
                break;
            case 2:
                data.levels["level3"] = true;
                break;
        }
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
        


    }

    private SaveData CheckScoreSingleSave(float startOfLevelScore, int currentLevel, string playerName)
    {
        string[] fileName = Directory.GetFiles(Application.persistentDataPath, playerName + ".dat");

        if (fileName.Length > 0)
        {
            FileStream file;
            file = File.OpenRead(fileName[0]);
            BinaryFormatter bf = new BinaryFormatter();
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            float score = 0;
            
            if (currentLevel == 0)
            {
                score = data.TotalScore();
            }
            else if (currentLevel == 1)
            {
                score = data.GetLevel1Score();
            }
            else if (currentLevel == 2)
            {
                score = data.GetLevel2Score();
            }
            else if (currentLevel == 3)
            {
                score = data.GetLevel3Score();
            }

            return data;
      
        }
        else
        {
            return new SaveData(startOfLevelScore, currentLevel, playerName);
        }
        

    }

    public List<SaveData> LoadSave()
    {
        List<SaveData> saves = new List<SaveData>();

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

