using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreTransferScript : MonoBehaviour {

    private float _score;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void setScore(float score)
    {
        _score = score;
    }

    public float getScore()
    {
        return _score;
    }
}
