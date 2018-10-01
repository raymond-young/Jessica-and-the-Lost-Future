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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!playingMiniGame)
        {
            GameObject npc = gameObject.transform.parent.gameObject;

            miniGameReset.SetStartGame(true, npc);
            playingMiniGame = true;
        }

    }

    public void FinishedMiniGame()
    {
        StartCoroutine(WaitFewSeconds());
    }

    private IEnumerator WaitFewSeconds()
    {
        Debug.Log("here");
        yield return new WaitForSeconds(2);
        Debug.Log("After");
        playingMiniGame = false;
    }

}
