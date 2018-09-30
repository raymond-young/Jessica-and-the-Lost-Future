using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISegment {

    List<Vector2> CalculatePoints(Vector2 orignalPos, Vector2 endPos, bool clockwise);
}
