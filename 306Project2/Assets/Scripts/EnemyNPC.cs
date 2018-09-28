using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPC : MonoBehaviour
{

    private Rigidbody2D enemyBody;
    public float xPos = 0;
    public float yPos = 4;
    private bool movingTowards = true;
    private bool wait = false;

    private Vector2 origonalPos;

    private float speed = 1f;


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

    private void MoveToPos(Vector2 currentPos, Vector2 newPos)
    {

        StartCoroutine(DoMove(newPos));
        
        wait = true;
    }

    private IEnumerator DoMove(Vector3 end)
    {
        //Calculate the remaining distance to move. 
        float RemainingDistance = Vector3.Distance(transform.position, end);

        //While that distance is greater than a very small amount
        while (RemainingDistance > 0.05)
        {
            Debug.Log(RemainingDistance);
            
            Vector2 calculatedPos = Vector2.MoveTowards(transform.position, end, speed * Time.deltaTime);

            enemyBody.MovePosition(calculatedPos);

            //Recalculate the remaining distance after moving.
            RemainingDistance = Vector3.Distance(transform.position, end);

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }

        wait = false;

        if (movingTowards)
        {
            movingTowards = false;
        }
        else
        {
            movingTowards = true;
        }
        
    }
}
