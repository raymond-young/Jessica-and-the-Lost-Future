using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hop : MonoBehaviour
{
    List<Color> rainbow = new List<Color>();
    int colorIndex;

    enum Arrow { LeftArrow, RightArrow };

    public GameObject stairPrefab;
    public GameObject blobPrefab;
    public Slider slider;
    public GameObject readyPrefab;
    public GameObject goPrefab;

    private Slider bar;
    GameObject ready;
    GameObject go;

    private List<GameObject> stairs = new List<GameObject>();
    private List<Arrow> stairRef = new List<Arrow>();
    
    private int numOfStairs;
    private int pos;

    static System.Random random = new System.Random();

    private float timeLimit;
    private float currentTime;
    private float timePenalty;
    private float readyTime = 0.9f;
    private float goTime = 0.5f;
    
    private bool gameStart;


    private float x;
    private float y;
    private float speed;
    private float speedThreshold;

    // Use this for initialization
    void Start () {
        //Initialise configurations
        numOfStairs = 25;
        timeLimit = 10f;
        timePenalty = timeLimit * 0.5f / numOfStairs;
        
        x = stairPrefab.GetComponent<RectTransform>().rect.width;
        y = 0;
        speed = stairPrefab.GetComponent<RectTransform>().rect.height / 10;
        speedThreshold = 3 * speed;

        //Add colors
        rainbow.Add(new Color(0, 136f/255f, 1));
        rainbow.Add(new Color(1, 170f/255f, 0));
        rainbow.Add(new Color(1, 119f/255f, 0));
        rainbow.Add(new Color(1, 0, 51f/255f));
        rainbow.Add(new Color(153f/255f, 17f/255f, 177f/255f));
        rainbow.Add(new Color(170f/255f, 221f/255f, 34f/255f));
        colorIndex = 0;

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

        //Generate stairs
        for (int i = 0; i < numOfStairs; i++)
        {
            GameObject stair = Instantiate(stairPrefab);
            RectTransform stairRectTransform = stair.GetComponent<RectTransform>();
            stairRectTransform.SetParent(parentRectTransform);
            float thisY;

            //Find the y position according to previous one's y position
            if (i > 0)
            {
                GameObject lastStair = stairs[i - 1];
                thisY = blobPrefab.GetComponent<RectTransform>().rect.height * 1.5f +
                    lastStair.GetComponent<RectTransform>().rect.height +
                    lastStair.GetComponent<RectTransform>().localPosition.y;

            }
            else
            {
                thisY = y;
            }

            //Randomly decide x position (on the left or on the right)
            if (random.Next(0, 2) == 0)
            {
                stairRectTransform.localPosition = new Vector2(x, thisY);
                stairRef.Add(Arrow.RightArrow);
            }
            else
            {
                stairRectTransform.localPosition = new Vector2(-x, thisY);
                stairRef.Add(Arrow.LeftArrow);
            }

            //Assign color
            if (colorIndex > rainbow.Count - 1)
            {
                colorIndex = 0;
            }
            stair.GetComponent<Image>().color = rainbow[colorIndex];
            colorIndex++;

            //Add to stair list
            stairs.Add(stair);
        }

        //Get ready for the game
        currentTime = - readyTime - goTime;
        gameStart = false;
        pos = 0;
    }
    

    void OnGUI()
    {
        
        //Listen to key press event
        Event e = Event.current;
        if (gameStart && e.type == EventType.KeyDown)
        {
            //If the right key was pressed, change color of the game object and move to the next one
            if ( pos < stairs.Count && e.keyCode.ToString().Equals(stairRef[pos].ToString()) && e.keyCode != KeyCode.None)
            {
                GameObject s = stairs[pos];

                if (pos > 0)
                {
                    stairs[pos - 1].GetComponent<Image>().color = Color.gray;
                }
                float blobX = s.GetComponent<RectTransform>().localPosition.x;
                float blobY = s.GetComponent<RectTransform>().localPosition.y 
                    + s.GetComponent<RectTransform>().rect.height / 2 
                    + blobPrefab.GetComponent<RectTransform>().rect.height / 2;
                blobPrefab.GetComponent<RectTransform>().localPosition = new Vector2(blobX, blobY);
                pos++;
                PlayCorrectSound();
                
                //Slowly increase speed
                if (speed < speedThreshold)
                {
                    speed += 0.4f;
                }
            }
            //Press wrong key, add time penalty
            else
            {
                PlayWrongSound();
                currentTime += timePenalty;
                bar.value = Mathf.Lerp(0f, 1f, currentTime / timeLimit);
            }
        }
    }

    bool InSafeZone()
    {
        float y = blobPrefab.GetComponent<RectTransform>().localPosition.y;
        if (pos == 0 && y > stairs[0].GetComponent<RectTransform>().localPosition.y)
        {
            return false;
        }
        return Mathf.Abs(y) < gameObject.GetComponentInParent<Canvas>().pixelRect.height / 2;
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
            else if (Mathf.Abs(currentTime) < goTime) //Show "Go!"
            {
                if (go.activeSelf)
                {
                    float time = Mathf.Sin(Mathf.Lerp(0.25f, 1f, Mathf.Abs(currentTime) / goTime));
                    go.GetComponent<Text>().color = new Color(time, time, time);
                }
                else
                {
                    go.SetActive(true);
                    PlayGoSound();
                    ready.SetActive(false);
                }
            }
            else //Show "Read?"
            {
                if (ready.activeSelf)
                {
                    float percentage = Mathf.Abs(currentTime) - goTime;
                    float time = Mathf.Sin(Mathf.Lerp(0.25f, 1f, percentage / readyTime));
                    ready.GetComponent<Text>().color = new Color(time, time, time);
                }
                else
                {
                    go.SetActive(false);
                    PlayReadySound();
                    ready.SetActive(true);
                }
            }
        }
        else
        {
            //Check whether finished/failed
            if (pos == numOfStairs - 1)
            {
                Finish();
            }
            if (!InSafeZone() || currentTime > timeLimit)
            {
                Debug.Log("not in safe zone");
                Fail();
            }

            //Move all the stairs
            foreach (GameObject stair in stairs)
            {
                RectTransform stairRectTransform = stair.GetComponent<RectTransform>();
                float newY = stairRectTransform.localPosition.y - speed;
                float newX = stairRectTransform.localPosition.x;
                stairRectTransform.localPosition = new Vector2(newX, newY);
            }

            //Manually move blob
            if (pos > 0)
            {
                float blobX = blobPrefab.GetComponent<RectTransform>().localPosition.x;
                float blobY = blobPrefab.GetComponent<RectTransform>().localPosition.y - speed;
                blobPrefab.GetComponent<RectTransform>().localPosition = new Vector2(blobX, blobY);
            }

            //Update time bar
            bar.value = Mathf.Lerp(0f, 1f, currentTime / timeLimit);
        }
        //Update timer
        currentTime += Time.deltaTime;
    }

    private void Finish()
    {
        PlaySucceedSound();
        //Notify the game manager that the player has successfully finished the game
        //GameObject.FindGameObjectWithTag("MiniGameManager").GetComponent<MiniGameManager>().FinishGame(true);

    }

    private void Fail()
    {
        PlayFailSound();
        //Notify the game manager that the player has failed the game
        //GameObject.FindGameObjectWithTag("MiniGameManager").GetComponent<MiniGameManager>().FinishGame(false);
    }

    public void PlayCorrectSound()
    {
        Debug.Log("Play Sound");
        GameObject sound = GameObject.Find("Arrow Correct");
        sound.GetComponent<AudioSource>().Play(0);

    }

    public void PlayWrongSound()
    {
        Debug.Log("Play Sound");
        GameObject sound = GameObject.Find("Arrow Wrong");
        sound.GetComponent<AudioSource>().Play(0);
    }

    public void PlaySucceedSound()
    {
        Debug.Log("Play Sound");
        GameObject sound = GameObject.Find("Succeed");
        sound.GetComponent<AudioSource>().Play(0);

    }

    public void PlayFailSound()
    {
        Debug.Log("Play Sound");
        GameObject sound = GameObject.Find("Fail");
        sound.GetComponent<AudioSource>().Play(0);

    }

    public void PlayReadySound()
    {
        Debug.Log("Play Sound");
        GameObject sound = GameObject.Find("Ready Set");
        sound.GetComponent<AudioSource>().Play(0);
    }

    public void PlayGoSound()
    {
        Debug.Log("Play Sound");
        GameObject sound = GameObject.Find("Go");
        sound.GetComponent<AudioSource>().Play(0);
    }
}
