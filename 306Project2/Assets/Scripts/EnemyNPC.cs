using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyNPC : Movement
{

    public float xPos;
    public float yPos;

    private Vector2 origonalPos;
    private Vector2 newPos;

    public float speed = 1f;

    public bool clockwise;

    

    private int segmentCount;

    public enum segmentNumber // your custom enumeration
    {
        one,
        two,
        three,
        four
    };
    public segmentNumber segmentNum = segmentNumber.one;
    private int segInt;

    public enum myEnum // your custom enumeration
    {
        linear,
        curve,
        square
    };
    public myEnum dropDown = myEnum.linear;  // this public var should appear as a drop down

    // Use this for initialization
    protected override void Start () {
        origonalPos = gameObject.transform.position;

        if (segmentNum == segmentNumber.one)
        {
            segInt = 1;
        }
        else if (segmentNum == segmentNumber.two)
        {
            segInt = 2;
        }
        else if (segmentNum == segmentNumber.three)
        {
            segInt = 3;
        }
        else if (segmentNum == segmentNumber.four)
        {
            segInt = 4;
        }

        segmentCount = segInt;
        base.Start();
    }

    // Update is called once per frame
    protected override void FixedUpdate() {
        if (dropDown.Equals(myEnum.linear))
        {
            if (!wait)
            {
                Vector2 newPos;
                if (movingTowards)
                {
                    newPos = new Vector2(xPos, yPos);
                }
                else
                {
                    newPos = origonalPos;
                }
                StartCoroutine(DoLinearMove(newPos, speed));
                wait = true;
            }
        }
        else if (dropDown.Equals(myEnum.curve))
        {

            if (!wait)
            {
                if (segmentNum == segmentNumber.one && segmentCount <= segInt)
                {
                    newPos = new Vector2(xPos, yPos);

                    StartCoroutine(DoCircularMove(newPos, speed, clockwise));
                    wait = true;

                    ToogleCurve();
                    segmentCount++;
                }
                else if (segmentNum == segmentNumber.one && segmentCount >= 2)
                {
                    StartCoroutine(DoCircularMove(origonalPos, speed, clockwise));
                    wait = true;

                    ToogleCurve();
                    segmentCount--;
                }
                
            }
        }
        else if (dropDown.Equals(myEnum.square))
        {
            if (!wait)
            {
                
                newPos = new Vector2(xPos, yPos);
                StartCoroutine(DoRectangleMove(origonalPos, newPos, speed, clockwise, segInt));
                wait = true;
            }
        }
        
  
    }

    private void ToogleCurve()
    {
        if (clockwise)
        {
            clockwise = false;
        }
        else
        {
            clockwise = true;
        }
    }


    
}
