using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPC : Movement
{

    private Rigidbody2D enemyBody;
    public float xPos;
    public float yPos;

    private Vector2 origonalPos;

    public float speed = 1f;


    // Use this for initialization
    protected override void Start () {
        enemyBody = gameObject.GetComponent<Rigidbody2D>();
        origonalPos = gameObject.transform.position;
        base.Start();
    }

    // Update is called once per frame
    protected override void FixedUpdate() {

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

        StartCoroutine(DoMove(newPos, speed));
        
        wait = true;
    }

    
}
