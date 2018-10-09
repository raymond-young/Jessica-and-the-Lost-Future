using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementManager : MonoBehaviour {

	public GameObject achievementPrefab;

	public Sprite[] sprites;

	public GameObject visualAchievement;

	// Dictionary's are basically Maps
	public Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();



	// Used to toggle showing the achievements screen
	public bool showAchievementScreen;
	public GameObject achievementMenu;


	public Sprite unlockedSprite;

	public static AchievementManager instance;

	public static AchievementManager Instance
	{
		get { 
			if(instance == null)
			{
				instance = GameObject.FindObjectOfType<AchievementManager>();
			}	

			return AchievementManager.instance; }
	}

	// Use this for initialization
	void Start () 
	{
		// Test Achivevement
		CreateAchievement("GeneralAchievement", "Press W","Press W to unlock", 0);


	}
	
	bool startUp = true;
	// Update is called once per frame
	void Update () {

		// Hide the achievement screen on startup.
		if (startUp) {
			showAchievementScreen = false;
			achievementMenu.SetActive(showAchievementScreen);
			startUp = false;
		}

		// Display the achievements if you press the 'i' key.
		if (Input.GetKeyDown(KeyCode.I))
		{
			showAchievementScreen = !showAchievementScreen;
			achievementMenu.SetActive(showAchievementScreen);
		}
		
		// Test achievement
		if (Input.GetKeyDown(KeyCode.W))
		{
			EarnAchievement("Press W");
		}
	}

	public void EarnAchievement(string title)
	{
		if(achievements[title].EarnAchievement())
		{
			// Earn the achievement if it's currently not earned.
			GameObject achievement = (GameObject)Instantiate(visualAchievement);
			SetAchievementInfo("EarnAchivementCanvas", achievement, title);

			StartCoroutine(HideAchievement(achievement));

		}
	}

	public IEnumerator HideAchievement(GameObject achievement)
	{
		yield return new WaitForSeconds(3);
		Destroy(achievement);
	}

	public void CreateAchievement(string parent, string title, string description, int spriteIndex) 
	{
		// Instantiate a new achivement from the prefab.
		GameObject achievement = (GameObject)Instantiate(achievementPrefab);

		// Create a new Achievement Object and add it to the Achievement Map.
		Achievement newAchievement = new Achievement(name, description, 10, spriteIndex, achievement);
		achievements.Add(title, newAchievement);

		// Set the achievement info according to it's title and the new instace you've made.
		SetAchievementInfo(parent, achievement, title);
	}

	public void SetAchievementInfo(string parent, GameObject achievement, string title)
	{
		
		// Make the achievement the child of the "GeneralAchievement" object (used to categorise achievements.)
		Debug.Log("Parent is: " + parent);
		achievement.transform.SetParent(GameObject.FindGameObjectWithTag(parent).transform);
		achievement.transform.localScale = new Vector3(1,1,1);
		
		// The ordering of the 'Achievement' children is important for this next part.
		
		// Get the title (child 0)
		achievement.transform.GetChild(0).GetComponent<Text>().text = title;

		// Get the description (child 1)
		achievement.transform.GetChild(1).GetComponent<Text>().text = achievements[title].Description;

		// Get the sprite (child 3)
		achievement.transform.GetChild(3).GetComponent<Image>().sprite = sprites[achievements[title].SpriteIndex];

	}
}
