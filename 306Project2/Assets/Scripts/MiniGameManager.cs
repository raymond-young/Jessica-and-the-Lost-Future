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
        GameObject[] npcGameObject = GameObject.FindGameObjectsWithTag("BadNPC");
        for (int i = 0; i < npcGameObject[i].transform.childCount; i++)
        {
            npcs.Add(npcGameObject[i].transform.GetChild(i).gameObject);
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
            player.LoseOnePower();

            //Calculates the opposite direction to the npc from the players input
            Vector2 direction = (player.transform.position - currentNpc.transform.position).normalized;

            //Used to detect if player stuck between another object and the bad npc
            RaycastHit2D hit = Physics2D.Raycast(player.transform.position, direction * 5f);

         
            if (hit.collider != null)
            {
                //If stuck, teleport player to their last prevoius recorded position 
                player.transform.position = player.GetPreviousPos();
            }
            else
            {
                //If not stuck, teleport player in opposite direction to the bad npc
                player.transform.Translate(direction * 5f);
            }

                


        }
        else
        { 
            //Successful minigame so destroy aura
            Destroy(currentNpc.transform.GetChild(0).gameObject);
        }

    }


}
