using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour {

    private List<GameObject> npcs = new List<GameObject>();
    private PlayerController player;

    private bool startGame = false;
    private bool hasStarted = false;

    private GameObject currentNpc;

    private GameObject miniGame;

    // Use this for initialization
    void Start () {

        GameObject npcGameObject = GameObject.FindGameObjectWithTag("NPCs");

        for (int i = 0; i < npcGameObject.transform.childCount; i++)
        {
            npcs.Add(npcGameObject.transform.GetChild(i).gameObject);
        }

        GameObject p = GameObject.FindGameObjectWithTag("player");
        player = p.GetComponent<PlayerController>();

    }
	
	// Update is called once per frame
	void Update () {
		
        if (startGame && !hasStarted)
        {
            for (int i = 0; i < npcs.Count; i++)
            {
                Rigidbody2D rigidBody = npcs[i].GetComponent<Rigidbody2D>();
                rigidBody.simulated = false;
                npcs[i].GetComponent<EnemyNPC>().enabled = false;
            }
            player.SetMotionToZero();
            player.enabled = false;

            miniGame = Instantiate(Resources.Load("Minigame", typeof(GameObject))) as GameObject;
            hasStarted = true;
        }

	}
    

    public void SetStartGame(bool startGame, GameObject npc)
    {
        this.startGame = startGame;
        currentNpc = npc;
    }
    
    public void FinishGame(bool success)
    {

        Destroy(miniGame);
        startGame = false;
        hasStarted = false;

        for (int i = 0; i < npcs.Count; i++)
        {
            Rigidbody2D rigidBody = npcs[i].GetComponent<Rigidbody2D>();
            rigidBody.simulated = true;

            npcs[i].GetComponent<EnemyNPC>().enabled = true;
        }
        player.enabled = true;

        AuraController controller = currentNpc.transform.GetChild(0).GetComponent<AuraController>();
        controller.FinishedMiniGame();

        if (!success)
        {
            player.LooseOnePower();
        }
        else
        { 
            Destroy(currentNpc.transform.GetChild(0).gameObject);
        }

    }


}
