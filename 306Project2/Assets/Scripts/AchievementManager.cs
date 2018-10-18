using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


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

	public bool Lock; // True if you want to prevent the achievements menu from turning off.

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

		CreateAchievement("GeneralAchievement", "Back to the Past", "Finish the tutorial", 0);
		CreateAchievement("GeneralAchievement", "Teacher's Pet", "Finish Level 1", 0);
		CreateAchievement("GeneralAchievement", "School Ace", "Finish Level 1 without losing any lives.", 0);
		CreateAchievement("GeneralAchievement", "Graduation Nation", "Finish Level 2", 0);
		CreateAchievement("GeneralAchievement", "Completionist", "Finish the game!", 0);


		// Test Achivevements
		CreateAchievement("GeneralAchievement", "Paying respects","Press F to unlock", 0);
		// CreateAchievement("GeneralAchievement", "Glow","Walk past an item to unlock", 0);
	}
	
	bool startUp = true;
	// Update is called once per frame
	void Update () {

		if (player == null){
			player = GameObject.FindGameObjectWithTag("player");
		}
		if (startUp) {
            if (achievementMenu == null) {
                achievementMenu = GameObject.FindGameObjectWithTag("AchievementMenu");
                achievementMenu.transform.SetParent(GameObject.Find("Canvas").transform);                
            }

			// Put the achievements in the menu object
			foreach(KeyValuePair<string,Achievement> entry in achievements) {
				SetAchievementInfo("GeneralAchievement", entry.Value.getAchieveObject(), entry.Key);
			}

		}

		// Hide the achievement screen on startup.
		if (startUp && !Lock) {
			showAchievementScreen = false;
			achievementMenu.SetActive(showAchievementScreen);
			startUp = false;
		}

		// Display the achievements if you press the 'i' key.
		if (Input.GetKeyDown(KeyCode.I) && !Lock)
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

 	public void BackToMenu()
    {
        SceneManager.LoadScene("WelcomeScene");
    }	
	
}
