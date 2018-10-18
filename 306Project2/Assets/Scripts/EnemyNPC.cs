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

    private Animator anim;
    

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


    public PathMovement dropDown = PathMovement.linear;  // this public var should appear as a drop down

    // Use this for initialization
    protected override void Start () {
        origonalPos = gameObject.transform.position;

        anim = GetComponent<Animator>();

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
    protected override void customFixedUpdate() {
        if (dropDown.Equals(PathMovement.linear))
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

                //update character animation 
                SetAnimationFacingDirection(newPos);
            }
        }
        else if (dropDown.Equals(PathMovement.curve))
        {

            if (!wait)
            {
                newPos = new Vector2(xPos, yPos);
                StartCoroutine(DoCircularMove(origonalPos, newPos, speed, segInt));
                wait = true;
                
            }
        }
        else if (dropDown.Equals(PathMovement.square))
        {
            if (!wait)
            {
                newPos = new Vector2(xPos, yPos);
                StartCoroutine(DoRectangleMove(origonalPos, newPos, speed, clockwise, segInt));
                wait = true;
            }
        }
        

    }

    void LateUpdate()
    {
        //Minimap enemy position code
        for (int i = 0; i < gameObject.transform.childCount; i++)
        {
            if (gameObject.transform.GetChild(i).name.Equals("miniMapEnemy"))
            {
                gameObject.transform.GetChild(i).gameObject.transform.position = gameObject.transform.position;
            }
        }
    }

    /**
     * Method that checks which direction the NPC should be facing depending on its destination
     * coordinates
     */
    private void SetAnimationFacingDirection(Vector2 position) {
        //character moving up
        if ((Mathf.Abs(position.x - transform.position.x) < 0.5) && (position.y-transform.position.y > 0)) {
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", 1);
        }
        //character moving down
        else if ((Mathf.Abs(position.x- transform.position.x) < 0.5) && (position.y- transform.position.y <= 0)) {
            anim.SetFloat("MoveX", 0);
            anim.SetFloat("MoveY", -1);
        }
        //character moving left 
        else if (position.x < transform.position.x) {
            anim.SetFloat("MoveX", -1);
            anim.SetFloat("MoveY", 0);
        }
        //chraracter moving right
        else {
            anim.SetFloat("MoveX", 1);
            anim.SetFloat("MoveY", 0);
        }
    }

    
}
