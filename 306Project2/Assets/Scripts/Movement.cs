using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour {

    private Rigidbody2D npcBody;
    private GameObject npc;

	// Use this for initialization
	protected virtual void Start () {
        npcBody = gameObject.GetComponent<Rigidbody2D>();
        npc = gameObject;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    protected IEnumerator DoMove(Vector3 end)
    {
        //Calculate the remaining distance to move based on the square magnitude of the difference between current position and end parameter. 
        float sqrRemainingDistance = (transform.position - end).sqrMagnitude;

        //While that distance is greater than a very small amount (Epsilon, almost zero):
        while (sqrRemainingDistance > float.Epsilon)
        {
            //Find a new position proportionally closer to the end, based on the moveTime
            Vector3 newPostion = Vector3.MoveTowards(npcBody.position, end, 1/1.0f * Time.deltaTime);

            //Call MovePosition on attached Rigidbody2D and move it to the calculated position.
            npcBody.MovePosition(newPostion);

            //Recalculate the remaining distance after moving.
            sqrRemainingDistance = (transform.position - end).sqrMagnitude;

            //Return and loop until sqrRemainingDistance is close enough to zero to end the function
            yield return null;
        }
    }
}
