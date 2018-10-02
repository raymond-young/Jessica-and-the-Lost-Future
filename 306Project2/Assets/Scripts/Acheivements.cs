using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
public class Acheivements : MonoBehaviour {

	//the achievements data object class. the data is in a seperate class for serialization purposes
	private AchievementsDO _achievementsDO;
	// Use this for initialization
	void Start () {
		if(File.Exists(Application.persistentDataPath +"/playerAchievements.dat")){
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/playerAchievements.dat", FileMode.Open);
			_achievementsDO = (AchievementsDO) bf.Deserialize(file);
			file.Close();
		}
	}

	void OnDestroy(){
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Open(Application.persistentDataPath + "/playerAChievements.dat", FileMode.Create);
		bf.Serialize(file,_achievementsDO);
		file.Close();
	}

	void handleArrowMinigame(){

	}

	void handleDoor(){
		_achievementsDO.doors++;
		if(_achievementsDO.doors == 1){
			_achievementsDO.ENTER_ONE_ROOM = true;
		}
		if(_achievementsDO.doors == 10){
			_achievementsDO.ENTER_TEN_ROOMS = true;
		}
		if(_achievementsDO.doors == 100){
			_achievementsDO.ENTER_HUNDRED_ROOMS = true;
		}
	}

	void finishLevel(){

	}

[Serializable]
private class AchievementsDO {
	public int doors = 0;

	//complete level one
	public bool MEET_MENTOR = false;

	//complete level one having not failed any minigames
	public bool COMPLETE_LEVEL_ONE_MAX_LIVES = false;

	//meet mentor with maximum confidence
	public bool COMPLETE_LEVEL_ONE_MAX_CONFIDENCE = false;

	//All rooms in level one discovered
	public bool EXPLORE_LEVEL_ONE = false;

	//game runs LOL
	public bool START_GAME = false;

	//pass through 1 door
	public bool ENTER_ONE_ROOM = false;
	
	//pass through 10 doors
	public bool ENTER_TEN_ROOMS = false;

	//pass through 100 doors
	public bool ENTER_HUNDRED_ROOMS = false;

	//pick up an item
	public bool PICKED_UP_FIRST_ITEM = false;

	//talk to a npc
	public bool TALKED_TO_FIRST_NPC = false;

	//clear the arrow minigame ONCE
	public bool KEYBOARD_WARRIOR = false;

	//clear the arrow minigame with less than 1 second to spare
	public bool JUST_IN_TIME = false;
}
}
