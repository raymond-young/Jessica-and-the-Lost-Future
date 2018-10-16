using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Backet : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collider)
    {
        GameObject.FindGameObjectWithTag("MinigameFallingBall").GetComponent<FallingBall>().CatchBall(collider.gameObject);
    }
}
