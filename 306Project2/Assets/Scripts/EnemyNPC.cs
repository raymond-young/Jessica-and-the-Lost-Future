using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPC : Movement
{

    private Rigidbody2D enemyBody;
    public float xPos;
    public float yPos;
    private bool movingTowards = true;
    private bool wait = false;

    private Vector2 origonalPos;

    public float speed = 1f;


    // Use this for initialization
    void Start () {
        enemyBody = gameObject.GetComponent<Rigidbody2D>();
        origonalPos = gameObject.transform.position;
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        if (!wait)
        {
            Vector2 newPos;
            if (movingTowards)
            {
                newPos = new Vector2(xPos, yPos);
            }
            else
            {
                Debug.Log("Have Waited");
                newPos = origonalPos;
            }
            Debug.Log(origonalPos + " " + newPos);
            MoveToPos(origonalPos, newPos);
        }
  
    }

    public void MoveToPos(Vector2 currentPos, Vector2 newPos)
    {

        StartCoroutine(DoMove(newPos));
        
        wait = true;
    }

    
}
