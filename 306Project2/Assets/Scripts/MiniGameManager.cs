using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour {

    //Reference list of enemy NPCs
    private GameObject canvas;
    private List<GameObject> npcs = new List<GameObject>();
    private PlayerController player;

    public AchievementManager achievementManager;

    private bool startGame = false;
    private bool hasStarted = false;

    private GameObject currentNpc;

    private GameObject miniGame;

    // Use this for initialization
    void Start () {
        canvas = GameObject.Find("Canvas");


        //Initialises reference to other NPC objects
        GameObject[] npcGameObject = GameObject.FindGameObjectsWithTag("BadNPC");
        achievementManager = GameObject.FindObjectOfType<AchievementManager>();

        for (int i = 0; i < npcGameObject.Length; i++)
        {
            npcs.Add(npcGameObject[i]);
        }

        //Initialises reference to player object
        GameObject p = GameObject.FindGameObjectWithTag("player");
        player = p.GetComponent<PlayerController>();

    }
	
	// Update is called once per frame
	void Update () {
		
        //On collision with aura, start minigame
        if (startGame && !hasStarted)
        {
            //Stop npcs moving
            for (int i = 0; i < npcs.Count; i++)
            {
                Rigidbody2D rigidBody = npcs[i].GetComponent<Rigidbody2D>();
                rigidBody.simulated = false;
                npcs[i].GetComponent<EnemyNPC>().enabled = false;
            }

            //Stop player moving
            player.SetMotionToZero();
            player.enabled = false;

            //Start minigame
            miniGame = Instantiate(Resources.Load("MGame", typeof(GameObject))) as GameObject;

            miniGame.transform.SetParent(canvas.transform);

            //Ensures mini game is positioned correctly on canvas
            miniGame.GetComponent<RectTransform>().position = Vector2.zero;
            miniGame.GetComponent<RectTransform>().localPosition = Vector2.zero;
            miniGame.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            
            hasStarted = true;
        }

	}
    
    //Starts minigame from aura collision
    public void SetStartGame(bool startGame, GameObject npc)
    {
        this.startGame = startGame;
        currentNpc = npc;
    }
    
    //Finish minigame (called by minigame generator on finish)
    public void FinishGame(bool success)
    {
        //Destroy the minigame
        Destroy(miniGame);
        startGame = false;
        hasStarted = false;

        //Restart npcs
        for (int i = 0; i < npcs.Count; i++)
        {
            Rigidbody2D rigidBody = npcs[i].GetComponent<Rigidbody2D>();
            rigidBody.simulated = true;

            npcs[i].GetComponent<EnemyNPC>().enabled = true;
        }

        //Restart player
        player.enabled = true;

        //Set delay for aura
        AuraController controller = currentNpc.transform.GetChild(0).GetComponent<AuraController>();
        controller.FinishedMiniGame(success);

        if (!success)
        {
            //Failed minigame so loose power
            player.LoseOnePower();

            //If stuck, teleport player to their last prevoius recorded position 
            player.transform.position = player.GetPreviousPos();

        }
        else
        { 
            Debug.Log("Finished minigame");
            //Successful minigame so destroy aura
            Destroy(currentNpc.transform.GetChild(0).gameObject);
            
            achievementManager.EarnAchievement("Keyboard Warrior");
        }

    }


}
