using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneSegmentCircle : ISegment
{
    public List<Vector2> CalculatePoints(Vector2 origonalPos, Vector2 endPos, bool clockwise)
    {
        List<Vector2> rectanglePoints = new List<Vector2>();

        rectanglePoints.Add(endPos);
        rectanglePoints.Add(origonalPos);

        return rectanglePoints;
    }
}
