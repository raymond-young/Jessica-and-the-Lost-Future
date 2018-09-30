using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeSegments : ISegment
{
    public List<Vector2> CalculatePoints(Vector2 origonalPos, Vector2 thirdPos, bool clockwise)
    {
        List<Vector2> rectanglePoints = new List<Vector2>();

        Vector2 secondPos;
        Vector2 fourthPos;
        if (clockwise)
        {
            secondPos = new Vector2(origonalPos.x, thirdPos.y);
            fourthPos = new Vector2(thirdPos.x, origonalPos.y);

        }
        else
        {
            secondPos = new Vector2(thirdPos.x, origonalPos.y);
            fourthPos = new Vector2(origonalPos.x, thirdPos.y);
        }

        //Important ordering of adding to rectangle
        rectanglePoints.Add(secondPos);
        rectanglePoints.Add(thirdPos);
        rectanglePoints.Add(fourthPos);
        rectanglePoints.Add(thirdPos);
        rectanglePoints.Add(secondPos);
        rectanglePoints.Add(origonalPos);

        return rectanglePoints;
    }
}
