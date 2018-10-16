using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Achievement
{
	private string name;

	public string Name
	{
		get { return name; }
		set { name = value; }
	}

	private string description;

	public string Description
	{
		get { return description; }
		set { description = value; }
	}

	private bool unlocked;

	private int spriteIndex;

	public int SpriteIndex
	{
		get { return spriteIndex; }
		set { spriteIndex = value; }
	}

	private GameObject achievementRef;

	public Achievement(string name, string description, int spriteIndex, GameObject achievementRef) 
	{
		this.name = name;
		this.description = description;
		this.spriteIndex = spriteIndex;
		this.achievementRef = achievementRef;

		this.unlocked = false;
	}

	public bool EarnAchievement()
	{
		if(!unlocked) 
		{
			// Change the background to the unlocked image
			achievementRef.GetComponent<Image>().sprite = AchievementManager.Instance.unlockedSprite;

			achievementRef.transform.GetChild(3).GetComponent<Image>().sprite = AchievementManager.Instance.sprites[1];


			this.unlocked = true;
			return true;
		} 
		else 
		{
			return false;
		}
	}


}
