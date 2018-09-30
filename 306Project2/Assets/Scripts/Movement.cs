using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Movement : MonoBehaviour {

    private Rigidbody2D npcBody;

    protected bool wait = false;
    protected bool movingTowards = true;

    private float roundness = 10;

    // Use this for initialization
    protected virtual void Start () {
        npcBody = gameObject.GetComponent<Rigidbody2D>();
      
    }

    // Update is called once per frame
    protected virtual void FixedUpdate () {
		
	}

    protected IEnumerator DoLinearMove(Vector3 end, float speed)
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

    protected IEnumerator DoRectangleMove(Vector2 origonalPos, Vector2 thirdPos, float speed, bool clockwise, int segmentNum)
    {

        ISegment segment = SegmentFactory.MakePoints(segmentNum, PathMovement.square);
        List<Vector2> rectanglePoints = segment.CalculatePoints(origonalPos, thirdPos, clockwise);

       
        for (int i = 0; i < rectanglePoints.Count; i++)
        {

            //Calculate the remaining distance to move. 
            float RemainingDistance = Vector2.Distance(transform.position, rectanglePoints[i]);
            //While that distance is greater than a very small amount
            while (RemainingDistance > 0.05)
            {
                npcBody.velocity = new Vector2(1, 0);

                Vector2 calculatedPos = Vector2.MoveTowards(transform.position, rectanglePoints[i], speed * Time.deltaTime);
                npcBody.MovePosition(calculatedPos);
                //Recalculate the remaining distance after moving.
                RemainingDistance = Vector2.Distance(transform.position, rectanglePoints[i]);
                //Return and loop until sqrRemainingDistance is close enough to zero to end the function
                yield return null;
            }
        }
        wait = false;
    }

    protected IEnumerator DoCircularMove(Vector2 origionalPos, Vector2 endPos, float speed, int segmentNum)
    {
        Vector2 centerPos = new Vector2();
        float angle = 0;

        int sin = 1;
        int cos = 1;

        // Top left of circle
        if ((origionalPos.x < endPos.x) && (origionalPos.y < endPos.y))
        {

            sin = 1;
            cos = 1;
            angle = 3.14f;
        }
        // Bottom left of circle
        else if ((origionalPos.x < endPos.x) && (origionalPos.y > endPos.y))
        {
            centerPos = new Vector2(endPos.x, origionalPos.y);

            sin = -1;
            cos = 1;
            angle = -3.14f;
        }
        // Top right of circle
        else if ((origionalPos.x > endPos.x) && (origionalPos.y < endPos.y))
        {
            centerPos = new Vector2(endPos.x, origionalPos.y);

            sin = -1;
            cos = 1;
            angle = 0;
        }
        // bottom right of circle
        else if ((origionalPos.x > endPos.x) && (origionalPos.y > endPos.y))
        {
            centerPos = new Vector2(endPos.x, origionalPos.y);

            sin = 1;
            cos = 1;
            angle = 0;
        }

        //Radius
        float radius = Vector2.Distance(origionalPos, centerPos);

        float totalRotation = 0;

        //Rotation amount depended on segment number
        if (segmentNum == 1)
        {
            totalRotation = 1.57f;
        }
        else if (segmentNum == 2)
        {
            totalRotation = 3.14f;
        }
        else if (segmentNum == 3)
        {
            totalRotation = 4.71f;
        }
        else if (segmentNum == 4)
        {
            totalRotation = 6.283f;
        }

        float oriRotation = totalRotation;

        //Rotate until totalRotation completed
        while (totalRotation > 0)
        {
            //Step size
            float step = speed * Time.deltaTime;

            //Rotation angles
            totalRotation = totalRotation - step;
            angle = angle - step;

            //Trig calculation
            Vector2 offset = new Vector2(cos * Mathf.Cos(angle), sin * Mathf.Sin(angle)) * radius;
            
            //Change position relitive to center pos 
            npcBody.MovePosition(centerPos + offset);

            yield return null;
        }

        //Repeat rotation in other direction
        while (totalRotation < oriRotation)
        {
            float step = -speed * Time.deltaTime;

            totalRotation = totalRotation - step;
            angle = angle - step;

            Vector2 offset = new Vector2(cos * Mathf.Cos(angle), sin * Mathf.Sin(angle)) * radius;

            npcBody.MovePosition(centerPos + offset);

            yield return null;
        }

        wait = false;
    }

}
