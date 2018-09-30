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

    protected IEnumerator DoCircularMove(Vector2 origionalPos, Vector2 endPos, float speed, bool clockwise, int segmentNum)
    {
        ISegment seg = SegmentFactory.MakePoints(segmentNum, PathMovement.square);
        List<Vector2> rectanglePoints = seg.CalculatePoints(origionalPos, endPos, clockwise);

        Vector2 centerPos;
        if (clockwise)
        {
            centerPos = new Vector2(endPos.x, transform.position.y);
        }
        else
        {
            centerPos = new Vector2(transform.position.x, endPos.y);

        }

        for (int recNum = 0; recNum < rectanglePoints.Count; recNum++)
        {
            for (int i = 0; i < roundness; i++)
            {
                float segment = i / roundness;

                Vector2 intermidiatePos = BezierPoints(segment, transform.position, centerPos, rectanglePoints[i]);

                //Calculate the remaining distance to move. 
                float RemainingDistance = Vector2.Distance(transform.position, intermidiatePos);
                //While that distance is greater than a very small amount
                while (RemainingDistance > 0.05)
                {
                    npcBody.velocity = new Vector2(1, 0);

                    Vector2 calculatedPos = Vector2.MoveTowards(transform.position, intermidiatePos, speed * Time.deltaTime);
                    npcBody.MovePosition(calculatedPos);
                    //Recalculate the remaining distance after moving.
                    RemainingDistance = Vector2.Distance(transform.position, intermidiatePos);

                    //Return and loop until sqrRemainingDistance is close enough to zero to end the function
                    yield return null;
                }
            }

        }

        wait = false;
    }

    private Vector2 BezierPoints(float segment, Vector2 p0, Vector2 p1, Vector2 p2)
    {
        //B(t) = (1-t)2P0 + 2(1-t)tP1 + t2P2 , 0 < t < 1 equation

        float u = 1 - segment;
        float tt = segment * segment;
        float uu = u * u;

        Vector2 firstPart = uu * p0;
        Vector2 secondPart = 2 * u * segment * p1;
        Vector2 thirdPart = tt * p2;

        Vector2 point = firstPart + secondPart + thirdPart;

        return point;
    }

 
}
