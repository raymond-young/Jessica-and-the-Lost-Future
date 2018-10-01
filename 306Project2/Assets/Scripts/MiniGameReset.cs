using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameReset : MonoBehaviour {

    private List<EnemyNPC> npcs = new List<EnemyNPC>();
    private PlayerController player;
    private AuraController aura;
    private Canvas miniGameCanvas;

    private bool startGame = false;

	// Use this for initialization
	void Start () {

        GameObject npcGameObject = GameObject.FindGameObjectWithTag("NPCs");

        for (int i = 0; i < npcGameObject.transform.childCount; i++)
        {
            npcs.Add(npcGameObject.transform.GetChild(i).gameObject.GetComponent<EnemyNPC>());
        }

        GameObject p = GameObject.FindGameObjectWithTag("player");
        player = p.GetComponent<PlayerController>();

        miniGameCanvas = GameObject.FindGameObjectWithTag("MiniGameCanvas").GetComponent<Canvas>();
        miniGameCanvas.enabled = false;
    }
	
	// Update is called once per frame
	void Update () {
		
        if (startGame)
        {
            for (int i = 0; i < npcs.Count; i++)
            {
                npcs[i].enabled = false;
            }
            player.SetMotionToZero();
            player.enabled = false;
            miniGameCanvas.enabled = true;
        }
	}

    public void SetStartGame(bool startGame)
    {
        this.startGame = startGame;
    }
    
    public void FinishGame(bool finish)
    {

    }


}
