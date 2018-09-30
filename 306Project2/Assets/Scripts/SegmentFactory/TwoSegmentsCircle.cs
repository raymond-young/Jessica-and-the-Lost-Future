using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TwoSegmentsCircle : ISegment
{
    public List<Vector2> CalculatePoints(Vector2 origonalPos, Vector2 endPos, bool clockwise)
    {
        List<Vector2> circlePoints = new List<Vector2>();
        Vector2 secondPos;
        Vector2 thirdPos ;

        if (clockwise)
        {
            secondPos = endPos;            
            thirdPos = new Vector2(endPos.x + (endPos.x-origonalPos.x), origonalPos.y);
            //thirdPos = new Vector2(origonalPos.x , endPos.y + (endPos.y-origonalPos.y) );
        }
        else
        {
            secondPos = endPos;
            thirdPos = new Vector2(origonalPos.x , endPos.y + (endPos.y-origonalPos.y) );
            //thirdPos = new Vector2(endPos.x + (endPos.x-origonalPos.x), origonalPos.y);
        }

        //Important ordering of adding to circle
        circlePoints.Add(secondPos);
        circlePoints.Add(thirdPos);
        circlePoints.Add(secondPos);
        circlePoints.Add(origonalPos);

        return circlePoints;
    }
}
