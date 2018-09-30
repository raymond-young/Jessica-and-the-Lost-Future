using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SegmentFactory  {

    public static ISegment MakePoints(int segmentNum)
    {
        if (segmentNum == 2)
        {
            return new TwoSegments();
        }
        else if (segmentNum == 3)
        {
            return new ThreeSegments();
        }
        else
        {
            return new FourSegments();
        }
    }
}
