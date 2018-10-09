using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hop : MonoBehaviour
{
    enum Arrow { LeftArrow, RightArrow };

    public GameObject stairPrefab;
    public GameObject blobPrefab;
    public Slider slider;
    public GameObject readyPrefab;
    public GameObject goPrefab;

    private Queue<GameObject> stairQueue = new Queue<GameObject>();
    private GameObject lastStair;
    private GameObject stepOnStair;
    private Queue<Arrow> stairRef = new Queue<Arrow>();
    private Slider bar;
    GameObject ready;
    GameObject go;

    private int numOfStairs;

    static System.Random random = new System.Random();

    private float timeLimit;
    private float currentTime;
    private float readyTime = 0.9f;
    private float goTime = 0.5f;
    
    private bool gameStart;

    private float timePenalty;
    private float speedThreshold;

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
        speedThreshold = 3 * speed;
        lastStair = null;
        
        //Initialise time bar
        RectTransform parentRectTransform = gameObject.GetComponent<RectTransform>();
        bar = Instantiate(slider);
        RectTransform barRectTransform = bar.GetComponent<RectTransform>();
        barRectTransform.sizeDelta = new Vector2(gameObject.GetComponentInParent<Canvas>().pixelRect.width * 0.95f,
            gameObject.GetComponentInParent<Canvas>().pixelRect.height * 0.02f);
        float sliderYPosition = gameObject.GetComponentInParent<Canvas>().pixelRect.height / 2 - barRectTransform.rect.height;
        barRectTransform.SetParent(parentRectTransform);
        barRectTransform.localPosition = new Vector2(0, -sliderYPosition);
        bar.value = 0;

        //Generate Ready/Go and set default properties
        ready = Instantiate(readyPrefab);
        ready.GetComponent<RectTransform>().SetParent(parentRectTransform);
        ready.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        ready.SetActive(false);

        go = Instantiate(goPrefab);
        go.GetComponent<RectTransform>().SetParent(parentRectTransform);
        go.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
        go.SetActive(false);
        
        currentTime = - readyTime - goTime;
        gameStart = false;
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
        if (speed < speedThreshold) {
            speed += 0.1f;
        }
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

        if (!inSafeZone())
        {
            Debug.Log("not in safe zone");
            Fail();
        }    
        //Listen to key press event
        Event e = Event.current;
        if (gameStart && e.type == EventType.KeyDown)
        {
            //If the right key was pressed, change color of the game object and move to the next one
            if (e.keyCode.ToString().Equals(stairRef.Peek().ToString()) && e.keyCode != KeyCode.None)
            {
                GameObject s = stairQueue.Dequeue();
                stairRef.Dequeue();

                if (stepOnStair != null)
                {
                    Destroy(stepOnStair);
                }
                stepOnStair = s;
                float blobX = s.GetComponent<RectTransform>().localPosition.x;
                float blobY = s.GetComponent<RectTransform>().localPosition.y 
                    + s.GetComponent<RectTransform>().rect.height/2 
                    + blobPrefab.GetComponent<RectTransform>().rect.height / 2;
                blobPrefab.GetComponent<RectTransform>().localPosition = new Vector2(blobX, blobY);
            }
            else
            {
                Fail();
            }
        }
    }

    bool inSafeZone()
    {
        float y = blobPrefab.GetComponent<RectTransform>().localPosition.y;
        return Mathf.Abs(y) < gameObject.GetComponentInParent<Canvas>().pixelRect.height / 2
            && y < stairQueue.Peek().GetComponent<RectTransform>().localPosition.y;
    }

    void Update () {
        if (!gameStart)
        {   
            //Start game. Set arrows visible
            if (currentTime >= 0)
            {
                go.SetActive(false);
                ready.SetActive(false);
                gameStart = true;
                currentTime = 0;
            }
            //Show "Go!"
            else if (Mathf.Abs(currentTime) < goTime)
            {
                if (go.activeSelf)
                {
                    float time = Mathf.Sin(Mathf.Lerp(0f, 1f, Mathf.Abs(currentTime) / goTime));
                    go.GetComponent<Text>().color = new Color(time, time, 0);
                }
                else
                {
                    go.SetActive(true);
                    ready.SetActive(false);
                }
            }
            //Show "Read?"
            else
            {
                if (ready.activeSelf)
                {
                    float percentage = Mathf.Abs(currentTime) - goTime;
                    float time = Mathf.Sin(Mathf.Lerp(0f, 1f, percentage / readyTime));
                    ready.GetComponent<Text>().color = new Color(0, time, 0);
                }
                else
                {
                    go.SetActive(false);
                    ready.SetActive(true);
                }
            }
        }
        else
        {
            //Move all the stairs
            foreach (GameObject stair in stairQueue)
            {
                RectTransform stairRectTransform = stair.GetComponent<RectTransform>();
                float newY = stairRectTransform.localPosition.y - speed;
                float newX = stairRectTransform.localPosition.x;
                stairRectTransform.localPosition = new Vector2(newX, newY);
            }

            //Manually move blob and the stair it is on
            if (stepOnStair != null)
            {
                float blobX = blobPrefab.GetComponent<RectTransform>().localPosition.x;
                float blobY = blobPrefab.GetComponent<RectTransform>().localPosition.y - speed;
                blobPrefab.GetComponent<RectTransform>().localPosition = new Vector2(blobX, blobY);
                float stairX = stepOnStair.GetComponent<RectTransform>().localPosition.x;
                float stairY = stepOnStair.GetComponent<RectTransform>().localPosition.y - speed;
                stepOnStair.GetComponent<RectTransform>().localPosition = new Vector2(stairX, stairY);
            }
            
            //Update time bar
            bar.value = Mathf.Lerp(0f, 1f, currentTime / timeLimit);
        }
        //Update timer
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
