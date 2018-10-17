using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingBall : MonoBehaviour {
    public GameObject ballPrefab;
    public GameObject backet;
    public GameObject boundary;
    public Text count;
    public GameObject toCatch;

    public Slider slider;
    public GameObject readyPrefab;
    public GameObject goPrefab;
    
    // List of possible sprite appearances.
    public List<Sprite> possibleSprites = new List<Sprite>();

    // Game objects for user feedback.
    Slider bar;
    GameObject ready;
    GameObject go;

    // Variables controlling aspects of the game's difficulty.
    float timeLimit;
    int goal;
    float generationGap;

    float currentTime;
    float generateTime;
    float readyTime = 0.9f;
    float goTime = 0.5f;


    // Position contraints
    float xRange;
    float y;

    // Ways to prevent the user from going past the limit of the screen.
    float wall;
    bool gameStart;

    float timePenalty;

    // Use this for initialization
    void Start () {
        //InitDifficulty(2);
        InitDifficulty(3);

        xRange = boundary.GetComponent<RectTransform>().rect.width / 2;
        y = gameObject.GetComponentInParent<Canvas>().pixelRect.height / 2;

        wall = xRange - backet.GetComponent<RectTransform>().rect.width / 2;

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

        //Get ready to start game
        currentTime = -readyTime - goTime;
        generateTime = 0f;
        gameStart = false;
        count.gameObject.SetActive(false);
        toCatch.SetActive(false);
    }
    


    // Update is called once per frame
    void Update () {
        if (!gameStart)
        {
            if (currentTime >= 0) //Start game. Set arrows visible
            {
                count.gameObject.SetActive(true);
                toCatch.SetActive(true);
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
            //Finish game when there is no more arrows to press or timeout
            if (goal <= 0)
            {
                Finish();
            }
            if (currentTime > timeLimit)
            {
                Fail();
            }
    
            //Generate new balls
            if (generateTime > generationGap)
            {
                // Create a new ball.
                GameObject ball = Instantiate(ballPrefab);
                // Set it to have a random appearance.
                int s = Random.Range(0, possibleSprites.Count);
                ball.GetComponent<Image>().sprite = possibleSprites[s];

                // Get the ball's parent and position.
                ball.GetComponent<RectTransform>().SetParent(gameObject.GetComponent<RectTransform>());
                // Set it to a random position.
                ball.GetComponent<RectTransform>().localPosition = new Vector2(Random.Range(0, xRange) * Random.Range(-1f, 1f), y);
                    
                generateTime = 0;
            }
            //Update time bar
            bar.value = Mathf.Lerp(0f, 1f, currentTime / timeLimit);
        }
        currentTime += Time.deltaTime;
        generateTime += Time.deltaTime;
    }

    public void CatchBall(GameObject ball)
    {
        PlayCorrectSound();
        Destroy(ball);
        goal--;
        count.text = goal.ToString();
    }

    public void MissBall()
    {
        PlayWrongSound();
        currentTime += timePenalty;
        bar.value = Mathf.Lerp(0f, 1f, currentTime / timeLimit);
    }

    private void InitDifficulty(int chapther)
    {
        switch (chapther)
        {
            case 3:
                goal = 12;
                timeLimit = 10f;
                generationGap = 0.5f;
                timePenalty = timeLimit / goal;
                count.text = goal.ToString();
                break;
            case 2:
            default:
                goal = 6;
                timeLimit = 10f;
                generationGap = 0.9f;
                timePenalty = timeLimit / goal;
                count.text = goal.ToString();
                break;
        }
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

    // Returns the boundary the backet can move to
    public float GetWall()
    {
        return wall;
    }

    // A way to query if the game has started.
    public bool gameStarted()
    {
        return gameStart;
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
