using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuraController : MonoBehaviour {

    private MiniGameReset miniGameReset;

	// Use this for initialization
	void Start () {
        miniGameReset = GameObject.FindGameObjectWithTag("MiniGameReset").GetComponent<MiniGameReset>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        miniGameReset.SetStartGame(true);
    }
}
