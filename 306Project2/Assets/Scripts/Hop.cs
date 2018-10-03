using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hop : MonoBehaviour {
    public GameObject stairPrefab;
    public GameObject blobPrefab;

    private Queue<GameObject> stairQueue = new Queue<GameObject>();
    private GameObject lastStair;
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

    // Use this for initialization
    void Start () {
        numOfStairs = 10;
        timeLimit = 10f;
        
        x = stairPrefab.GetComponent<RectTransform>().rect.width;
        y = gameObject.GetComponentInParent<Canvas>().pixelRect.height /2;
        lastStair = null;
        //Debug.Log(lastStair);
        generateStair();
        generateStair();
    }


    void generateStair()
    {
        GameObject stair = Instantiate(stairPrefab);

        RectTransform parentRectTransform = gameObject.GetComponent<RectTransform>();
        RectTransform stairRectTransform = stair.GetComponent<RectTransform>();
        stairRectTransform.SetParent(parentRectTransform);

        float thisY;
        if (lastStair != null)
        {
            Debug.Log("previous Y " + lastStair.GetComponent<RectTransform>().localPosition.y);
            thisY = 80 + lastStair.GetComponent<RectTransform>().localPosition.y;
        } 
        else
        {
            thisY = y;
        }

        Debug.Log("thisY: " + thisY);

        if (random.Next(0, 2) == 0)
        {
            stairRectTransform.localPosition = new Vector2(x, thisY);   
        }
        else
        {
            stairRectTransform.localPosition = new Vector2(-x, thisY);
        }
        stairQueue.Enqueue(stair);
        lastStair = stair;
    }



	
	// Update is called once per frame
	void Update () {
		
	}
}
