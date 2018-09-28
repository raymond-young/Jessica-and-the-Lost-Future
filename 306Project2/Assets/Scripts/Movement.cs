using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour {

    private Rigidbody2D npcBody;

    protected bool wait = false;
    protected bool movingTowards = true;

    // Use this for initialization
    protected virtual void Start () {
        npcBody = gameObject.GetComponent<Rigidbody2D>();
      
    }

    // Update is called once per frame
    protected virtual void FixedUpdate () {
		
	}

    protected IEnumerator DoMove(Vector3 end, float speed)
    {
        //Calculate the remaining distance to move. 
        float RemainingDistance = Vector3.Distance(transform.position, end);
        //While that distance is greater than a very small amount
        while (RemainingDistance > 0.05)
        {
            npcBody.velocity = new Vector2(1, 0);

            Vector2 calculatedPos = Vector2.MoveTowards(transform.position, end, speed * Time.deltaTime);
            npcBody.MovePosition(calculatedPos);
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
