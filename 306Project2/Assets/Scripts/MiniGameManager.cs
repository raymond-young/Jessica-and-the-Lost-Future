using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour {

    //Reference list of enemy NPCs
    private List<GameObject> npcs = new List<GameObject>();
    private PlayerController player;

    private bool startGame = false;
    private bool hasStarted = false;

    private GameObject currentNpc;

    private GameObject miniGame;

    // Use this for initialization
    void Start () {

        //Initialises reference to other NPC objects
        GameObject npcGameObject = GameObject.FindGameObjectWithTag("NPCs");
        for (int i = 0; i < npcGameObject.transform.childCount; i++)
        {
            npcs.Add(npcGameObject.transform.GetChild(i).gameObject);
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
            miniGame = Instantiate(Resources.Load("Minigame", typeof(GameObject))) as GameObject;
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
        controller.FinishedMiniGame();

        if (!success)
        {
            //Failed minigame so loose power
            player.LooseOnePower();
        }
        else
        { 
            //Successful minigame so destroy aura
            Destroy(currentNpc.transform.GetChild(0).gameObject);
        }

    }


}
