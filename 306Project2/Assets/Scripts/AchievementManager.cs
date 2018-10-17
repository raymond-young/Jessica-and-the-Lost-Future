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
	private bool showAchievementScreen;
	public GameObject achievementMenu;

	public GameObject player;

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
		CreateAchievement("GeneralAchievement", "Item Grabber","Pick up an item", 0);
		CreateAchievement("GeneralAchievement", "First Timer","Play the minigame for the first time.", 0);
		CreateAchievement("GeneralAchievement", "Keyboard Warrior","Play the minigame without losing a life.", 0);
		CreateAchievement("GeneralAchievement", "Conversation Starter","Talk to 5 NPCs.", 0);
		CreateAchievement("GeneralAchievement", "Maxed Out", "Fill up the confidence bar.", 0);

		// Test Achivevements
		CreateAchievement("GeneralAchievement", "Pay respects","Press F to unlock", 0);
		// CreateAchievement("GeneralAchievement", "Press W","Press W to unlock", 0);
		// CreateAchievement("GeneralAchievement", "Press A","Press A to unlock", 0);
		// CreateAchievement("GeneralAchievement", "Press S","Press S to unlock", 0);
		// CreateAchievement("GeneralAchievement", "Press D","Press D to unlock", 0);
		// CreateAchievement("GeneralAchievement", "Glow","Walk past an item to unlock", 0);


		
	}
	
	bool startUp = true;
	// Update is called once per frame
	void Update () {

		if (player == null){
			player = GameObject.FindGameObjectWithTag("player");
		}
		if (startUp) {
			Debug.Log ("achievementMenu is false!");
			achievementMenu = GameObject.FindGameObjectWithTag("AchievementMenu");
			achievementMenu.transform.SetParent(GameObject.Find("Canvas").transform);

			Debug.Log("Adding achivements to menu after new level");
			// Put the achievements in the menu object
			foreach(KeyValuePair<string,Achievement> entry in achievements) {
				SetAchievementInfo("GeneralAchievement", entry.Value.getAchieveObject(), entry.Key);
			}

		}

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

			// Pause
			if (showAchievementScreen) 
			{
				Time.timeScale = 0.00001f;
			} else {
				Time.timeScale = 1f;
			}
		}
		
		// // Test achievement
		// if (Input.GetKeyDown(KeyCode.W))
		// {
		// 	EarnAchievement("Press W");
		// }

		// // Test achievement
		// if (Input.GetKeyDown(KeyCode.A))
		// {
		// 	EarnAchievement("Press A");
		// }

		// // Test achievement
		// if (Input.GetKeyDown(KeyCode.S))
		// {
		// 	EarnAchievement("Press S");
		// }

		// // Test achievement
		// if (Input.GetKeyDown(KeyCode.D))
		// {
		// 	EarnAchievement("Press D");
		// }

		// Test achievement
		if (Input.GetKeyDown(KeyCode.F))
		{
			EarnAchievement("Pay respects");
		}


	}

	public void EarnAchievement(string title)
	{
		if(achievements[title].EarnAchievement())
		{
			// Earn the achievement if it's currently not earned.
			GameObject achievement = (GameObject)Instantiate(visualAchievement);
			SetAchievementInfo("EarnAchievementCanvas", achievement, title);

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
		Achievement newAchievement = new Achievement(name, description, spriteIndex, achievement);
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

		// Get the sprite (child 2)
		achievement.transform.GetChild(2).GetComponent<Image>().sprite = sprites[achievements[title].SpriteIndex];
	}

	public void SetStartup(bool start) {
		startUp = start;
	}

	
	// 	//complete level one
	// public bool MEET_MENTOR = false;

	// //complete level one having not failed any minigames
	// public bool COMPLETE_LEVEL_ONE_MAX_LIVES = false;

	// //meet mentor with maximum confidence
	// public bool COMPLETE_LEVEL_ONE_MAX_CONFIDENCE = false;

	// //All rooms in level one discovered
	// public bool EXPLORE_LEVEL_ONE = false;

	// //pick up an item
	// public bool PICKED_UP_FIRST_ITEM = false;

	// //talk to a npc
	// public bool TALKED_TO_FIRST_NPC = false;

	// //clear the arrow minigame ONCE
	// public bool KEYBOARD_WARRIOR = false;

	// //clear the arrow minigame with less than 1 second to spare
	// public bool JUST_IN_TIME = false;
	
	
}
