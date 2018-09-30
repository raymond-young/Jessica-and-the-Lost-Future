using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeSegmentsCircle : ISegment
{
    public List<Vector2> CalculatePoints(Vector2 origonalPos, Vector2 secondPos, bool clockwise)
    {
        List<Vector2> rectanglePoints = new List<Vector2>();

        Vector2 thirdPos;
        Vector2 fourthPos;

        if (clockwise)
        {
            thirdPos = new Vector2(secondPos.x * 2, origonalPos.y);
            fourthPos = new Vector2(secondPos.x, secondPos.y * -1);
        }
        else
        {
            thirdPos = new Vector2(secondPos.x * -2, origonalPos.y);
            fourthPos = new Vector2(origonalPos.x, secondPos.y * -1);
        }


        rectanglePoints.Add(secondPos);
        rectanglePoints.Add(thirdPos);
        rectanglePoints.Add(fourthPos);
        rectanglePoints.Add(thirdPos);
        rectanglePoints.Add(secondPos);
        rectanglePoints.Add(origonalPos);

        return rectanglePoints;
    }
}
