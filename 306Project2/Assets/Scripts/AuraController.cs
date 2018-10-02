using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraController : MonoBehaviour {

    private MiniGameManager miniGameReset;

    private bool playingMiniGame = false;

	// Use this for initialization
	void Start () {
        miniGameReset = GameObject.FindGameObjectWithTag("MiniGameManager").GetComponent<MiniGameManager>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    //Triggers minigame
    private void TriggerMiniGame(Collider2D collision)
    {
        if (!playingMiniGame && collision.gameObject.tag.Equals("player"))
        {
            GameObject npc = gameObject.transform.parent.gameObject;

            miniGameReset.SetStartGame(true, npc);
            playingMiniGame = true;
        }

    }

    //Triggers minigame on start of collision
    private void OnTriggerEnter2D(Collider2D collision)
    {
        TriggerMiniGame(collision);
    }

    //Triggers minigame during collision in case start not detected
    private void OnTriggerStay2D(Collider2D collision)
    {
        TriggerMiniGame(collision);
    }

    //Reset the minigame
    public void FinishedMiniGame()
    {
        playingMiniGame = false;
        //StartCoroutine(WaitFewSeconds());
    }

    private IEnumerator WaitFewSeconds()
    {
        yield return new WaitForSeconds(2);
        playingMiniGame = false;
    }

}
