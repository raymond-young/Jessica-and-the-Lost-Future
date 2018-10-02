using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class CutsceneController : Movement {

    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
    }

    // Changes the scene to a different level.
    [YarnCommand("move_down")]
    public void TutorialScene1(string destination)
    {
        Vector2 newPos = new Vector2(0, -5);
        DoLinearMove(newPos, 10);
    }
}
