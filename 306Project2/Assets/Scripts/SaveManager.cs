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


    public void SaveLevel(float startOfLevelScore, int currentLevel, string playerName, int differculty)
    {
        string destination = Application.persistentDataPath + "/" + playerName + ".dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
        else file = File.Create(destination);

        SaveData data = new SaveData(startOfLevelScore, currentLevel, playerName, differculty);
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();
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
