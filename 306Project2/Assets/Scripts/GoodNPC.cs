using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Yarn.Unity;

public class GoodNPC : Movement
{
    public float xPos;
    public float yPos;

    private Vector2 origonalPos;

    public float speed = 1f;

    public string characterName = "";

    [FormerlySerializedAs("startNode")]
    public string talkToNode = "";

    [Header("Optional")]
    public TextAsset scriptToLoad;

    // Use this for initialization
    protected override void Start () {
        if (scriptToLoad != null)
        {
            FindObjectOfType<DialogueRunner>().AddScript(scriptToLoad);
        }

        origonalPos = gameObject.transform.position;
        base.Start();
    }

    // Update is called once per frame
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
                newPos = origonalPos;
            }
            MoveToPos(origonalPos, newPos);
        }
    }

    private void MoveToPos(Vector2 currentPos, Vector2 newPos)
    {

        StartCoroutine(DoLinearMove(newPos, speed));

        wait = true;
    }
}
