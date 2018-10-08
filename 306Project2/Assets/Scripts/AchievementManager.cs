using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour {

	public GameObject achievementPrefab;

	public Sprite[] sprites;

	// Use this for initialization
	void Start () 
	{
		CreateAchievement("General", "TestTitle","This is a description",0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void CreateAchievement(string category, string title, string description, int spriteIndex) 
	{
		GameObject achievement = (GameObject)Instantiate(achievementPrefab);

		SetAchievementInfo(category,achievement,title,description,spriteIndex);
	}

	public void SetAchievementInfo(string category, GameObject achievement, string title, string description, int spriteIndex)
	{
		achievement.transform.SetParent(GameObject.Find(category).transform);
		achievement.transform.localScale = new Vector3(1,1,1);
		
		// The ordering of the 'Achievement' children is important for this next part.
		
		// Get the title (child 0)
		achievement.transform.GetChild(0).GetComponent<Text>().text = title;

		// Get the description (child 1)
		achievement.transform.GetChild(1).GetComponent<Text>().text = description;

		// Get the description (child 3)
		achievement.transform.GetChild(3).GetComponent<Image>().sprite = sprites[spriteIndex];

	}
}
