using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FallingBall : MonoBehaviour {
    public GameObject ballPrefab;
    public GameObject backet;
    public Text count;
    

    List<GameObject> balls = new List<GameObject>();

    int numOfBalls = 10;
    int goal;
    float xRange;
    float y;

    float leftWall;
    float rightWall;

	// Use this for initialization
	void Start () {
        goal = 98;
        count.text = goal.ToString();
        

        xRange = gameObject.GetComponentInParent<Canvas>().pixelRect.width / 2;
        y = gameObject.GetComponentInParent<Canvas>().pixelRect.height / 2;

        rightWall = gameObject.GetComponentInParent<Canvas>().pixelRect.width / 2;
        leftWall = -rightWall;
    }
    


    void OnGUI()
    {
        //Finish game when tehre is no more arrows to press
        if (goal == 0)
        {
            Finish();
        }
        //if (gameStart)
        //{
            //Listen to key press event
            Event e = Event.current;
            if (e.type == EventType.KeyDown)
            {
                float x = backet.GetComponent<RectTransform>().localPosition.x;
                if (e.keyCode.ToString().Equals("LeftArrow") && x > leftWall)
                {
                    backet.GetComponent<RectTransform>().localPosition.x = x - 10;
                }
                else if (e.keyCode.ToString().Equals("RightArrow") && x < rightWall)
                {
                    backet.GetComponent<RectTransform>().localPosition.x = x + 10;

                }
            }
       // }
    }

    // Update is called once per frame
    void Update () {
        
        if (balls.Count < numOfBalls)
        {
            GameObject ball = Instantiate(ballPrefab);
            ball.GetComponent<RectTransform>().SetParent(gameObject.GetComponent<RectTransform>());
            ball.GetComponent<RectTransform>().localPosition = new Vector2(Random.Range(0, xRange) * Random.Range(-1f, 1f), y);
            balls.Add(ball);
        }
	}


    private void Finish()
    {
        //Notify the game manager that the player has successfully finished the game
        //GameObject.FindGameObjectWithTag("MiniGameManager").GetComponent<MiniGameManager>().FinishGame(true);
        Debug.Log("finished");
    }

    private void Fail()
    {
        //Notify the game manager that the player has failed the game
        //GameObject.FindGameObjectWithTag("MiniGameManager").GetComponent<MiniGameManager>().FinishGame(false);
    }
}
