using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hop : MonoBehaviour
{
    enum Arrow { LeftArrow, RightArrow };

    public GameObject stairPrefab;
    public GameObject blobPrefab;

    private Queue<GameObject> stairQueue = new Queue<GameObject>();
    private GameObject lastStair;
    private Queue<Arrow> stairRef = new Queue<Arrow>();
    private int numOfStairs;

    static System.Random random = new System.Random();

    private float timeLimit;
    private float currentTime;
    private float readyTime = 0.9f;
    private float goTime = 0.5f;
    
    private bool gameStart;

    private float timePenalty;

    private float x;
    private float y;
    private float speed;

    // Use this for initialization
    void Start () {
        //Initialise configurations
        numOfStairs = 10;
        timeLimit = 10f;
        
        x = stairPrefab.GetComponent<RectTransform>().rect.width;
        y = gameObject.GetComponentInParent<Canvas>().pixelRect.height / 2;
        speed = stairPrefab.GetComponent<RectTransform>().rect.height / 10;
        lastStair = null;

        generateStair();
        generateStair();

        currentTime = 0;
    }


    //One call of this function will generate one new stair and add it to the queue
    void generateStair()
    {
        GameObject stair = Instantiate(stairPrefab);

        RectTransform parentRectTransform = gameObject.GetComponent<RectTransform>();
        RectTransform stairRectTransform = stair.GetComponent<RectTransform>();
        stairRectTransform.SetParent(parentRectTransform);

        //Decide the Y postion of the new stair according to the previous one's y position
        float thisY;
        if (lastStair != null)
        {
            thisY = blobPrefab.GetComponent<RectTransform>().rect.height * 1.5f +
                lastStair.GetComponent<RectTransform>().rect.height +
                lastStair.GetComponent<RectTransform>().localPosition.y;
        } 
        else
        {
            thisY = y;
        }

        if (random.Next(0, 2) == 0)
        {
            stairRectTransform.localPosition = new Vector2(x, thisY);
            stairRef.Enqueue(Arrow.RightArrow);
        }
        else
        {
            stairRectTransform.localPosition = new Vector2(-x, thisY);
            stairRef.Enqueue(Arrow.LeftArrow);
        }
        stairQueue.Enqueue(stair);
        lastStair = stair;
    }


    void OnGUI()
    {
        //Listen to key press
        if (currentTime > timeLimit)
        {
            Finish();
        }
        if (stairRef.Count < numOfStairs)
        {
            generateStair();
        }

        //Listen to key press event
        Event e = Event.current;
        if (e.type == EventType.KeyDown)
        {
            //If the right key was pressed, change color of the game object and move to the next one
            if (e.keyCode.ToString().Equals(stairRef.Peek().ToString()) && e.keyCode != KeyCode.None)
            {
                GameObject s = stairQueue.Dequeue();
                Destroy(s);
                stairRef.Dequeue();
            }
            else
            {
                Fail();
            }
        }
    }

    
    void Update () {
		//Update 
        foreach (GameObject stair in stairQueue)
        {
            RectTransform stairRectTransform = stair.GetComponent<RectTransform>();
            float newY = stairRectTransform.localPosition.y - speed;
            float newX = stairRectTransform.localPosition.x;
            stairRectTransform.localPosition = new Vector2(newX, newY);

        }
        currentTime += Time.deltaTime;
	}



    private void Finish()
    {
        Debug.Log("finished");
        //Notify the game manager that the player has successfully finished the game
        //GameObject.FindGameObjectWithTag("MiniGameManager").GetComponent<MiniGameManager>().FinishGame(true);
    }

    private void Fail()
    {
        Debug.Log("failed");
        //Notify the game manager that the player has failed the game
        //GameObject.FindGameObjectWithTag("MiniGameManager").GetComponent<MiniGameManager>().FinishGame(false);
    }
}
