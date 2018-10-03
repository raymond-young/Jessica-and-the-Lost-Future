using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTransferScript : MonoBehaviour {

    private string _score;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setScore(string score)
    {
        _score = score;
    }

    public string getScore()
    {
        return _score;
    }
}
