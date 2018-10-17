using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Yarn.Unity;

public class GoodNPC : Movement {
    public float xPos;
    public float yPos;

    private Vector2 originalPos;

    public float speed = 1f;

    public string characterName = "";

    [FormerlySerializedAs("startNode")]
    public string talkToNode = "";

    [Header("Optional")]
    public TextAsset scriptToLoad;

    public bool isMovingCharacter = false;

    // Use this for initialization
    protected override void Start () {
        if (scriptToLoad != null) {
            FindObjectOfType<DialogueRunner>().AddScript(scriptToLoad);
        }

        originalPos = gameObject.transform.position;
        base.Start();
    }

    // Update is called once per frame

    protected override void FixedUpdate()
    {
        if (isMovingCharacter)
        {
            if (!wait)
            {
                Vector2 newPos;
                if (movingTowards)
                {
                    newPos = new Vector2(xPos, yPos);
                }
                else
                {
                    newPos = originalPos;
                }
                MoveToPos(originalPos, newPos);
            }
        }
    }

    protected override void customFixedUpdate () {
       if (!wait)
        {
            Vector2 newPos;
            if (movingTowards)
            {
                newPos = new Vector2(xPos, yPos);
            }
            else
            {
                newPos = originalPos;
            }
        }
    }

    // Moves the NPC to a position.
    private void MoveToPos(Vector2 currentPos, Vector2 newPos) {
        if (isMovingCharacter) {
            StartCoroutine(DoLinearMove(newPos, speed));
            wait = true;
        }
    }

    // Changes the scene to a different level.
    [YarnCommand("move_down")]
    public void TutorialScene1(string destination)
    {
        Debug.Log("routine called");
        Vector2 newPos = new Vector2(-10, 0);
        MoveToPos(originalPos, newPos);
        
    }

    [YarnCommand("moveSophia")]
    public void MoveSophia(string destination)
    {
        Debug.Log("move sophia called");
        Vector3 move = new Vector3(gameObject.transform.localPosition.x, -0f, 0);
        gameObject.transform.localPosition = move;
    }

    // Hides event zone when no longer needed
    [YarnCommand("hide")]
    public void Hide(string destination)
    {
        GameObject.Find(destination).SetActive(false);
    }

}
